#include <iostream>
#include <cmath>
#include <iomanip>
#include <windows.h>
using namespace std;

// ANSI escape codes for colors
#define RESET   "\033[0m"
#define RED     "\033[31m"
#define GREEN   "\033[32m"
#define YELLOW  "\033[33m"
#define BLUE    "\033[34m"
#define MAGENTA "\033[35m"
#define CYAN    "\033[36m"

void printTitle() {
    cout << YELLOW;
    cout << R"(
            ___       ___       ___       ___
           /\  \     /\__\     /\  \     /\  \
          /::\  \   /:/ _/_   /::\  \   /::\  \
         /:/\:\__\ /:/_/\__\ /::\:\__\ /:/\:\__\
         \:\ \/__/ \:\/:/  / \:\::/  / \:\/:/  /
          \:\__\    \::/  /   \::/  /   \::/  /
           \/__/     \/__/     \/__/     \/__/
)";

    cout << "\n------------------------------------------------------------" << endl;
    cout << RESET << "    cubo - an efficient, recursive square root calculator" << endl;
    cout << YELLOW << "------------------------------------------------------------" << RESET << endl;;
}

int simplifyRoot(const int n, int& underRoot) {
    if (n == 0) {
        underRoot = 0;
        return 0;
    }
    if (n == 1) {
        underRoot = 1;
        return 1;
    }

    int i = 2;
    while (i * i <= n) {
        if (n % (i * i) == 0) {
            int subCoefficient = simplifyRoot(n / (i * i), underRoot);
            return i * subCoefficient;
        }
        i++;
    }

    underRoot = n;
    return 1;
}

void displayRoot(const int coeff, const int underRoot, const bool exact, const int precision = 2) {
    cout << CYAN;
    if (exact) {
        if (underRoot == 1)
            cout << coeff;
        else if (coeff == 1)
            cout << "\u221A" << underRoot;
        else
            cout << coeff << "\u221A" << underRoot;
    } else {
        cout << fixed << setprecision(precision) << (coeff * sqrt(underRoot));
    }
    cout << RESET;
}

int main() {
    SetConsoleOutputCP(CP_UTF8);
    system("cls");  // Clear screen at start

    // Enable ANSI escape codes in Windows console
    HANDLE hOut = GetStdHandle(STD_OUTPUT_HANDLE);
    DWORD dwMode = 0;
    GetConsoleMode(hOut, &dwMode);
    SetConsoleMode(hOut, dwMode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);

    int number;
    char choice;
    int precision;

    printTitle();

    while (true) {
        cout << CYAN << "\n→ Enter a positive integer " << RESET << ": ";
        cin >> number;

        if (number == 0) {
            cout << RED << "\nThank you for using the Square Root Simplifier!\n" << RESET;
            break;
        }
        if (number < 0) {
            cout << RED << "❌ Error: Please enter a positive number." << RESET << endl;
            continue;
        }

        int underRoot;
        int coefficient = simplifyRoot(number, underRoot);

        cout << CYAN << "→ Choose output format:\n" << RESET;
        cout << GREEN << "\t[E] Exact form\n";
        cout << "\t[D] Decimal approximation\n" << RESET;
        cout << CYAN << "Your choice: " << RESET;
        cin >> choice;

        if (toupper(choice) == 'D') {
            cout << GREEN << "→ Enter decimal precision (1-10): " << RESET;
            cin >> precision;
            precision = max(1, min(10, precision));

            cout << GREEN << "Result: " << RESET << "√" << number << " = ";
            displayRoot(coefficient, underRoot, false, precision);
            cout << endl;
        } else {
            cout << GREEN << "Result: " << RESET << "√" << number << " = ";
            displayRoot(coefficient, underRoot, true);
            cout << endl;
        }
    }

    return 0;
}