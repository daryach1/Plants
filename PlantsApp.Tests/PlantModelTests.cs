using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlantsApp;
using System;

namespace PlantsApp.Tests
{
    [TestClass]
    public sealed class PlantModelTests
    {
        [TestMethod]
        public void CreateValidTree_InitializesCorrectly()
        {
            var tree = new Tree("Дуб", 15);
            Assert.AreEqual("Дуб", tree.Name);
            Assert.AreEqual(15, tree.Age);
            StringAssert.Contains(tree.GetInfo(), "[Дерево]");

        }

        [TestMethod]
        public void CreateValidBush_InitializesCorrectly()
        {
            var bush = new Bush("Сирень", Month.Май);
            Assert.AreEqual("Сирень", bush.Name);
            Assert.AreEqual(Month.Май, bush.FloweringMonth);
        }

        [TestMethod]
        public void CreateValidCactus_InitializesCorrectly()
        {
            var cactus = new Cactus("Алоэ", 2.5);
            Assert.AreEqual("Алоэ", cactus.Name);
            Assert.AreEqual(2.5, cactus.SpineLength);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void InvalidName_ThrowsArgumentException(string name)
        {
            Assert.Throws<ArgumentException>(() => new Tree(name, 10));
        }

        [TestMethod]
        public void NegativeAge_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Tree("Береза", -5));
        }

        [TestMethod]
        public void NegativeSpineLength_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Cactus("Опунция", -1.0));
        }
    }
}
