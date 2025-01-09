//ID Generator and Validator
#include <algorithm>
#include <format>
#include <iostream>
#include <random>
#include <iomanip>
#include <windows.h>

using namespace std;
HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);

string CryptoGen(int length) {
    random_device random;
    mt19937 generator(random());

    const string letters = "abcdefghijklmnopqrstuvwxyz";
    const string numbers = "0123456789";

    string oss;
    for(int i = 0; i < length; i++) {
        if(uniform_int_distribution<int> choice(0, 1); choice(generator)) {
            uniform_int_distribution<uint_fast8_t> letter_dist(0, letters.length()-1);
            oss += letters[letter_dist(generator)];
        } else {
            uniform_int_distribution<uint_fast8_t> num_dist(0, numbers.length()-1);
            oss += numbers[num_dist(generator)];
        }
    }
    return oss;
}

string CyanoGen() {
    random_device random;
    mt19937 generator(random());
    uniform_int_distribution<int> dist(0, 9);
    const string cyano[10] = {"azo", "halo", "cyano", "thio", "bromo", "chloro", "hydro", "oxo", "litho", "silico"};
    string oss = cyano[dist(generator)];
    return oss;
}

int getLength() {
    const int MIN_LENGTH = 8;
    const int MAX_LENGTH = 32;

    while (true) {
        int length;
        cout << "Enter \033[32mvUID\033[0m length: ";
        cin >> length;

        if (length == 0) {
            cout << "\033[32mvUID\033[0m length cannot be zero." << endl;
            continue;
        }

        if (length < MIN_LENGTH) {
            cout << "\033[32mvUID\033[0m length is too short." << endl;
            continue;
        }

        if (length > MAX_LENGTH) {
            cout << "\033[32mvUID\033[0m length cannot be greater than 32." << endl;
            continue;
        }

        if (length % 2 != 0) {
            cout << "\033[32mvUID\033[0m length should be an even number." << endl;
            Sleep(500);

            char response;
            while (true) {
                cout << "Change length value? [Y/N]: ";
                cin >> response;
                response = tolower(response);

                if (response == 'y') break;
                if (response == 'n') return length;
                cout << "Invalid input." << endl;
            }
            continue;
        }

        return length;
    }
}

bool useCyano() {
    bool cyano = false;

    char response;
    while (true) {
        cout << "Do you wish to enable \033[36mCyanoGen\033[0m morphology? \nCyanoGen adds another layer of uniqueness to your ID." << endl;
        cout << "[Y/N] ";
        cin >> response;
        response = tolower(response);

        if (response == 'y') {
            cyano = true;
            break;
        }
        if (response == 'n') break;
        cout << "Invalid input." << endl;
    }

    return cyano;
}

string IdConstructor(int length, bool cyano) {
    string vUID = CryptoGen(length);
    string id;
    if (cyano) {
        string cyanogen = CyanoGen();
        if (length >= 16) {
            string sID = vUID.substr(0, (length/2)-1);
            string eID = vUID.substr((length/2)-1, length);
            id = format("CX-{}-{}-{}",sID, cyanogen, eID);
        } else id = format("BX-{}-{}",cyanogen, vUID);
    } else {
        id = format("AX-{}", vUID);
    }

    return id;
}

int main()
{
    SetConsoleOutputCP(CP_UTF8);
    cout <<
" ▄▄·  ▄· ▄▌ ▄▄▄·  ▐ ▄        ▄▄ • ▄▄▄ . ▐ ▄\n"
"▐█ ▌▪▐█▪██▌▐█ ▀█ •█▌▐█ ▄█▀▄ ▐█ ▀ ▪▀▄.▀·•█▌▐█\n"
"██ ▄▄▐█▌▐█▪▄█▀▀█ ▐█▐▐▌▐█▌.▐▌▄█ ▀█▄▐▀▀▪▄▐█▐▐▌\n"
"▐███▌ ▐█▀·.▐█▪ ▐▌██▐█▌▐█▌.▐▌▐█▄▪▐█▐█▄▄▌██▐█▌\n"
"·▀▀▀   ▀ •  ▀  ▀ ▀▀ █▪ ▀█▄▀▪·▀▀▀▀  ▀▀▀ ▀▀ █▪\n";
    cout <<"-----------------------------------------------" << endl;
    cout << "Cyanogen v1.0.0" << " -=-=- " << "Customisable ID Generator" << endl;
    cout <<"-----------------------------------------------" << endl;
    Sleep(1000);
    cout << "Welcome to \033[32mCyanogen\033[0m! " << endl;
    Sleep(500);
    cout << "Cyanogen is a cryptographically-safe, customisable ID Generator." << endl;
    Sleep(2000);

    cout << "\nIDs follow the following template: \t" << "\033[33mAX/BX/CX\033[0m - \033[36mcyano[opt]\033[0m - \033[32mvUID[8-32]\033[0m" << endl;

    cout << "Dictionary: " << endl << "- \033[32mvUID[8-32]\033[0m - Virtual UID" << endl << "- \033[36mcyano[opt]\033[0m - Cyano Morphology Toggle" << endl;
    cout <<"\nTo create your ID, follow the on-screen instructions." << endl;
    Sleep(2000);

    Generator:

    int length = getLength();
    bool cyano = useCyano();

    cout << "Your shiny new ID is: " << endl;

    SetConsoleTextAttribute(hConsole, FOREGROUND_GREEN | FOREGROUND_INTENSITY);
    cout << IdConstructor(length, cyano) << endl;
    SetConsoleTextAttribute(hConsole, FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE);

    Sleep(2000);
    cout << "\nDo you wish to generate another ID? [Y/N]";
    string response;
    cin >> response;
    while (true) {
        if(response == "Y" || response == "y") {
            system("cls");
            goto Generator;
        }
        if(response == "N" || response == "n") {
            break;
        }
    }
    system("pause");
return 0;
}



