using System;
using System.Collections.Generic;
using System.Linq;
using CQ.HttpApi.Owin;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.Owin
{
    public partial class CommandHandlingTests
    {
        private IList<TestCommand> _handledCommands;

        public class TestCommand
        {
            public TestCommand()
            {
                Id = Guid.NewGuid();
            }

            public Guid Id { get; set; }
        }

        protected override void Configure(OwinConfig cfg)
        {
            var commandTypes = new[]
            {
                typeof (TestCommand)
            };
            
            _handledCommands = new List<TestCommand>();
            Action<object> commandHandler = command =>
            {
                _handledCommands.Add(command as TestCommand);
            };

            cfg.EnableCommandHandling(commandTypes, commandHandler);
        }

        protected void CommandShouldBeHandled(TestCommand command)
        {
            Assert.IsNotNull(_handledCommands.FirstOrDefault(c => c.Id == command.Id), "CommandShouldBeHandled");
        }

        protected void NumberOfHandledCommandsShouldBe(int expectedCount)
        {
            Assert.AreEqual(expectedCount, _handledCommands.Count, "NumberOfHandledCommandsShouldBe");
        }
    }
}