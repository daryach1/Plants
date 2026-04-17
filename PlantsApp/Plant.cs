using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantsApp
{
    enum Month
    {
        Январь, Февраль, Март, Апрель, Май, Июнь, Июль, Август, Сентябрь, Октябрь, Ноябрь, Декабрь
    }
    abstract class Plant
    {
        public string Name { get; set; }
        public Plant(string name)
        {
            Name = name;
        }
        public abstract string GetInfo();
    }

    class Tree : Plant
    {
        public long Age { get; set; }
        public Tree(string name, long age): base(name)
        {
            Age = age;
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
            SpineLength = spineLength;
        }

        public override string GetInfo()
        {
            return $"[Кактус] Название: {Name}, Длина колючек: {SpineLength}";
        }
    }
}
