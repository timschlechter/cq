using System;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.Owin
{
    [TestFixture]
    public partial class QueryHandlingTests : OwinBaseTests
    {
        [Test]
        public void ExecuteQuery_Simple_InvokesQueryHandler()
        {
            var query = new IntQuery {Id = Guid.NewGuid(), FixedResult = 1};

            ExecuteQuery<IntQuery, int>(query);

            QueryShouldBeHandled(query);
            NumberOfHandledQueriesShouldBe(1);
        }
    }
}