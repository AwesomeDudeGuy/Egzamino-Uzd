using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        string uselog;
        string passlog;
        string user;
        string pass;
        string hash;
        string log;
        public byte[] salty;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            uselog = UserLogin.Text;
            passlog = PassLogin.Text;
            user =(string)UserData.Content;
            pass = (string)PassData.Content;
            hash = (string)HashData.Content;

            var sha256 = new SHA256Managed();
            byte[] salt = salty;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(passlog);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            byte[] hashedBytes = sha256.ComputeHash(saltedPassword);
            byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
            Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
            Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

            log = Convert.ToBase64String(hashedPasswordWithSalt);

            if (uselog == user && log == hash ) {

                Window1 hashing = new Window1();

                hashing.Password.Text = pass;
                hashing.HashPassword.Text = hash;

                hashing.Show();
                this.Close();
            }
            else
            {
                Mess.Content = "Wrong password or username";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Signup gosingup = new Signup();
            gosingup.Show();
            this.Close();
        }
        private void Forced_Click(object sender, RoutedEventArgs e)
        {
            if (UserData.Content == "") {
                Mess.Content = "No users detected";
            }
            else
            {
                pass = (string)PassData.Content;
                Forced remember = new Forced();
                remember.Show();
                remember.TruePass.Content = pass;
            }
        }
    }
}
