using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Shapes;

namespace WpfApp2
{

    public partial class Signup : Window
    {
        string user;
        string pass;
        public Signup()
        {
            InitializeComponent();
        }

        public void Signup_Click(object sender, RoutedEventArgs e)
        {
            user = UserText.Text;
            pass = PassText.Text;
            if(user == "" || pass == "") {
                Mess.Content = "Write valid information ";
            }
            else
            {
                var rng = new RNGCryptoServiceProvider();
                byte[] salt = new byte[16];
                rng.GetBytes(salt);
                Mess.Content = Convert.ToBase64String(salt);
                var sha256 = new SHA256Managed();

                byte[] passwordBytes = Encoding.UTF8.GetBytes(pass);
                byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);
                byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];

                Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
                Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);
                
                MainWindow gologin = new MainWindow();
                gologin.UserData.Content = user;
                gologin.PassData.Content = pass;
                gologin.HashData.Content = Convert.ToBase64String(hashedPasswordWithSalt);
                gologin.salty = salt;
                gologin.Show();
                this.Close();
                
            }
        }
    }
}
