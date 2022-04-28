using Kasyno.Entities;
using Kasyno.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Kasyno
{
    public partial class WindowRoulette : Window, IGame, IStatistics
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public AppUser User { get; set; }
        public WindowRoulette(AppUser user)
        {
            User = user;

            InitializeComponent();
            LblMoney.Content = $"{User.AppUserDetails.Balance}$";
        }

        #region Interface implementation

        public void Play()
        {
            int option = CheckOption(); // 0 - not selected, 1 - black, 2 - red, 3 - green

            if (option != 0) // if any option is checked (black/red/green)
            {
                Random rnd = new Random();

                int number = rnd.Next(0, 36 + 1);

                LblRoulette.Content = number.ToString();
                if (number == 0)
                {
                    LblRoulette.Foreground = Brushes.Black;
                    RctngRoulette.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                }
                else if (number % 2 == 0)
                {
                    LblRoulette.Foreground = Brushes.Black;
                    RctngRoulette.Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
                }
                else
                {
                    LblRoulette.Foreground = Brushes.White;
                    RctngRoulette.Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                }

                // checks if user won
                IsGameWon(number, option);
            }
        }

        public bool CheckUserBalance()
        {
            User = context.Users.Include(u => u.AppUserDetails).FirstOrDefault(u => u.Id == User.Id);

            if (String.IsNullOrWhiteSpace(TbBet.Text))
            {
                MessageBox.Show("Bet field cannot be empty!", "Error", MessageBoxButton.OK);
                return false;
            }

            double betAmount = 0;
            if (!Double.TryParse(TbBet.Text, out betAmount))
            {
                MessageBox.Show("Bet field must be filled with numerical value!", "Error", MessageBoxButton.OK);
                return false;
            }

            if (betAmount <= 0)
            {
                MessageBox.Show("You cannot bet less or equal 0$", "Error", MessageBoxButton.OK);
                return false;
            }

            if (User.AppUserDetails.Balance - betAmount < 0)
            {
                MessageBox.Show("You do not have enough money!", "Error", MessageBoxButton.OK);
                return false;
            }

            return true;
        }

        public void ShowStatistics()
        {
            WindowStatistics window = new WindowStatistics(User, 1);
            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

        #endregion

        #region Buttons
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            WindowAuthor window = new WindowAuthor();

            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void MenuStatistics_Click(object sender, RoutedEventArgs e)
        {
            ShowStatistics();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (CheckUserBalance()) // if entered value was correct and user has enough money
            {
                Play();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Other methods

        /// <summary>
        /// checks whether user chose an option or not
        /// </summary>
        /// <returns>number of an option</returns>
        private int CheckOption()
        {
            if (RadioBlack.IsChecked == false && RadioGreen.IsChecked == false && RadioRed.IsChecked == false)
            {
                MessageBox.Show("You have to choose a color", "Error", MessageBoxButton.OK);
                return 0;
            }
            else if (RadioBlack.IsChecked == true)
            {
                return 1;
            }
            else if (RadioRed.IsChecked == true)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        /// <summary>
        /// checks whether game is won or not
        /// </summary>
        /// <param name="number">random number on roulette</param>
        /// <param name="option">color value regarding to chosen radio button</param>
        private void IsGameWon(int number, int option)
        {
            bool isWon = false;
            double bet = Convert.ToDouble(TbBet.Text);

            if (number == 0 && option == 3) // green
            {
                bet = bet * 6;
                isWon = true;
            }
            else if (number % 2 == 0 && option == 2) // black
            {
                bet = bet * 2;
                isWon = true;
            }
            else if (number % 2 == 1 && option == 1) // red
            {
                bet = bet * 2;
                isWon = true;
            }
            else
            {
                isWon = false;
            }

            double balanceBeforeGame = User.AppUserDetails.Balance;

            if (isWon)
            {
                User.AppUserDetails.Balance = User.AppUserDetails.Balance + bet;
                MessageBox.Show($"You have won {bet}$!", "WIN", MessageBoxButton.OK);
            }
            else
            {
                User.AppUserDetails.Balance = User.AppUserDetails.Balance - bet;
                MessageBox.Show($"You have lost {bet}$!", "LOSE", MessageBoxButton.OK);
            }

            context.Users.Update(User);
            context.SaveChanges();
            User = context.Users.Include(u => u.AppUserDetails).FirstOrDefault(u => u.Id == User.Id);

            History history = new History()
            {
                AppUserId = User.Id,
                GameId = 1,
                BetAmount = bet,
                IsWon = isWon,
            };

            context.History.Add(history);
            context.SaveChanges();
            LblMoney.Content = $"{User.AppUserDetails.Balance}$";
        }

        #endregion
    }
}
