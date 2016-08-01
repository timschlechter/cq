using System;
using System.Collections.Generic;
using System.Linq;
using CQ.HttpApi.Owin;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi.Owin
{
    [TestFixture]
    public class CommandHandlingTests : OwinBaseTests
    {
        #region Helpers

        private IList<TestCommand> _handledCommands;

        public class TestCommand
        {
            public TestCommand()
            {
                Id = Guid.NewGuid();
            }

            public Guid Id { get; private set; }
        }

        public class IntegerListCommand : TestCommand
        {
            public IList<int> Items;
        }

        protected override void Configure(OwinConfig cfg)
        {
            var commandTypes = new[]
            {
                typeof (TestCommand),
                typeof (IntegerListCommand)
            };

            _handledCommands = new List<TestCommand>();
            Action<object> commandHandler = command => { _handledCommands.Add(command as TestCommand); };

            cfg.EnableCommandHandling(commandTypes, commandHandler);
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
        #endregion

        [Test]
        public void ExecuteCommand_Simple_InvokesCommandHandler()
        {
            var command = new TestCommand();

            ExecuteCommand(command);

            CommandShouldBeHandled(command);
            NumberOfHandledCommandsShouldBe(1);
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
    }
}