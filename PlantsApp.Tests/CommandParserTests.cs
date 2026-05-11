using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlantsApp;

namespace PlantsApp.Tests
{
    [TestClass]
    public class CommandParserTests
    {
        private readonly CommandParser _parser = new CommandParser();

        [TestMethod]
        public void CreateValidTree_ReturnsTreeInstance()
        {
            var parts = new[] { "ADD", "дерево", "Сосна", "50" };
            var plant = _parser.CreatePlant(parts);
            Assert.IsInstanceOfType(plant, typeof(Tree));
            Assert.AreEqual("Сосна", plant.Name);
        }

        [TestMethod]
        public void CreateValidBush_ReturnsBushInstance()
        {
            var parts = new[] { "ADD", "кустарник", "Роза", "Май" };
            var plant = _parser.CreatePlant(parts);
            Assert.IsInstanceOfType(plant, typeof(Bush));
        }

        [TestMethod]
        public void CreateValidCactus_ReturnsCactusInstance()
        {
            var parts = new[] { "ADD", "кактус", "Эхиноцереус", "4,1"};
            var plant = _parser.CreatePlant(parts);
            Assert.IsInstanceOfType(plant, typeof(Cactus));
        }

        [TestMethod]
        public void InsufficientParams_ThrowsArgumentException()
        {
            var parts = new[] { "ADD", "дерево" };
            Assert.Throws<ArgumentException>(() => _parser.CreatePlant(parts));
        }

        [TestMethod]
        public void InvalidAgeFormat_ThrowsFormatException()
        {
            var parts = new[] { "ADD", "дерево", "Клен", "двадцать" };
            Assert.Throws<FormatException>(() => _parser.CreatePlant(parts));
        }

        [TestMethod]
        public void UnknownType_ThrowsArgumentException()
        {
            var parts = new[] { "ADD", "цветок", "ромашка", "лето" };
            Assert.Throws<ArgumentException>(() => _parser.CreatePlant(parts));
        }
    }
}
