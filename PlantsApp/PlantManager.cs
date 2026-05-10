using System;
using System.Collections.Generic;

namespace PlantsApp
{
    public class PlantManager
    {
        private List<IPlant> _plants = new List<IPlant>();
        private IComparer _comparer;

        public PlantManager()
        {
            _comparer = new PlantComparer();
        }

        public void AddPlant (IPlant plant) 
        { _plants.Add(plant); }
        public void RemovePlant(string field, string op, string value)
        { _plants.RemoveAll(plant => _comparer.Compare(plant, field, op, value)); }

        public void PrintPlants()
        {
            Console.WriteLine("------Текущее состояние контейнера:------");
            if (_plants.Count == 0)
            {
                Console.WriteLine("Нет растений");
                return;
            }

            foreach (var plant in _plants)
            {
                Console.WriteLine(plant.GetInfo());
            }
            Console.WriteLine("-----------------------------------------");
        }
    }
}
