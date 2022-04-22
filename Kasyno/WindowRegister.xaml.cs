using Kasyno.Entities;
using Kasyno.Helpers;
using System;
using System.Linq;
using System.Windows;

namespace Kasyno
{
    public partial class WindowRegister : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public WindowRegister()
        {
            InitializeComponent();
        }

        private bool DoesLoginExist(string username)
        {
            var user = context.Users.FirstOrDefault(u => u.Login == username); // predicate -> wyrazenie, ktore zwraca true lub false

            if (user != null)
                return true;

            return false;
        }

        private bool AreFieldsEmpty(string username, string password, string confirmPassword, string firstName, string surname, string initialDeposit)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(surname) ||
                string.IsNullOrWhiteSpace(initialDeposit))
                return true;

            return false;
        }

        private bool ValidateData(string username, string password, string confirmPassword, string firstName, string surname, string initialDeposit)
        {
            if (DoesLoginExist(username))
            {
                MessageBox.Show($"Username already exist!", "Error", MessageBoxButton.OK);
                return false;
            }

            if (AreFieldsEmpty(username, password, confirmPassword, firstName, surname, initialDeposit))
            {
                MessageBox.Show($"You have to fill all the fields!!", "Error", MessageBoxButton.OK);
                return false;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show($"Password do not match!", "Error", MessageBoxButton.OK);
                return false;
            }

            double number = 0;
            if (!Double.TryParse(initialDeposit, out number))
            {
                MessageBox.Show($"Deposit field must be filled with numerical value!", "Error", MessageBoxButton.OK);
                return false;
            }

            if (number < 0)
            {
                MessageBox.Show($"Initial deposit cannot be less than 0$", "Error", MessageBoxButton.OK);
                return false;
            }


            return true;
        }

        private void Register(string username, string hashedPassword, string firstName, string surname, double initialDeposit)
        {
            AppUser user = new AppUser()
            {
                Login = username,
                Passsword = hashedPassword,
                AppUserDetails = new AppUserDetails()
                {
                    FirstName = firstName,
                    Surname = surname,
                    Balance = initialDeposit
                },
            };

            context.Users.Add(user);
            context.SaveChanges();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = TbUsername.Text;
            string password = TbPassword.Text;
            string confirmPassword = TbConfirmPassword.Text;
            string firstName = TbFirstName.Text;
            string surname = TbSurname.Text;
            string initialDeposit = TbInitialDeposit.Text;

            if (ValidateData(username, password, confirmPassword, firstName, surname, initialDeposit))
            {
                string hashedPassword = Encryptor.Sha256(password);
                double deposit = Convert.ToDouble(initialDeposit);
                Register(username, hashedPassword, firstName, surname, deposit);
            }
        }
    }
}
