using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CQ.HttpApi.Tests.HttpApi
{
    [TestFixture]
    public class ObjectHelperTests
    {
        [Test]
        public void Expand()
        {
            var expanded = ObjectHelper.Expand(new Dictionary<string, object>
            {
                {"Value", 0},
                {"Level1.Value", 1},
                {"Level1.Level2.Value", 2}
            });

            Assert.AreEqual(0, expanded.Value);
            Assert.AreEqual(1, expanded.Level1.Value);
            Assert.AreEqual(2, expanded.Level1.Level2.Value);
        }

        [Test]
        public void Flatten()
        {
            var flattened = ObjectHelper.Flatten(new
            {
                Value = 0,
                Level1 = new
                {
                    Value = 1,
                    Level2 = new
                    {
                        Value = 2
                    }
                }
            });

            Assert.AreEqual(0, flattened["Value"].Single());
            Assert.AreEqual(1, flattened["Level1.Value"].Single());
            Assert.AreEqual(2, flattened["Level1.Level2.Value"].Single());
        }

        [Test]
        public void Flatten_IntegerArrayProperty()
        {
            var flattened = ObjectHelper.Flatten(new
            {
                Items = new[] {0, 1, 2}
            });

            var items = flattened["Items[]"].ToArray();
            Assert.AreEqual(0, items[0]);
            Assert.AreEqual(1, items[1]);
            Assert.AreEqual(2, items[2]);
        }
    }
}