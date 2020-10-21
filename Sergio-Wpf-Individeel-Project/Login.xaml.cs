using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Security.Cryptography;

namespace Sergio_Wpf_Individeel_Project
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string hash = "kcuf@BV";
            byte[] data = UTF8Encoding.UTF8.GetBytes(txtPassword.Password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    Byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    txtPassword.Password = Convert.ToBase64String(results, 0, results.Length);
                }
            }
            using (MagazijnEntities ctx = new MagazijnEntities())
            {

                var geselecteerdeGebruiker = ctx.PersoneelsIDs.Where(x => x.Username == txtUsername.Text && x.Wachtwoord == txtPassword.Password).Count();
                var loging = ctx.PersoneelsIDs.Where(x => x.Username == txtUsername.Text).Select(x => x.Wachtwoord).Count();
                //var wachtwoord = ctx.PersoneelsIDs.Where(x => x.Username == txtUsername.Text).Select(x => x.Wachtwoord).FirstOrDefault().ToString();

                if (geselecteerdeGebruiker == 1)
                {

                    string username = txtUsername.Text;
                    txtPassword.Clear();
                    txtUsername.Clear();
                    //MessageBox.Show("Gebruiker naam gevonden and pass: " + wachtwoord);
                    HoofdMenu hoofd = new HoofdMenu(username);
                    hoofd.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Gebruiker naam niet gevonden xxxxxxxxxxxx");
                    txtPassword.Clear();
                    txtUsername.Clear();
                }
                //    var wachtwoord = ctx.PersoneelsIDs.Where(x => x.Username == txtUsername.Text).Select(x => x.Wachtwoord).FirstOrDefault().ToString();
                //    byte[] data = Convert.FromBase64String(wachtwoord);

                //using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                //{
                //    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                //    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                //    {
                //        ICryptoTransform transform = tripDes.CreateDecryptor();
                //        Byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                //        wachtwoord = UTF32Encoding.UTF8.GetString(results);
                //        //MessageBox.Show(wachtwoord);
                //    }
                //}
            }



            //if (wachtwoord == txtPassword.Password)
            //{
            //    string username = txtUsername.Text;
            //    txtPassword.Clear();
            //    txtUsername.Clear();
            //    //MessageBox.Show("Gebruiker naam gevonden and pass: " + wachtwoord);
            //    HoofdMenu hoofd = new HoofdMenu(username);
            //    hoofd.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Gebruiker naam niet gevonden xxxxxxxxxxxx");
            //}
        } 
                private void Registratie_Click(object sender, RoutedEventArgs e)
                {
                Registratie registratie = new Registratie();
                registratie.ShowDialog();
                }
    }

   
}

