using System;
using System.IO;

class SierpinskiTriangle
{
    static void DrawTriangle(StreamWriter svg, double x1, double y1, 
                           double x2, double y2, double x3, double y3)
    {
        svg.WriteLine($"<polygon points=\"{x1},{y1} {x2},{y2} {x3},{y3}\" " +
                      "style=\"fill:black;stroke:none\" />");
    }

    static void Sierpinski(StreamWriter svg, double x1, double y1, 
                         double x2, double y2, double x3, double y3, int depth)
    {
        if (depth == 0)
        {
            DrawTriangle(svg, x1, y1, x2, y2, x3, y3);
        }
        else
        {
            double mid12x = (x1 + x2) / 2;
            double mid12y = (y1 + y2) / 2;
            double mid23x = (x2 + x3) / 2;
            double mid23y = (y2 + y3) / 2;
            double mid31x = (x3 + x1) / 2;
            double mid31y = (y3 + y1) / 2;

            Sierpinski(svg, x1, y1, mid12x, mid12y, mid31x, mid31y, depth - 1);
            Sierpinski(svg, x2, y2, mid23x, mid23y, mid12x, mid12y, depth - 1);
            Sierpinski(svg, x3, y3, mid31x, mid31y, mid23x, mid23y, depth - 1);
        }
    }

    static void GenerateSierpinski(int depth)
    {
        const int width = 800;
        const int height = 700;
        string filePath = "sierpinski.svg";

        using (StreamWriter svg = new StreamWriter(filePath))
        {
            svg.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            svg.WriteLine($"<svg width=\"{width}\" height=\"{height}\" " + "xmlns=\"http://www.w3.org/2000/svg\">");
            svg.WriteLine("<rect width=\"100%\" height=\"100%\" fill=\"white\" />");

            double side = width * 0.85;
            double x1 = (width - side) / 2;
            double y1 = height - 50;
            double x2 = x1 + side;
            double y2 = height - 50;
            double x3 = x1 + side / 2;
            double y3 = height - 50 - side * Math.Sqrt(3) / 2;

            Sierpinski(svg, x1, y1, x2, y2, x3, y3, depth);

            svg.WriteLine("</svg>");
        }

        Console.WriteLine($"Файл {filePath} успешно создан. Откройте его в браузере.");
    }

    static void Main()
    {
        Console.Write("Введите глубину рекурсии (рекомендуется 0-7): ");
        if (!int.TryParse(Console.ReadLine(), out int depth) || depth < 0)
        {
            Console.WriteLine("Глубина должна быть неотрицательным числом.");
            return;
        }

        GenerateSierpinski(depth);
    }
}