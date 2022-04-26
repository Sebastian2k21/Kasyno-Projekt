using Kasyno.Entities;
using Kasyno.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace Kasyno
{
    public partial class WindowLogin : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public WindowLogin()
        {
            InitializeComponent();
        }

        private bool AreFieldsEmpty(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
                return true;

            return false;
        }

        private bool ValidatePassword(AppUser user, string enteredPassword)
        {
            string hashedPassword = Encryptor.Sha256(enteredPassword);

            if (user.Passsword == hashedPassword)
            {
                return true;
            }

            return false;
        }

        private AppUser Login(string username, string password)
        {
            if (AreFieldsEmpty(username, password))
            {
                MessageBox.Show($"You have to fill all the fields!", "Error", MessageBoxButton.OK);
                return null;
            }

            AppUser user = context.Users.Include(u => u.AppUserDetails)
                            .FirstOrDefault(u => u.Login == username);

            if (user == null)
            {
                MessageBox.Show($"There is no such a user!", "Error", MessageBoxButton.OK);
                return null;
            }

            if (ValidatePassword(user, password))
            {
                return user;
            }
            else
            {
                MessageBox.Show($"Wrong password!!", "Error", MessageBoxButton.OK);
            }

            return null;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TbLogin.Text;
            string password = TbPassword.Password;

            AppUser user = Login(username, password);

            if (user != null)
            {
                WindowEntry window = new WindowEntry(user);
                this.Hide();
                window.ShowDialog();
                this.ShowDialog();
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            WindowRegister window = new WindowRegister();
            this.Hide();
            window.ShowDialog();
            this.Show();
        }
    }
}
