using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantsApp
{
    public enum Month
    {
        Январь, Февраль, Март, Апрель, Май, Июнь, Июль, Август, Сентябрь, Октябрь, Ноябрь, Декабрь
    }

    public interface IPlant
    {
        string GetInfo();
    }

    abstract class Plant : IPlant
    {
        public string Name { get; }
        protected Plant(string name)
        {
            Name = string.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException("Название растения не может быть пустым", nameof(name))
            : name;
        }
        public abstract string GetInfo();
    }

    class Tree : Plant
    {
        public long Age { get; }
        public Tree(string name, long age): base(name)
        {
            Age = age < 0
                ? throw new ArgumentOutOfRangeException(nameof(age), "Возраст дерева не может быть отрицательным")
                : age;
        }
        public override string GetInfo()
        {
            return $"[Дерево] Название: {Name}, возраст: {Age}";
        }
    }
    
    class Bush : Plant
    {
        public Month FloweringMonth { get; set; }
        public Bush(string name, Month month): base(name)
        {
            FloweringMonth = month;
        }

        public override string GetInfo()
        {
            return $"[Кустарник] Название: {Name}, месяц цветения: {FloweringMonth}";
        }

    }

    class Cactus : Plant
    {
        public double SpineLength { get; set; }
        public Cactus(string name, double spineLength): base(name)
        {
            SpineLength = spineLength < 0
                ? throw new ArgumentOutOfRangeException(nameof(spineLength), "Длина колючек не может быть отрицательной")
                : spineLength;
        }

        public override string GetInfo()
        {
            return $"[Кактус] Название: {Name}, Длина колючек: {SpineLength}";
        }
    }
}
