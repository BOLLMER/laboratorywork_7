#include <iostream>
#include <vector>
#include <algorithm>
#include <climits>

using namespace std;

int minCoinsHelper(int amount, const vector<int>& coins, vector<int>& memo)
{
    if (amount == 0) return 0;
    if (amount < 0) return -1;

    if (memo[amount] != -2) return memo[amount];

    int minCount = INT_MAX;
    bool foundSolution = false;

    for (int coin : coins)
    {
        int result = minCoinsHelper(amount - coin, coins, memo);
        if (result != -1)
        {
            minCount = min(minCount, result + 1);
            foundSolution = true;
        }
    }

    memo[amount] = foundSolution ? minCount : -1;
    return memo[amount];
}

int minCoins(int amount, const vector<int>& coins)
{
    vector<int> memo(amount + 1, -2);
    return minCoinsHelper(amount, coins, memo);
}

int main() {
    int amount, n;

    cout << "Введите сумму, которую нужно набрать: ";
    cin >> amount;

    cout << "Введите количество различных номиналов монет: ";
    cin >> n;

    vector<int> coins(n);
    cout << "Введите номиналы монет через пробел: ";
    for (int i = 0; i < n; ++i)
    {
        cin >> coins[i];
    }

    for (int coin : coins)
    {
        if (coin <= 0)
        {
            cout << "Номиналы монет должны быть положительными числами!\n";
            return 1;
        }
    }

    int result = minCoins(amount, coins);

    if (result == -1)
    {
        cout << "Невозможно набрать указанную сумму данными монетами.\n";
    }
    else
    {
        cout << "Минимальное количество монет: " << result << endl;
    }

    return 0;
}
