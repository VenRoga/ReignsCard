using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Shapes
{
    class Program
    {
        static void Main(string[] args)
        {
           Shape[] myShapes = new Shape[] {
                new Circle{ petName = "Henry", color = "Blue", number = 12},
                new Circle{ petName = "Dan", color = "Red", number = 16},
                new Circle{ petName = "Mary", color = "Black", number = 22},
                new Hexagon{ petName = "Colly", color = "Red", number = 52},
                new Hexagon{ petName = "Hank", color = "Blue", number = 76},
                new Hexagon{ petName = "Sven", color = "White", number = 43},
                new Circle{ petName = "Mary", color = "Blue", number = 31},
                new Circle{ petName = "Dan", color = "Blue", number = 64},
                new Hexagon{ petName = "Colly", color = "Blue", number = 1},
                new Hexagon{ petName = "Hank", color = "Black", number = 7},
                new Hexagon{ petName = "Sven", color = "Red", number = 68},
                new Circle{ petName = "Melvin", color = "Red", number = 55}
            };
            // создайте запросы LINQ. Результаты выполнения вывести на экран.
            // 1) все фигуры красного цвета
            var redShapes = from f in myShapes where f.color.CompareTo("Red")==0 select f;
            // 2) количество фигур с именем Colly
            var q = (from f in myShapes where f.petName.CompareTo("Colly") == 0 select f).Count();
            // 3) фигуры синего цвета упорядоченные по убыванию
            var qq = (from f in myShapes where f.color.CompareTo("Blue") == 0 orderby f.number descending select f);
            // 4) только красные фигуры с number >53 
            var qqq = (from f in myShapes where f.color.CompareTo("Red") == 0 && f.number > 53 select f);
            // 5) только окружности(!) с number >20, но < 60 упорядоченные по petName
            var qqqq = (from f in myShapes where f is Circle && f.number > 20 && f.number < 60 orderby f.petName select f);
            // 6) создайте метод, который возвращает массив шестиугольников(!), упорядоченных по number
            Res(myShapes);
            // 7) определить сумму номеров фигур синего цвета(только LINQ)
            var qqqqq = (from f in myShapes where f.color.CompareTo("Blue") == 0 select (int)f.number).Sum();
            Console.WriteLine(q);
            Console.WriteLine("+++++++++++++++++++++++");
            Pr(qq);
            Pr(qqq);
            Pr(qqq);
            Pr(qqqq);
            Console.WriteLine(qqqqq);
            Console.WriteLine("+++++++++++++++++++++++");
            // ответы писать после каждого задания
            // ниже будут запросы по двум массивам
            Shape[] arShapes = new Shape[] {
                new Circle{ petName = "Dan", color = "Вlack", number = 47},
                new Hexagon{ petName = "Havk", color = "Blue", number = 76},
                new Hexagon{ petName = "Svintus", color = "Orange", number = 32},
                new Circle{ petName = "Dad", color = "Magenta", number = 41},
                new Hexagon{ petName = "Corvin", color = "Grey", number = 11},
                new Hexagon{ petName = "Tom", color = "Green", number = 71},
                new Hexagon{ petName = "Sarah", color = "Yellow", number = 62},
                new Circle{ petName = "Mervin", color = "Red", number = 59}
            };
            // 8) получить из двух массивов множество всех возможных уникальных названий
            // цветов, упорядоченных по алфавиту.
            var w = myShapes.Select(f => f.color).Union(arShapes.Select(f => f.color)).OrderBy(c => c);
            foreach (var qweqw in w) Console.WriteLine(qweqw); Console.WriteLine("+++++++++++++++++++++++");
            // 9) получить объединение фигур из двух массивов и из него выбрать те,
            // у которых номер >40
            var ww = (from qww in myShapes where qww.number > 40 select qww).Concat(from qww in arShapes where qww.number > 40 select qww);
            Pr(ww);
            // 10) найти пересечение названий цветов в двух массивах
            var www = (from qww in myShapes select qww.color).Intersect(from qww in arShapes where qww.number > 40 select qww.color);
            foreach (var qweqw in www) Console.WriteLine(qweqw); Console.WriteLine("+++++++++++++++++++++++");
            Console.ReadLine();
        }
        public static Hexagon[] Res(Shape[] q)
        {
            var qq = q.OfType<Hexagon>().OrderBy(f => f.number).ToArray();
            return qq;
        }

        public static void Pr(IEnumerable<Shape> q)
        {
            foreach(var qw in q) Console.WriteLine(qw);
            Console.WriteLine("+++++++++++++++++++++++");
        }
    }
}
