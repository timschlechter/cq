using System;
using System.Collections.Generic;
using System.Linq;
using CQ.HttpApi.Owin;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.Owin
{
    public partial class QueryHandlingTests
    {
        private IDictionary<TestQuery, object> _handledQueries;

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

        protected override void Configure(OwinConfig cfg)
        {
            _handledQueries = new Dictionary<TestQuery, object>();

            var queryTypes = new[]
            {
                typeof (TestQuery),
                typeof (IntQuery)
            };

            Func<object, object> queryHandler = query =>
            {
                var testQuery = (TestQuery) query;
                _handledQueries.Add(testQuery, testQuery.FixedResult);
                return testQuery.FixedResult;
            };

            cfg.EnableQueryHandling(queryTypes, queryHandler);
        }

        protected void QueryShouldBeHandled(TestQuery query)
        {
            var handled = _handledQueries.FirstOrDefault(q => q.Key.Id == query.Id);
            Assert.IsNotNull(handled, "QueryShouldBeHandled");
            Assert.AreEqual(handled.Key.FixedResult, handled.Value, "QueryShouldBeHandled: Result");
        }

        protected void NumberOfHandledQueriesShouldBe(int expectedCount)
        {
            Assert.AreEqual(expectedCount, _handledQueries.Count, "NumberOfHandledQueriesShouldBe");
        }
    }
}