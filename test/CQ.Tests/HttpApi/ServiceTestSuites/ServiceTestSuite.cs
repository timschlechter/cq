using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CQ.HttpApi.Client;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.ServiceTestSuites
{
    [TestFixture]
    public abstract class ServiceTestSuite
    {
        [SetUp]
        public virtual void SetUp()
        {
            _httpApiClient = new HttpApiClient(_rootUrl);

            _handledCommands = new List<TestCommand>();
            _handledQueries = new Dictionary<ITestQuery, object>();

            var commandTypes = new[]
            {
                typeof(TestCommand),
                typeof(ErrorCommand),
                typeof(IntegerListCommand)
            };

            var queryTypes = new[]
            {
                typeof(IntQuery),
                typeof(IntegerListQuery)
            };

            Action<object> handleCommand = command =>
            {
                _handledCommands.Add(command as TestCommand);

                if (command is ErrorCommand)
                {
                    throw new Exception("ErrorCommand Message");
                }
            };

            Func<object, object> handleQuery = query =>
            {
                var testQuery = (ITestQuery) query;
                _handledQueries.Add(testQuery, testQuery.FixedResult);
                return testQuery.FixedResult;
            };

            StartService(_rootUrl, commandTypes, handleCommand, queryTypes, handleQuery);
        }

        [TearDown]
        public void Cleanup()
        {
            _httpApiClient = null;

            StopService();
        }

        private static HttpApiClient _httpApiClient;
        private readonly string _rootUrl = "http://localhost:12345";
        private IList<TestCommand> _handledCommands;
        private IDictionary<ITestQuery, object> _handledQueries;

        protected abstract void StartService(string rootUrl, Type[] commandTypes, Action<object> handleCommand, Type[] queryTypes, Func<object, object> handleQuery);

        protected abstract void StopService();


        protected void ExecuteCommand<TCommand>(TCommand command)
        {
            var task = _httpApiClient.ExecuteCommand(command);
            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        protected void ExpectException<T>(Action action, Action<T> asserter)
            where T : Exception
        {
            var thrown = false;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(T), ex.GetType());

                asserter((T) ex);

                thrown = true;
            }

            if (!thrown)
            {
                Assert.Fail("Expected Exception was not thrown");
            }
        }

        protected TResult ExecuteQuery<TResult>(IQuery<TResult> query)
        {
            var task = _httpApiClient.ExecuteQuery(query);

            task.Wait();

            return task.Result;
        }

        public class TestCommand
        {
            public TestCommand()
            {
                Id = Guid.NewGuid();
            }

            public Guid Id { get; set; }
        }

        public class ErrorCommand : TestCommand
        {
        }

        public class IntegerListCommand : TestCommand
        {
            public IList<int> Items;
        }

        protected void CommandShouldBeHandled(TestCommand command)
        {
            Assert.IsNotNull(GetHandledCommand<TestCommand>(command.Id), "CommandShouldBeHandled");
        }

        protected T GetHandledCommand<T>(Guid commandId)
            where T : TestCommand
        {
            return _handledCommands.FirstOrDefault(c => c.Id == commandId) as T;
        }

        protected void NumberOfHandledCommandsShouldBe(int expectedCount)
        {
            Assert.AreEqual(expectedCount, _handledCommands.Count, "NumberOfHandledCommandsShouldBe");
        }

        public interface ITestQuery
        {
            Guid Id { get; set; }
            object FixedResult { get; }
        }

        public class TestQuery<T> : ITestQuery, IQuery<T>
        {
            public TestQuery()
            {
                Id = Guid.NewGuid();
            }

            public T FixedResult { get; set; }

            public Guid Id { get; set; }

            object ITestQuery.FixedResult => FixedResult;
        }

        public class IntQuery : TestQuery<int>
        {
        }

        public class IntegerListQuery : TestQuery<IList<int>>
        {
            public IList<int> Items { get; set; }
        }


        protected void QueryShouldBeHandled(ITestQuery query)
        {
            var handled = _handledQueries.FirstOrDefault(q => q.Key.Id == query.Id);
            Assert.IsNotNull(handled, "QueryShouldBeHandled");
            Assert.AreEqual(handled.Key.FixedResult, handled.Value, "QueryShouldBeHandled: Result");
        }

        protected void NumberOfHandledQueriesShouldBe(int expectedCount)
        {
            Assert.AreEqual(expectedCount, _handledQueries.Count, "NumberOfHandledQueriesShouldBe");
        }

        [Test]
        public void ExecuteCommand_IntegerListCommand_ListItemsAreDeserialized()
        {
            var command = new IntegerListCommand {Items = new List<int> {0, 1, 2}};

            ExecuteCommand(command);

            CommandShouldBeHandled(command);
            NumberOfHandledCommandsShouldBe(1);

            var handledCommand = GetHandledCommand<IntegerListCommand>(command.Id);

            Assert.AreEqual(3, handledCommand.Items.Count);
            Assert.AreEqual(0, handledCommand.Items[0]);
            Assert.AreEqual(1, handledCommand.Items[1]);
            Assert.AreEqual(2, handledCommand.Items[2]);
        }

        [Test]
        public void ExecuteCommand_Simple_InvokesCommandHandler()
        {
            var command = new TestCommand();

            ExecuteCommand(command);

            CommandShouldBeHandled(command);
            NumberOfHandledCommandsShouldBe(1);
        }

        [Test]
        public void ExecuteCommand_ThrowsException()
        {
            ExpectException<HttpApiException>(
                () => ExecuteCommand(new ErrorCommand()),
                ex =>
                {
                    Assert.IsNotNull(ex);
                    Assert.AreEqual(HttpStatusCode.InternalServerError, ex.StatusCode);
                    Assert.AreEqual("ErrorCommand Message", ex.Message);
                });
        }

        [Test]
        public void ExecuteQuery_IntegerListQuery_ItemsAreSerialized()
        {
            var query = new IntegerListQuery
            {
                Items = new List<int> {0, 1, 2},
                FixedResult = new List<int> {3, 4, 5}
            };

            ExecuteQuery(query);

            QueryShouldBeHandled(query);
            NumberOfHandledQueriesShouldBe(1);
        }

        [Test]
        public void ExecuteQuery_Simple_InvokesQueryHandler()
        {
            var query = new IntQuery {FixedResult = 1};

            ExecuteQuery(query);

            QueryShouldBeHandled(query);
            NumberOfHandledQueriesShouldBe(1);
        }
    }
}