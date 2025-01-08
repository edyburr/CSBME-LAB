#include <iostream>
#include <windows.h>
using namespace std;

static void display_binary(unsigned int n, int nb)
{
    int k = 0;
    char val_b[32];
    if (n <= 255)
        k = 8;
    else if (n <= 65535)
        k = 16;
    else if (n <= 2147483646)
        k = 32;
    if (nb > 0)
        k = nb;
    if (n > 2147483646) {
        k = 0;
        cout << "\nNumber too large";
    }
    for (int i = 0; i < k; i++) {
        val_b[k - 1 - i] = (n % 2 == 0) ? '0' : '1';
        n = n / 2;
    }
    // binary display
    for (int i = 0; i < k; i++) {
        cout << (val_b[i] == '0' ? char(176) : char(219)) << " ";
    }
    // prompter on the first position
    for (int i = 0; i < (2 * k + 1); i++) {
        cout << "\b";
    }
}

void convert_temperature(double tempBuf, char scaleBuf, char scaleConv) {
    double tempC, tempF, tempK;
    double convertedTemp = tempBuf;
    string scaleStr;

    switch (scaleConv) {
    case 'C':
        if (scaleBuf == 'F') {
            convertedTemp = (tempBuf - 32) * 5.0 / 9.0;
        }
        else if (scaleBuf == 'K') {
            convertedTemp = tempBuf - 273.15;
        }
        scaleStr = "Celsius";
        break;
    case 'F':
        if (scaleBuf == 'C') {
            convertedTemp = (tempBuf * 9.0 / 5.0) + 32;
        }
        else if (scaleBuf == 'K') {
            convertedTemp = (tempBuf - 273.15) * 9.0 / 5.0 + 32;
        }
        scaleStr = "Fahrenheit";
        break;
    case 'K':
        if (scaleBuf == 'C') {
            convertedTemp = tempBuf + 273.15;
        }
        else if (scaleBuf == 'F') {
            convertedTemp = (tempBuf - 32) * 5.0 / 9.0 + 273.15;
        }
        scaleStr = "Kelvin";
        break;
    }

    Sleep(500);
    cout << "The temperature is: " << convertedTemp << " " << scaleStr << "\n";
    Sleep(500);
    char response;

    while (true) {
        Sleep(500);
        cout << "Do you want to display the temperature in binary format? [y/n]: ";
        cin >> response;

        if (response == 'y' || response == 'n') {
            break;
        }

        cout << "Please enter a valid response! \n";
        cout << "Choose 'y' to confirm, or 'n' to close the program!\n\n";
    }

    if (response == 'y') {
        unsigned int tempUint = static_cast<unsigned int>(convertedTemp);
        display_binary(tempUint, 16);
    }
}

int main() {
    double tempBuf;
    char scaleBuf, scaleConv;

    while (true) {
        cout << "Welcome to the Temperature Converter! \n";
        cout << "---------------------------------------- \n";

        Sleep(1000);
        cout << "Enter the temperature value you want to convert: ";
        cin >> tempBuf;

        while (true) {
            Sleep(500);
            cout << "Enter the unit of measure: [C/F/K] ";
            cin >> scaleBuf;

            if (scaleBuf == 'C' || scaleBuf == 'F' || scaleBuf == 'K') {
                break;
            }

            cout << "Please enter a valid unit of measure! \n";
            cout << "Choose from 'C' for Celsius, 'F' for Fahrenheit, or 'K' for Kelvin\n\n";
        }

        system("cls");
        Sleep(1000);
        cout << "You entered the temperature of " << tempBuf << '\370' << scaleBuf << endl;

        Sleep(1000);

        while (true) {
            Sleep(500);
            cout << "Enter the unit of measure you want to convert to: [C/F/K] ";
            cin >> scaleConv;

            if (scaleConv == 'C' || scaleConv == 'F' || scaleConv == 'K') {
                break;
            }

            cout << "Please enter a valid unit of measure! \n";
            cout << "Choose from 'C' for Celsius, 'F' for Fahrenheit, or 'K' for Kelvin\n\n";
        }

        system("cls");
        Sleep(1000);

        convert_temperature(tempBuf, scaleBuf, scaleConv);

        Sleep(500);
        cout << "\n \n";
        system("pause");
        system("cls");
    }
}