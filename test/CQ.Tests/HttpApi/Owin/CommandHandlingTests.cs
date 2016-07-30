using System;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.Owin
{
    [TestFixture]
    public partial class CommandHandlingTests : OwinBaseTests
    {
        [Test]
        public void ExecuteCommand_Simple_InvokesCommandHandler()
        {
            var command = new TestCommand {Id = Guid.NewGuid()};

            ExecuteCommand(command);

            CommandShouldBeHandled(command);
            NumberOfHandledCommandsShouldBe(1);
        }
    }
}