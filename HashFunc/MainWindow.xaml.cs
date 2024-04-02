using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HashFunc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComputeHashButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputTextBox.Text;
            if (!string.IsNullOrEmpty(input))
            {
                byte[] hash = ComputeHash(input);
                HashResultTextBlock.Text = BitConverter.ToString(hash).Replace("-", "");
            }
            else
            {
                MessageBox.Show("Please enter some text to compute hash.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateRandomNumbersButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputTextBox.Text;
            if (!string.IsNullOrEmpty(input))
            {
                byte[] hash = ComputeHash(input);
                string hashString = BitConverter.ToString(hash).Replace("-", "");
                HashResultTextBlock.Text = hashString;

                RandomNumbersListBox.Items.Clear();
                for (int i = 0; i < hashString.Length - 3; i += 4)
                {
                    string substring = hashString.Substring(i, 4);
                    int randomNumber = int.Parse(substring, System.Globalization.NumberStyles.HexNumber);
                    RandomNumbersListBox.Items.Add(new TextBlock { Text = $"Substring {i / 4 + 1}: {substring}, Random Number: {randomNumber}" });
                }
            }
            else
            {
                MessageBox.Show("Please enter some text to generate random numbers.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private byte[] ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                return sha256.ComputeHash(inputBytes);
            }
        }
    }
}
