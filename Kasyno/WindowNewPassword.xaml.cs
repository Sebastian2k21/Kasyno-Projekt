using Kasyno.Entities;
using Kasyno.Helpers;
using System.Linq;
using System.Windows;

namespace Kasyno
{
    public partial class WindowNewPassword : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public AppUser User { get; set; }
        public WindowNewPassword(AppUser user)
        {
            InitializeComponent();

            User = user;
        }

        private bool ValidateData(string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show($"You have to fill both fields!", "Error", MessageBoxButton.OK);
                return false;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show($"Password must be the same!", "Error", MessageBoxButton.OK);
                return false;
            }

            return true;
        }

        private void BtnSetPassword_Click(object sender, RoutedEventArgs e)
        {
            string password = TbPassword.Password;
            string confirmPassword = TbPasswordConfirm.Password;

            if (ValidateData(password, confirmPassword))
            {
                AppUser user = context.Users.FirstOrDefault(u => u.Id == User.Id);

                user.Passsword = Encryptor.Sha256(password);

                context.Users.Update(user);
                context.SaveChanges();

                MessageBox.Show($"You have sucessfully updated your password!", "Info", MessageBoxButton.OK);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
