using System;
using System.Collections.Generic;
using System.Linq;
using CQ.HttpApi.Owin;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.Owin
{
    public partial class CommandHandlingTests
    {
        public IList<TestCommand> HandledCommands { get; set; }

        public class TestCommand
        {
            public TestCommand()
            {
                Id = Guid.NewGuid();
            }

            public Guid Id { get; set; }
        }

        protected override void Configure(CQAppBuilder app)
        {
            HandledCommands = new List<TestCommand>();

            var knownCommandTypes = new[]
            {
                typeof (TestCommand)
            };

            Action<object> commandHandler = command => { HandledCommands.Add(command as TestCommand); };

            app.EnableCommandHandling(knownCommandTypes, commandHandler);
        }

        protected void CommandShouldBeHandled(TestCommand command)
        {
            Assert.IsNotNull(HandledCommands.FirstOrDefault(c => c.Id == command.Id), "CommandShouldBeHandled");
        }

        protected void NumberOfHandledCommandsShouldBe(int expectedCount)
        {
            Assert.AreEqual(expectedCount, HandledCommands.Count, "NumberOfHandledCommandsShouldBe");
        }
    }
}