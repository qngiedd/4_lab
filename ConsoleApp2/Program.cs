using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lab4
{
    public class Set<T> : IEnumerable<T> //для того чтобы обратиться к объекту класса в цикле foreach,
                                         //необходимо реализовать интерфейсы IEnumerator и IEnumerable в их обобщенной или необобщенной форме
    {
        public List<T> _items = new List<T>();//коллекция хранимых данных.
                                              //T - тип данных, хранимых во множестве
        public Owner owner;
        public Date myDate;
        public class Owner
        {
            public string name;
            public string organisation;

            public Owner(string name, string organisation)
            {
                this.name = name;
                this.organisation = organisation;
            }
        }
        public Set()
        {
            owner = new Owner("Angelina Draguts", "BSTU");
            myDate = new Date();
        }
        public class Date
        {
            public DateTime time;
            public Date()
            {
                time = DateTime.Now;
            }
        }
        public int Count //подсчёт
        {
            get
            {
                return _items.Count;
            }
        }
        public void Add(T item)  //ДОБАВЛЕНИЕ ДАННЫХ ВО МНОЖЕСТВО
        {
            //проверяем входные данные на пустоту, генерируем исключения вручную
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _items.Add(item);
        }
        public static Set<T> operator +(Set<T> set1, Set<T> set2) //ОБЪЕДИНЕНИЕ МНОЖЕСТВ
        {
            //проверяем входные данные на пустоту
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            var resultSet = new Set<T>(); //результирующее множество

            var items = new List<T>(); //элементы данных результирующего множества.

            if (set1._items != null && set1._items.Count > 0)
            {
                items.AddRange(set1._items); //AddRange создает для каждого значения новый элемент в коллекции
            }

            if (set2._items != null && set2._items.Count > 0)
            {
                items.AddRange(set2._items);
            }
            // Удаляем все дубликаты из результирующего множества элементов данных.
            //Операция ToList создает List типа T
            resultSet._items = items.Distinct().ToList(); //Distinct - удаляет дублированные элементы из последовательности
            return resultSet;
        }

        public static Set<T> operator -(Set<T> set1, Set<T> set2) // ПЕРЕСЕЧЕНИЕ МНОЖЕСТВ
        {
            // Проверяем входные данные на пустоту.
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }

            // Результирующее множество.
            var resultSet = new Set<T>();

            // Выбираем множество содержащее наименьшее количество элементов.
            if (set1.Count < set2.Count)
            {
                foreach (var item in set1._items)
                {
                    if (set2._items.Contains(item)) //Contains - определяет, есть ли элемент в List <T>.
                    {
                        resultSet.Add(item);
                    }
                }
            }
            else
            {
                foreach (var item in set2._items)
                {
                    if (set1._items.Contains(item))
                    {
                        resultSet.Add(item);
                    }
                }
            }
            return resultSet; //возвращаем результирующее множество
        }
        public static bool operator *(Set<T> set1, Set<T> set2)  //ПОДМНОЖЕСТВО
        {
            if (set1 == null)
            {
                throw new ArgumentNullException(nameof(set1));
            }

            if (set2 == null)
            {
                throw new ArgumentNullException(nameof(set2));
            }
            // Если все элементы первого множества содержатся во втором, то возвращаем истину, иначе ложь.
            if (set1._items.All(s => set2._items.Contains(s))) return true; //В лямбда-выражениях лямбда-оператор => отделяет входные параметры с левой стороны от тела лямбда с правой стороны.
            else return false;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() //возвращает перечислитель, который выполняет итерацию по коллекции.
        {
            return _items.GetEnumerator();
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Set<string> a = new Set<string>();
            Console.WriteLine("владелец: " + a.owner.name + "; организация: " + a.owner.organisation + "; дата и время создания: " + a.myDate.time);
            Console.WriteLine();
            // Создаем множества.
            var set1 = new Set<int>()
            {
            1, 2, 3, 4, 5, 6
            };

            var set2 = new Set<int>()
            {
            4, 5, 6, 7, 8, 9
            };

            var set3 = new Set<int>()
            {
            2, 3, 4
            };
            var set4 = new Set<int>()
            {
            2, 3, 4, 3, 4, 1, 8, 5
            };

            // Выполняем операции со множествами.
            Set<int> union = set1 + set2;
            Set<int> intersection = set1 - set2;
            bool subset1 = set3 * set1;
            bool subset2 = set3 * set2;

            // Выводим исходные множества на консоль.
            PrintSet(set1, "Первое множество: ");
            PrintSet(set2, "Второе множество: ");
            PrintSet(set3, "Третье множество: ");
            PrintSet(set4, "Четвертое множество: ");

            Console.WriteLine();
            // Выводим результирующие множества на консоль.
            PrintSet(union, "Объединение первого и второго множества: ");
            PrintSet(intersection, "Пересечение первого и второго множества: ");

            if (subset1)
            {
                Console.WriteLine("Третье множество является подмножеством первого.");
            }
            else
            {
                Console.WriteLine("Третье множество не является подмножеством первого.");
            }

            if (subset2)
            {
                Console.WriteLine("Третье множество является подмножеством второго.");
            }
            else
            {
                Console.WriteLine("Третье множество не является подмножеством второго.");
            }

            Console.WriteLine();
            string testString = "TEST TEST TEST TEST TEST";
            Console.WriteLine(testString);
            testString.Com(", ");

            Console.WriteLine("Максимальный элемент set4= " + MathOperation.GetMaxElement(set4));
            Console.WriteLine("Минимальный элемент set4= " + MathOperation.GetMinElement(set4));
            Console.WriteLine("(Max-Min)= " + MathOperation.Raz(set4));
            Console.WriteLine("Сумма элементов set4= " + MathOperation.Sum(set4));
            Console.Write("Множество set4 без повторов: "); MathOperation.Povtor(set4);

            Console.ReadKey();
        }
        private static void PrintSet(Set<int> set, string title)
        {

            Console.Write(title);
            foreach (var item in set)
            {
                Console.Write($"{item} ");
            }
            Console.Write($" ||| Мощность множества: {set.Count}");
            Console.WriteLine();
        }
    }
    static class MathOperation
    {
        public static string Com(this string str, string a) //метод расширения для добавления запятой после каждого слова
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ') { Console.Write(a); }
                else { Console.Write(str[i]); }
            }
            Console.WriteLine();
            return str;
        }

        public static void Povtor(this Set<int> set) //метод расширения для удаления дубликатов из множества
        {
            int[] temp = new int[set.Count];
            int i = 0;
            foreach (int item in set._items)
            {
                temp[i] = item; i++;
            }
            int[] a = temp.Distinct().ToArray();
            for (i = 0; i < a.Length; i++)
            {
                Console.Write(a[i] + " ");
            }
        }
        public static int GetMaxElement(Set<int> set)
        {
            int[] temp = new int[set.Count];
            int i = 0;
            foreach (int item in set._items)
            {
                temp[i] = item; i++;
            }

            return temp.Max();
        }

        public static int GetMinElement(Set<int> set)
        {
            int[] temp = new int[set.Count];
            int i = 0;
            foreach (int item in set._items)
            {
                temp[i] = item; i++;
            }

            return temp.Min();
        }

        public static int Sum(Set<int> set)
        {
            int Sum = 0;
            foreach (int item in set._items)
            {
                Sum = Sum + item;
            }
            return Sum;
        }

        public static int Raz(Set<int> set)
        {
            int Raz, max, min;
            int[] temp = new int[set.Count];
            int i = 0;
            foreach (int item in set._items)
            {
                temp[i] = item; i++;
            }
            min = temp.Min();
            max = temp.Max();
            Raz = max - min;
            return Raz;
        }
        
    }
}