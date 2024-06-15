using ClassLibrary10lab;
namespace lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary = new MySortedDictionary<int, MyList<MusicalInstrument>>();
            FillSortedDictionary(sortedDictionary);

            do
            {
                Console.WriteLine("1. Показать музыкальные группы");
                Console.WriteLine("2. Из всех групп выбрать гитары");
                Console.WriteLine("3. Найти группу с максимальным количеством инструментов");
                Console.WriteLine("4. Вывести количество инструментов каждого типа");
                Console.WriteLine("5. Объединение первой и последней группы");
                Console.WriteLine("6. Соединение участников с инструментами");

                int action = IsInt(1, 6);
                switch (action)
                {
                    case 1:
                        ShowSortedDictionary(sortedDictionary);
                        break;
                    case 2:
                        ShowGuitarExt(sortedDictionary);
                        ShowGuitarLinq(sortedDictionary);
                        break;
                    case 3:
                        FindMaxInstrumentsCountExt(sortedDictionary);
                        FindMaxInstrumentsCountLinq(sortedDictionary);
                        break;
                    case 4:
                        GroupByBrandExt(sortedDictionary);
                        GroupByBrandLinq(sortedDictionary);
                        break;
                    case 5:
                        UnionFirstLastExt(sortedDictionary);
                        UnionFirstLastLinq(sortedDictionary);
                        break;
                    case 6:
                        List<Participants> participants = new List<Participants>();
                        FillListParticipants(participants);
                        ShowParticipants(participants);
                        JoinParticipantsAndInstrumentsExt(participants, sortedDictionary);
                        JoinParticipantsAndInstrumentsLinq(participants, sortedDictionary);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте еще раз.");
                        break;
                }

            } while (true);
        }

        //Функция заполнения SortedDictionary
        static void FillSortedDictionary(MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            Random random = new Random();
            int count = random.Next(5, 10);

            for (int i = 0; i < count; i++)
            {
                int countInstruments = random.Next(3, 10);
                MyList<MusicalInstrument> instruments = new MyList<MusicalInstrument>();
                for (int j = 0; j < countInstruments; j++)
                {
                    int choice = random.Next(1, 4);
                    MusicalInstrument instrument;
                    if (choice == 1)
                        instrument = new Guitar();
                    else if (choice == 2)
                        instrument = new ElectricGuitar();
                    else
                        instrument = new Piano();

                    instrument.RandomInit();
                    instruments.AddToBegin(instrument);
                }
                sortedDictionary.Add(i + 1, instruments);
            }
        }

        //Функция для просмотра SortedDictionary
        static void ShowSortedDictionary(MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            foreach (var members in sortedDictionary)
            {
                Console.WriteLine("Муз. группа №" + members.Key);
                foreach (var instrument in members.Value)
                {
                    instrument.Show();
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        //Функция использования метода расширения
        static void ShowGuitarExt(MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            Console.WriteLine("Метод расширения");
            var result = sortedDictionary.SelectMany(t => t.Value).Where(t => t is Guitar);
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
        }

        //Функция использования LINQ запроса (выборка всех гитар)
        static void ShowGuitarLinq(MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            Console.WriteLine("LINQ");
            var result = from kvp in sortedDictionary
                         from instrument in kvp.Value
                         where instrument is Guitar
                         select instrument;
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
        }

        //Функция нахождения максимального кол-ва инструментов
        static void FindMaxInstrumentsCountExt(MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            // Находим максимальное количество инструментов среди всех списков в словаре
            int maxCount = sortedDictionary.Values.Max(list => list.Count);

            // Находим первую пару ключ-значение, у которой количество инструментов равно максимальному
            var maxKeyValuePair = sortedDictionary.First(kvp => kvp.Value.Count == maxCount);

            // Находим индекс этой пары ключ-значение в списке словаря
            int index = sortedDictionary.ToList().IndexOf(maxKeyValuePair);

            // Выводим информацию о максимальном количестве инструментов и номере группы (индекс + 1)
            Console.WriteLine($"Максимальное количество инструментов = {maxCount}, номер группы = {index + 1}");
        }

        //Функция нахождения максимального кол-ва инструментов LINQ
        static void FindMaxInstrumentsCountLinq(MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            var maxKeyValuePair = sortedDictionary.OrderByDescending(kvp => kvp.Value.Count).First();
            int index = sortedDictionary.ToList().IndexOf(maxKeyValuePair);

            Console.WriteLine($"Максимальное количество инструментов = {maxKeyValuePair.Value.Count}, номер группы = {index + 1}");
        }

        //Группировка данных
        static void GroupByBrandExt(MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            var result = sortedDictionary.SelectMany(kvp => kvp.Value).GroupBy(instrument => instrument.Name);
            foreach (var group in result)
            {
                Console.WriteLine($"Модель - {group.Key}, Кол-во - {group.Count()}");
            }
        }

        //Группировка данных LINQ
        static void GroupByBrandLinq(MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            var result = from kvp in sortedDictionary
                         from instrument in kvp.Value
                         group instrument by instrument.Name;
            foreach (var group in result)
            {
                Console.WriteLine($"Модель - {group.Key}, Кол-во - {group.Count()}");
            }
        }

        static void UnionFirstLastExt(MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            var result = sortedDictionary.First().Value.Union(sortedDictionary.Last().Value);
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void UnionFirstLastLinq(MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            var result = (from instrument in sortedDictionary.First().Value select instrument)
                         .Union(from instrument in sortedDictionary.Last().Value select instrument);
            foreach (var item in result)
            {
                item.Show();
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void FillListParticipants(List<Participants> participants)
        {
            Random random = new Random();
            int count = random.Next(5, 10);
            for (int i = 0; i < count; i++)
            {
                Participants participant = new Participants();
                participant.RandomInit();

                participants.Add(participant);
            }
        }

        static void ShowParticipants(List<Participants> participants)
        {
            foreach (var participant in participants)
            {
                Console.WriteLine(participant.ToString());
            }
        }

        static void JoinParticipantsAndInstrumentsExt(List<Participants> participants, MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            var res = from participant in participants
                      from kvp in sortedDictionary
                      where kvp.Key == ((participant.Age % sortedDictionary.Count) + 1) // Используем остаток от деления возраста на количество групп для получения номера группы
                      select new { Participants = participant, Instruments = kvp.Value };

            foreach (var item in res)
            {
                Console.WriteLine($"Участник: {item.Participants}");
                foreach (var instrument in item.Instruments)
                {
                    Console.WriteLine($"Инструмент: {instrument}");
                }
            }
            Console.WriteLine();
        }

        static void JoinParticipantsAndInstrumentsLinq(List<Participants> participants, MySortedDictionary<int, MyList<MusicalInstrument>> sortedDictionary)
        {
            var res = participants.SelectMany(participant =>
                      sortedDictionary.Where(kvp => kvp.Key == ((participant.Age % sortedDictionary.Count) + 1)) // Используем остаток от деления возраста на количество групп для получения номера группы
                      .SelectMany(kvp => kvp.Value)
                      .Select(instrument => new { Participants = participant, Instrument = instrument }));

            foreach (var item in res)
            {
                Console.WriteLine($"Участник: {item.Participants}, Инструмент: {item.Instrument}");
            }
            Console.WriteLine();
        }

        
        static int IsInt(int min, int max)
        {
            bool isConvert;
            int number;
            do
            {
                string buf = Console.ReadLine();
                isConvert = int.TryParse(buf, out number);
                if (!isConvert || number < min || number > max)
                {
                    Console.WriteLine($"Неправильно введено число. Введите значение от {min} до {max}");
                }
            } while (!isConvert || number < min || number > max);
            return number;
        }

    }
}
