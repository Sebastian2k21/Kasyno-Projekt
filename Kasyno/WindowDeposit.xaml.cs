using Kasyno.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace Kasyno
{
    public partial class WindowDeposit : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public AppUser User { get; set; }
        public WindowDeposit(AppUser user)
        {
            InitializeComponent();

            User = user;
        }

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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
