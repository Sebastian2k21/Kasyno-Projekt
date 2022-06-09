using Kasyno.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace Kasyno
{
    /// <summary>
    /// klasa okna depozytu
    /// </summary>
    public partial class WindowDeposit : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public AppUser User { get; set; }

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="user"></param>
        public WindowDeposit(AppUser user)
        {
            InitializeComponent();

            User = user;
        }

        /// <summary>
        /// sprawdza czy kwota wprowadzona przez uzytkownika jest poprawna
        /// </summary>
        /// <param name="value">wartosc liczbowa jako napis</param>
        /// <param name="number">zwraca wartosc przez referencje jako wartosc liczbowa (jesli jest liczba)</param>
        /// <returns>true jesli jest liczba, w przeciwnym razie false</returns>
        private bool ValidateData(string value, out double number)
        {
            number = 0;
            if (string.IsNullOrWhiteSpace(value))
            {
                MessageBox.Show($"Deposit field cannot be empty", "Error", MessageBoxButton.OK);
                return false;
            }

            if (!Double.TryParse(value, out number))
            {
                MessageBox.Show($"Deposit field must be filled with numerical value!", "Error", MessageBoxButton.OK);
                return false;
            }

            if (number <= 0)
            {
                MessageBox.Show($"You cannot deposit less or equal 0$", "Error", MessageBoxButton.OK);
                return false;
            }

            return true;
        }

        /// <summary>
        /// po kliknieciu przycisku Deposit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeposit_Click(object sender, RoutedEventArgs e)
        {
            string value = TbDeposit.Text;
            double number = 0;

            if (ValidateData(value, out number))
            {
                AppUser user = context.Users.Include(u => u.AppUserDetails)
                            .FirstOrDefault(u => u.Id == User.Id);

                user.AppUserDetails.Balance += number;

                context.Users.Update(user);
                context.SaveChanges();

                MessageBox.Show($"You stored money sucessfully.\nNow your balance is {User.AppUserDetails.Balance + number}", "Info", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// po kliknieciu przycisku Back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
