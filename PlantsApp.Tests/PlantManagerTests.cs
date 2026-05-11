using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlantsApp;
using System.Reflection;
using System.Collections;

namespace PlantsApp.Tests
{
    [TestClass]
    public class PlantManagerTests
    {
        private readonly PlantManager _manager = new PlantManager();

        [TestMethod]
        public void AddPlant_IncreasesCount()
        {
            _manager.AddPlant(new Tree("Береза", 5));
            Assert.AreEqual(1, GetPlantsCount());
        }

        [TestMethod]
        public void RemovePlant_ByCondition_RemoveCorrectly()
        {
            _manager.AddPlant(new Tree("Дуб", 10));
            _manager.AddPlant(new Tree("Ель", 3));
            _manager.AddPlant(new Cactus("Алоэ", 2.0));

            _manager.RemovePlant("возраст", ">", "5");
            Assert.AreEqual(2, GetPlantsCount());
        }

        [TestMethod]
        public void RemovePlant_NoMatches_DoesNotThrow()
        {
            _manager.AddPlant(new Tree("Сосна", 2));
            _manager.RemovePlant("возраст", ">", "100");
            Assert.AreEqual(1, GetPlantsCount());
        }

        [TestMethod]
        public void RemovePlant_InvalidField_DoesNotThrow()
        {
            _manager.AddPlant(new Bush("Гортензия", Month.Июнь));
            _manager.RemovePlant("несуществующее_поле", "=", "тест");
            Assert.AreEqual(1, GetPlantsCount());
        }
        private int GetPlantsCount()
        {
            var field = typeof(PlantManager).GetField("_plants", BindingFlags.NonPublic | BindingFlags.Instance);
            var list = field?.GetValue(_manager) as IList;
            return list?.Count ?? 0;
        }
    }
}
