using System;
using System.Collections.Generic;

class CoinChange
{
    static int MinCoinsHelper(int amount, List<int> coins, int[] memo)
    {
        if (amount == 0) return 0;
        if (amount < 0) return -1;
        
        if (memo[amount] != -2) 
            return memo[amount];
        
        int minCount = int.MaxValue;
        bool foundSolution = false;
        
        foreach (int coin in coins)
        {
            int res = MinCoinsHelper(amount - coin, coins, memo);
            if (res != -1)
            {
                minCount = Math.Min(minCount, res + 1);
                foundSolution = true;
            }
        }
        
        memo[amount] = foundSolution ? minCount : -1;
        return memo[amount];
    }

    static int MinCoins(int amount, List<int> coins)
    {
        int[] memo = new int[amount + 1];
        for (int i = 0; i < memo.Length; i++)
            memo[i] = -2; // -2 означает "не вычислено"
        
        return MinCoinsHelper(amount, coins, memo);
    }

    static void Main()
    {
        Console.Write("Введите сумму, которую нужно набрать: ");
        int amount;
        while (!int.TryParse(Console.ReadLine(), out amount) || amount < 0)
        {
            Console.Write("Некорректный ввод. Введите положительное число: ");
        }
        
        Console.Write("Введите количество номиналов монет: ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.Write("Некорректный ввод. Введите число больше 0: ");
        }
        
        List<int> coins = new List<int>();
        Console.WriteLine("Введите номиналы монет через пробел:");
        string[] input = Console.ReadLine().Split(' ');
        
        foreach (string s in input)
        {
            if (int.TryParse(s, out int coin) && coin > 0)
            {
                coins.Add(coin);
            }
        }
        
        if (coins.Count != n)
        {
            Console.WriteLine("Ошибка: количество введенных номиналов не соответствует указанному.");
            return;
        }
        
        int result = MinCoins(amount, coins);
        
        if (result == -1)
        {
            Console.WriteLine("Невозможно набрать указанную сумму данными монетами.");
        }
        else
        {
            Console.WriteLine($"Минимальное количество монет: {result}");
        }
    }
}