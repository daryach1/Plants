using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlantsApp;

namespace PlantsApp.Tests
{
    [TestClass]
    public class PlantComparerTests
    {
        private readonly PlantComparer _comparer = new PlantComparer();

        [TestMethod]
        [DataRow("=", true)]
        [DataRow("!=", false)]
        public void CompareStrings_EqualName_ReturnsExpected(string op, bool expected)
        {
            var plant = new Tree("Дуб", 10);
            Assert.AreEqual(expected, _comparer.Compare(plant, "название", op, "Дуб"));
        }

        [TestMethod]
        [DataRow(">", true)]
        [DataRow("<", false)]
        [DataRow(">=", true)]
        [DataRow("<=", false)]
        public void CompareNumbers_Age_ReturnsExpected(string op, bool expected)
        {
            var plant = new Tree("Ель", 10);
            Assert.AreEqual(expected, _comparer.Compare(plant, "возраст", op, "5"));
        }

        [TestMethod]
        [DataRow(">", "2,0", true)]  
        [DataRow("<", "5,0", true)]   
        [DataRow("=", "4,2", true)]   
        public void CompareNumbers_SpineLength_ReturnsExpected(string op, string compareValue, bool expected)
        {
            var plant = new Cactus("Маммиллярия", 4.2);
            Assert.AreEqual(expected, _comparer.Compare(plant, "длинаколючек", op, compareValue));
        }

        [TestMethod]
        public void Compare_AgeOnBush_ReturnsFalse()
        {
            var bush = new Bush("Барбарис", Month.Апрель);
            Assert.IsFalse(_comparer.Compare(bush, "возраст", ">", "1"));
        }

        [TestMethod]
        public void Compare_UnknownField_ReturnsFalse()
        {
            var plant = new Tree("Ясень", 20);
            Assert.IsFalse(_comparer.Compare(plant, "цвет", "=", "зеленый"));
        }
    }
}
