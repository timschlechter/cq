using System;
using System.Collections.Generic;
using System.Linq;
using CQ.HttpApi.Owin;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.Owin
{
    public partial class QueryHandlingTests
    {
        public IDictionary<TestQuery, object> HandledQueries { get; set; }

        public class TestQuery
        {
            public TestQuery()
            {
                Id = Guid.NewGuid();
            }

            public Guid Id { get; set; }

            public object FixedResult { get; set; }
        }

        public class IntQuery : TestQuery, IQuery<int>
        {
        }

        protected override void Configure(CQAppBuilderDecorator app)
        {
            HandledQueries = new Dictionary<TestQuery, object>();

            var queryTypes = new[]
            {
                typeof (TestQuery),
                typeof (IntQuery)
            };

            Func<object, object> queryHandler = query =>
            {
                var testQuery = (TestQuery) query;
                HandledQueries.Add(testQuery, testQuery.FixedResult);
                return testQuery.FixedResult;
            };

            app.EnableQueryHandling(queryTypes, queryHandler);
        }

        protected void QueryShouldBeHandled(TestQuery query)
        {
            var handled = HandledQueries.FirstOrDefault(q => q.Key.Id == query.Id);
            Assert.IsNotNull(handled, "QueryShouldBeHandled");
            Assert.AreEqual(handled.Key.FixedResult, handled.Value, "QueryShouldBeHandled: Result");
        }

        protected void NumberOfHandledQueriesShouldBe(int expectedCount)
        {
            Assert.AreEqual(expectedCount, HandledQueries.Count, "NumberOfHandledQueriesShouldBe");
        }
    }
}