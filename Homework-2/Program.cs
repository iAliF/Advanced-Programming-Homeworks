// .NET Framework Version 4.8.1


using System;

namespace HomeWork2
{
    class Point
    {
        public double X;
        public double Y;

        public Point() : this(0, 0)
        {
        }


        public Point(double xy) : this(xy, xy)
        {
        }

        public Point(double x, double y)
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
            return $"Point(({X}, {Y})";
        }
    }

    class Line
    {
        // AX + BY + C = 0
        // Y = (-A/B)X - (C/B)
        public double A, B, C;

        public double VarCoeff => -(A / B);
        // ویژگی عدد ثابت
        public double ConstValue => -(C / B);

        public double YIntercept => ConstValue;
        
        public Line() : this(1, 1)
        {
        }

        public Line(double a, double b, double c = 0)
        {
            A = a;
            B = b;
            C = c;
        }

        // f(x)
        public double Calculate(double x) => (VarCoeff * x) + ConstValue; // (-A/B)X + (-C/B)

        public Line PerpendicularLine(Point p)
        {
            // m = -b / a
            double newM = B / A;

            return LineFromData(newM, p, A);
        }


        public static Point IntersectionOfTwoLines(Line l1, Line l2)
        {
            // Are they parallel
            if (Math.Abs((l1.A / l2.A) - (l1.B / l2.B)) == 0)
                return null;

            // AX + BY + C = A`X + B`Y + C`
            var a = l1.A;
            var b = l1.B;
            var c = l2.A;
            var d = l2.B;
            var c1 = l1.C;
            var c2 = l2.C;

            // Calculate with matrix ..
            // Det of matrix:
            var det = a * d - b * c;

            // Intersection X, Y
            var x = ((-d * c1) + (b * c2)) / det;
            var y = ((c * c1 - a * c2)) / det;

            return new Point(x, y);
        }

        public static Line LinePassingTwoPoints(Point p1, Point p2)
        {
            // dx = x1 - x2
            var dx = p1.X - p2.X;
            // dy = y1 - y2
            var dy = p1.Y - p2.Y;
            // m
            var m = dy / dx;

            return LineFromData(m, p1, dx);
        }

        // Line from M, Point and constant
        public static Line LineFromData(double m, Point p, double constant)
        {
            // y - y0 = m(x - x0)
            // -m.x + y + (m.x0 - y0) = 0 
            // A = -m | B = 1 | C = m.x0 - y0
            double[] coeffs = {-m, 1, (m * p.X) - p.Y};

            double mult = coeffs[1] < 0 ? constant : -constant;
            for (int i = 0; i < coeffs.Length; i++)
            {
                coeffs[i] *= mult;
            }

            return new Line(coeffs[0], coeffs[1], coeffs[2]);
        }

        public void Display()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return $"Line | {A}X + ({B})Y + ({C}) = 0";
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            
            Point origin = new Point();
            Point p2 = new Point(3, 4);
            origin.Display();
            Console.WriteLine("=========================================");

            // 2x + 5y - 5 = 0
            Line l1 = new Line(2, 5, -5);
            l1.Display();
            Console.WriteLine($"Line 1 | X = 10 => Y = {l1.Calculate(10)}");
            Console.WriteLine($"Line 1 | X = 0 => Y = {l1.Calculate(0)}");
            Console.WriteLine($"Line 1 Y-Intercept is {l1.YIntercept}");
            Console.WriteLine("=========================================");
            
            // 4x + 6y + 8 = 0
            Line l2 = new Line(4, 6, 8);
            l2.Display();
            Console.WriteLine("=========================================");

            Console.WriteLine($"Intersect of Line 1 & Line 2 is: {Line.IntersectionOfTwoLines(l1, l2)}");
            Console.WriteLine("=========================================");

            Console.WriteLine($"Point 1: {origin} | Point 2: {p2}");
            Console.WriteLine($"Line passing Point 1 & Point 2 is: {Line.LinePassingTwoPoints(origin, p2)}");
            Console.WriteLine("=========================================");

            Point onLine = new Point(10, l1.Calculate(10));
            Console.WriteLine($"Perpendicular Line of Line 1 at {onLine} => {l1.PerpendicularLine(onLine)}");
            Console.WriteLine("=========================================");

            Line l3 = new Line();
            Console.WriteLine($"Default Line => {l3}");
            Console.WriteLine("=========================================");

            Line l4 = new Line(6, 8);
            Console.WriteLine($"Line with A & B => {l4}");
            Console.WriteLine("=========================================");

        }
    }
}