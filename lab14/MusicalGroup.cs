using ClassLibrary10lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14
{
    public class MusicalGroup : IInit, IComparable, ICloneable
    {
        public string Name { get; set; }
        public string Number { get; set; }

        public MusicalGroup()
        {
            // Пустой конструктор без параметров
        }

        public MusicalGroup(string name, string number)
        {
            Name = name;
            Number = number;
        }

        static string[] Names = { "Король и шут", "Кузнечики", "ВИАГРА" };
        protected static Random rnd = new Random();

        public virtual void Show()
        {
            Console.WriteLine($"Музыкальная группа: {Name} {Number}");
        }

        public virtual void Init()
        {
            Console.WriteLine("Введите название музыкальной группы:");
            Name = Console.ReadLine();
            Console.WriteLine("Введите номер группы:");
            Number = Console.ReadLine();
        }

        public virtual void RandomInit()
        {
            string[] names = { "Король и шут", "Кузнечики", "ВИАГРА" };
            Name = names[rnd.Next(Names.Length)];
            string[] numbers = { "1", "2", "3" };
            Number = numbers[rnd.Next(Names.Length)];
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}
