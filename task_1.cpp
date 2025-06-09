#include <iostream>
#include <fstream>
#include <cmath>

using namespace std;

void drawTriangle(ofstream &svg, double x1, double y1, double x2, double y2, double x3, double y3)
{
    svg << "<polygon points=\""
        << x1 << "," << y1 << " "
        << x2 << "," << y2 << " "
        << x3 << "," << y3
        << "\" style=\"fill:black;stroke:none\" />\n";
}

void sierpinski(ofstream &svg, double x1, double y1, double x2, double y2, double x3, double y3, int depth)
{
    if (depth == 0)
    {
        drawTriangle(svg, x1, y1, x2, y2, x3, y3);
    }
    else
    {
        double mx12 = (x1 + x2) / 2.0;
        double my12 = (y1 + y2) / 2.0;
        double mx23 = (x2 + x3) / 2.0;
        double my23 = (y2 + y3) / 2.0;
        double mx31 = (x3 + x1) / 2.0;
        double my31 = (y3 + y1) / 2.0;

        sierpinski(svg, x1, y1, mx12, my12, mx31, my31, depth - 1);
        sierpinski(svg, x2, y2, mx23, my23, mx12, my12, depth - 1);
        sierpinski(svg, x3, y3, mx31, my31, mx23, my23, depth - 1);
    }
}

void generateSierpinski(int depth)
{
    const int width = 800;
    const int height = 700;
    ofstream svg("sierpinski.svg");

    if (!svg) {
        cerr << "Ошибка создания файла sierpinski.svg\n";
        return;
    }

    svg << "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n";
    svg << "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"" << width << "\" height=\"" << height << "\">\n";
    svg << "<rect width=\"100%\" height=\"100%\" fill=\"white\" />\n";

    double side = width * 0.85;
    double x1 = (width - side) / 2.0;
    double y1 = height - 50;
    double x2 = x1 + side;
    double y2 = height - 50;
    double x3 = x1 + side / 2.0;
    double y3 = height - 50 - side * sqrt(3) / 2.0;

    sierpinski(svg, x1, y1, x2, y2, x3, y3, depth);

    svg << "</svg>\n";
    svg.close();
    cout << "Файл sierpinski.svg успешно создан. Откройте его в браузере.\n";
}

int main()
{
    int depth;
    cout << "Введите глубину рекурсии (рекомендуется 0-7): ";
    cin >> depth;

    if (depth < 0)
    {
        cout << "Глубина должна быть неотрицательной.\n";
        return 1;
    }

    generateSierpinski(depth);
    return 0;
}
