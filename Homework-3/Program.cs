using System;
using System.Collections.Generic;
using System.Threading;

namespace HomeWork3
{
    class Point
    {
        // Maximum Y of all points
        public static int MaxY = 0;

        private int _x, _y;
        public char Sign;
        public bool IsShow;
        public ConsoleColor Color;
        
        public int X
        {
            get => _x;
            set
            {
                if (value < 0)
                    throw new Exception("X should be positive");

                _x = value;
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                if (value < 0)
                    throw new Exception("Y should be positive");

                _y = value;
            }
        }


        public Point() : this(0, 0)
        {
        }


        public Point(int xy) : this(xy, xy)
        {
        }

        public Point(int x, int y, char sign = '*', ConsoleColor color = ConsoleColor.White)
        {
            X = x;
            Y = y;
            Sign = sign;
            IsShow = false;
            Color = color;
        }

        public void Show()
        {
            if (!IsShow)
            {
                IsShow = true;
                Program.WriteAt(_x, _y, Sign, Color);
            }
        }

        
        public void Hide()
        {
            if (IsShow)
            {
                IsShow = false;
                Program.WriteAt(_x, _y, ' ', Color);
            }
        }

        public static void PrintLine(Point p1, Point p2)
        {
            if (p1.X > p2.X)
                (p1, p2) = (p2, p1);

            float m = (float)(p2.Y - p1.Y) / (p2.X - p1.X);
            for (int i = p1.X + 1; i < p2.X; i++)
            {
                Program.WriteAt(i, (int)((i - p1.X) * m + p1.Y), '*');
            }
        }
    }


    internal class Program
    {
        private static readonly Random RandomObj = new Random();
        private static readonly int TimeToSleep = 50;
        private static readonly char LettersSign = '$';


        public static void Main(string[] args)
        {
            Fill();
            
            Point origin = new Point();
            origin.Show();
            Thread.Sleep(500);
            origin.Hide();

            // Ali F
            var points = new List<(char, int, int, int)>
            {
                ('p', 0, 4, 0), ('p', 1, 3, 0), ('p', 2, 2, 0), ('p', 3, 1, 0), ('p', 4, 0, 0), ('p', 5, 1, 0),
                ('p', 6, 2, 0), ('p', 7, 3, 0), ('p', 8, 4, 0), ('h', 3, 5, 2), // A
                ('v', 0, 4, 10), ('h', 10, 15, 4), // L
                ('h', 17, 21, 0), ('v', 0, 4, 19), ('h', 17, 21, 4), // I
                ('h', 25, 30, 0), ('v', 0, 4, 25), ('h', 25, 30, 2),
            };


            // P => Point
            // V => Vertical Line
            // H => Horizontal Line
            foreach (var point in points)
            {
                var (nt, nx, ny, nz) = point;

                if (nt == 'p')
                {
                    ShowPoint(nx, ny);
                }
                else if (nt == 'h')
                {
                    // H Line
                    for (int i = nx; i <= ny; i++)
                    {
                        ShowPoint(i, nz);
                    }
                }
                else if (nt == 'v')
                {
                    // V Line
                    for (int i = nx; i <= ny; i++)
                    {
                        ShowPoint(nz, i);
                    }
                }
            }

            Point p = new Point(6);
            Point p2 = new Point(10, 10, '#', ConsoleColor.Yellow);
            p.Show();
            p2.Show();
            Point.PrintLine(p, p2);

            Console.SetCursorPosition(0, Point.MaxY + 2);
            Console.WriteLine($"point number one IsShow property => {p.IsShow}");
        }

        // Show Point with random color
        private static void ShowPoint(int x, int y)
        {
            var mPoint = new Point(x, y, LettersSign, (ConsoleColor)RandomObj.Next(0, 16));
            mPoint.Show();
            Thread.Sleep(TimeToSleep);
        }

        private static void Fill()
        {
            Console.Clear();
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.WriteLine(" ");
            }
        }

        public static void WriteAt(int x, int y, char c, ConsoleColor color = ConsoleColor.White)
        {
            if (y > Point.MaxY)
                Point.MaxY = y;

            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(c);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}