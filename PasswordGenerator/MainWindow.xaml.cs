using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;

namespace PasswordGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        int passwdLen;
        char[] currentChar;
        char[] specialChars;
        char startFlag = '\0';
        string flags = "";
        public MainWindow()
        {
            InitializeComponent();
            specialChars = new char[31];

            for (int i = 33; i < 48; i++)
            {
                specialChars[i - 33] = (char)i;
            }
            for (int i = 58; i < 65; i++)
            {
                specialChars[i - 44] = (char)i;
            }
            for (int i = 91; i < 97; i++)
            {
                specialChars[i - 70] = (char)i;
            }
            for (int i = 123; i < 127; i++)
            {
                specialChars[i - 96] = (char)i;
            }
        }

        char randomUpperChar()
        {
            char character = (char)rand.Next(65, 91);
            return character;
        }

        char randomLowerChar()
        {
            char character = (char)rand.Next(97, 123);
            return character;
        }

        char randomNumber()
        {
            char character = (char)rand.Next(48, 58);
            return character;
        }

        char randomSpecial()
        {
            return specialChars[rand.Next(31)];
        }

        void checkFlags()
        {
            if (upper.IsChecked == true) flags += "/u";
            if (lower.IsChecked == true) flags += "/l";
            if (numbers.IsChecked == true) flags += "/n";
            if (special.IsChecked == true) flags += "/s";
        }

        void checkStartFlag()
        {
            if (startUpper.IsChecked == true)
                startFlag = 'u';
            else if (startLower.IsChecked == true)
                startFlag = 'l';
            else if (startNumber.IsChecked == true)
                startFlag = 'n';
            else if (startSpecial.IsChecked == true)
                startFlag = 's';
        }

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            flags = "";
            startFlag = '\0';
            checkStartFlag();
            checkFlags();
            if(Int16.Parse(passwdLength.Text) < 4)
                alert.Content = "Password length should be between 4 and 99 characters";
            else if (String.IsNullOrEmpty(flags))
                alert.Content = "You have to check something!";
            else
            {
                passwdLen = Int16.Parse(passwdLength.Text);
                currentChar = new char[passwdLen];
                for (int i = 0; i < passwdLen; i++)
                {
                    switch (rand.Next(4))
                    {
                        case 0:
                            if (flags.Contains("/u"))
                                currentChar[i] = randomUpperChar();
                            else if (flags.Contains("/l"))
                                currentChar[i] = randomLowerChar();
                            else if (flags.Contains("/n"))
                                currentChar[i] = randomNumber();
                            else if (flags.Contains("/s"))
                                currentChar[i] = randomSpecial();
                            break;
                        case 1:
                            if (flags.Contains("/l"))
                                currentChar[i] = randomLowerChar();
                            else if (flags.Contains("/u"))
                                currentChar[i] = randomUpperChar();
                            else if (flags.Contains("/s"))
                                currentChar[i] = randomSpecial();
                            else if (flags.Contains("/n"))
                                currentChar[i] = randomNumber();
                            break;
                        case 2:
                            if (flags.Contains("/n"))
                                currentChar[i] = randomNumber();
                            else if (flags.Contains("/s"))
                                currentChar[i] = randomSpecial();
                            else if (flags.Contains("/u"))
                                currentChar[i] = randomUpperChar();
                            else if (flags.Contains("/l"))
                                currentChar[i] = randomLowerChar();
                            break;
                        case 3:
                            if (flags.Contains("/s"))
                                currentChar[i] = randomSpecial();
                            else if (flags.Contains("/n"))
                                currentChar[i] = randomNumber();
                            else if (flags.Contains("/l"))
                                currentChar[i] = randomLowerChar();
                            else if (flags.Contains("/u"))
                                currentChar[i] = randomUpperChar();
                            break;
                    }
                }
                if(startFlag != '\0')
                {
                    switch(startFlag)
                    {
                        case 'u':
                            currentChar[0] = randomUpperChar();
                            break;
                        case 'l':
                            currentChar[0] = randomLowerChar();
                            break;
                        case 'n':
                            currentChar[0] = randomNumber();
                            break;
                        case 's':
                            currentChar[0] = randomSpecial();
                            break;
                    }
                }

                passwdBox.Text = new string(currentChar);

                alert.Content = "";
            }
            
        }

        private void CheckIfNumbers(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
