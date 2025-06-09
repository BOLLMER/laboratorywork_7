using System;
using System.Collections.Generic;
using System.Linq;

class SystemInstaller
{
    static bool CanInstallRecursive(List<int> computers, List<int> systems, int systemIndex)
    {
        if (systemIndex == systems.Count)
            return true;

        for (int i = 0; i < computers.Count; i++)
        {
            if (computers[i] >= systems[systemIndex])
            {
                computers[i] -= systems[systemIndex];

                if (CanInstallRecursive(computers, systems, systemIndex + 1))
                    return true;

                computers[i] += systems[systemIndex];
            }
        }

        return false;
    }

    static bool CanInstallSystems(List<int> computers, List<int> systems)
    {
        computers = computers.OrderByDescending(x => x).ToList();
        systems = systems.OrderByDescending(x => x).ToList();

        return CanInstallRecursive(new List<int>(computers), systems, 0);
    }

    static void Main()
    {
        Console.Write("Введите количество компьютеров: ");
        int computerCount;
        while (!int.TryParse(Console.ReadLine(), out computerCount) || computerCount <= 0)
        {
            Console.Write("Ошибка. Введите положительное число: ");
        }

        List<int> computers = new List<int>();
        Console.WriteLine("Введите память каждого компьютера через пробел:");
        string[] compInput = Console.ReadLine().Split(' ');
        foreach (string s in compInput)
        {
            if (int.TryParse(s, out int memory) && memory > 0)
            {
                computers.Add(memory);
            }
        }

        if (computers.Count != computerCount)
        {
            Console.WriteLine("Ошибка: количество компьютеров не совпадает!");
            return;
        }

        Console.Write("Введите количество систем: ");
        int systemCount;
        while (!int.TryParse(Console.ReadLine(), out systemCount) || systemCount <= 0)
        {
            Console.Write("Ошибка. Введите положительное число: ");
        }

        List<int> systems = new List<int>();
        Console.WriteLine("Введите требования к памяти для каждой системы через пробел:");
        string[] sysInput = Console.ReadLine().Split(' ');
        foreach (string s in sysInput)
        {
            if (int.TryParse(s, out int requirement) && requirement > 0)
            {
                systems.Add(requirement);
            }
        }

        if (systems.Count != systemCount)
        {
            Console.WriteLine("Ошибка: количество систем не совпадает!");
            return;
        }

        bool canInstall = CanInstallSystems(computers, systems);

        Console.WriteLine(canInstall 
            ? "Все системы могут быть установлены" 
            : "Невозможно установить все системы");
    }
}