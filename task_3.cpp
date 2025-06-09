#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

bool canInstallSystemsRecursive(vector<int>& computers, const vector<int>& systems, int systemIndex)
{
    if (systemIndex == systems.size())
    {
        return true;
    }
    for (int i = 0; i < computers.size(); ++i)
    {
        if (computers[i] >= systems[systemIndex])
        {
            computers[i] -= systems[systemIndex];
            if (canInstallSystemsRecursive(computers, systems, systemIndex + 1))
            {
                return true;
            }
            computers[i] += systems[systemIndex];
        }
    }
    return false;
}

bool canInstallSystems(vector<int> computers, vector<int> systems)
{
    sort(systems.rbegin(), systems.rend());

    sort(computers.rbegin(), computers.rend());

    return canInstallSystemsRecursive(computers, systems, 0);
}

int main() {
    int m, n;

    cout << "Введите количество компьютеров: ";
    cin >> m;

    vector<int> computers(m);
    cout << "Введите свободную память для каждого компьютера (через пробел): ";
    for (int i = 0; i < m; ++i)
    {
        cin >> computers[i];
        if (computers[i] <= 0)
        {
            cout << "Память компьютера должна быть положительной!\n";
            return 1;
        }
    }

    cout << "Введите количество систем для установки: ";
    cin >> n;

    vector<int> systems(n);
    cout << "Введите требования к памяти для каждой системы (через пробел): ";
    for (int i = 0; i < n; ++i)
    {
        cin >> systems[i];
        if (systems[i] <= 0)
        {
            cout << "Требования системы должны быть положительными!\n";
            return 1;
        }
    }

    if (canInstallSystems(computers, systems))
    {
        cout << "Все системы могут быть установлены на компьютеры.\n";
    }
    else
    {
        cout << "Невозможно установить все системы на имеющиеся компьютеры.\n";
    }

    return 0;
}
