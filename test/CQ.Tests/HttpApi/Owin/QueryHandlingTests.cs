using System;
using System.Collections.Generic;
using System.Linq;
using CQ.HttpApi.Owin;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.Owin
{
    [TestFixture]
    public class QueryHandlingTests : OwinBaseTests
    {
        private IDictionary<ITestQuery, object> _handledQueries;


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

        protected override void Configure(OwinConfig cfg)
        {
            _handledQueries = new Dictionary<ITestQuery, object>();

            var queryTypes = new[]
            {
                typeof (IntQuery),
                typeof (IntegerListQuery)
            };

            Func<object, object> queryHandler = query =>
            {
                var testQuery = (ITestQuery) query;
                _handledQueries.Add(testQuery, testQuery.FixedResult);
                return testQuery.FixedResult;
            };

            cfg.EnableQueryHandling(queryTypes, queryHandler);
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
        public void ExecuteQuery_IntegerListQuery_ItemsAreSerialized()
        {
            var query = new IntegerListQuery
            {
                Items = new List<int> {0, 1, 2},
                FixedResult = new List<int> {3, 4, 5}
            };

            ExecuteQuery<IntegerListQuery, IList<int>>(query);

            QueryShouldBeHandled(query);
            NumberOfHandledQueriesShouldBe(1);
        }

        [Test]
        public void ExecuteQuery_Simple_InvokesQueryHandler()
        {
            var query = new IntQuery {FixedResult = 1};

            ExecuteQuery<IntQuery, int>(query);

            QueryShouldBeHandled(query);
            NumberOfHandledQueriesShouldBe(1);
        }
    }
}