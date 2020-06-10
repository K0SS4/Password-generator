using System;
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

        char randomUpperChars()
        {
            char character = (char)rand.Next(65, 91);
            return character;
        }

        char randomLowerChars()
        {
            char character = (char)rand.Next(97, 123);
            return character;
        }

        char randomNumbers()
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

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            passwdLen = Int16.Parse(passwdLength.Text);
            currentChar = new char[passwdLen];
            checkFlags();
            if (flags.Contains("/u") && flags.Contains("/l") && flags.Contains("/n") && flags.Contains("/s"))
            {
                for (int i = 0; i < passwdLen; i++)
                {
                    switch (rand.Next(4))
                    {
                        case 0:
                            currentChar[i] = randomUpperChars();
                            break;
                        case 1:
                            currentChar[i] = randomLowerChars();
                            break;
                        case 2:
                            currentChar[i] = randomNumbers();
                            break;
                        case 3:
                            currentChar[i] = randomSpecial();
                            break;
                    }

                }
                passwdBox.Text = new string(currentChar);
                alert.Content = "";
            }
            else alert.Content = "You have to check something!";
        }
    }
}
