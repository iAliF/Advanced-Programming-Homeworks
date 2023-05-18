using System;

namespace HomeWork1
{
    class Point
    {
        public float X;
        public float Y;

        public double DistanceFromOrigin => Math.Sqrt(X * X + Y * Y);


        public Point() : this(0, 0)
        {
        }

        
        public Point(float xy) : this(xy, xy)
        {
        }

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void Display()
        {
            Console.WriteLine(this);
        }


        public override string ToString()
        {
            return $"Point({X}, {Y})";
        }
    }

    class Rectangle
    {
        private int _width, _height;

        public Point Corner;

        
        public int Width
        {
            get => _width;
            set
            {
                if (value < 0)
                    throw new Exception("Invalid value for Width property");

                _width = value;
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                if (value < 0)
                    throw new Exception("Invalid value for Height property");

                _height = value;
            }
        }

        public int Area => Width * Height;

        public int Perimeter => 2 * (Width + Height);

        public Rectangle() : this(new Point(), 1, 1)
        {
        }

        
        public Rectangle(int width, int height) : this(new Point(), width, height)
        {
        }

        
        public Rectangle(int x, int y, int width, int height) : this(new Point(x, y), width, height)
        {
        }

        
        public Rectangle(Point corner, int width, int height)
        {
            Corner = corner;
            Width = width;
            Height = height;
        }

        
        public void Display()
        {
            Console.WriteLine(this);
        }

        
        // تابع بررسی همپوشانی دو مستطیل
        // به این صورت که چک میکند 4 نقطه مستطیل، درون مستطیل دیگر هستند یا نه 
        public bool HasOverlap(Rectangle rect)
        {
            // تعریف نقاط مستطیل و ذخیره آن ها در آرایه
            Point[] points =
            {
                Corner, // Upper Right
                new Point(Corner.X - Width, Corner.Y), // Upper Left
                new Point(Corner.X, Corner.Y - Height), // Lower Right
                new Point(Corner.X - Width, Corner.Y - Height) // Lower Right
            };

            // چک کردن اینکه آیا این نقاط درون مستطیل دیگر هستند یا نه
            foreach (var point in points)
            {
                if (rect.IsInRectangle(point))
                    return true;
            }

            return false;
        }

        // تابع چک کردن اینکه نقطه درون مستطیل هست یا نه
        public bool IsInRectangle(Point point) => point.X < Corner.X
                                                  && point.X > (Corner.X - Width)
                                                  && point.Y < Corner.Y
                                                  && point.Y > (Corner.Y - Height);

        public override string ToString() {
            return $"Rectangle(x={Corner.X}, y={Corner.Y}, width={Width}, height={Height})";
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            // -=[Points]=-
            Point p1 = new Point(3, 4);
            Point p2 = new Point(); // (0, 0)
            Point p3 = new Point(7); // (7, 7) 

            Console.WriteLine("Points: ");
            p1.Display();
            p2.Display();
            p3.Display();

            Console.WriteLine($"P1 Distance From Origin: {p1.DistanceFromOrigin}");

            // -=[Rectangles]=-
            Rectangle r1 = new Rectangle(); // Corner: Origin | w, h = 1
            Rectangle r2 = new Rectangle(p2, 7, 13); // Corner: p2
            Rectangle r3 = new Rectangle(3, 4, 6, 4); // Corner: (3, 4), W: 6, H: 4

            Console.WriteLine("Rectangles: ");
            r1.Display();
            r2.Display();
            r3.Display();

            Console.WriteLine(
                $"Rectangle 3 | Area: {r3.Area} | Perimeter: {r3.Perimeter} |" +
                $" Corner Distance From Origin: {r3.Corner.DistanceFromOrigin}"
            );
            
            Console.WriteLine($"Is P1 in R1: {r1.IsInRectangle(p1)}");
            
            Console.WriteLine($"IS R1 & R2 has overlap: {r1.HasOverlap(r2)}");
        }
    }
}