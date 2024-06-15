using ClassLibrary10lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab14
{
    public class Participants : IInit
    {
        private string name;
        private int age;
        private static int countInstance = 0;
        private static Random random = new Random();

        public Participants()
        {
            name = "No Name";
            age = 0;
            countInstance++;
        }

        public Participants(string name, int age, double gpa)
        {
            Name = name;
            Age = age;
            countInstance++;
        }

        public Participants(Participants other)
        {
            Name = other.name;
            Age = other.age;
            countInstance++;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public static int GetCountInstance()
        {
            return countInstance;
        }

        public void Init()
        {
            Console.WriteLine("Введите имя");
            name = Console.ReadLine();

            Console.WriteLine("Введите возраст");
            age = int.Parse(Console.ReadLine());

            
        }

        public void RandomInit()
        {
            string[] names = { "Иван", "Олег", "Михаил", "Игорь", "Анна", "Мария" };

            name = names[random.Next(names.Length)];
            age = random.Next(10, 40);

        }

    }
}
