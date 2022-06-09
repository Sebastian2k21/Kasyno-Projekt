using Kasyno.Entities;
using Kasyno.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace Kasyno
{
    /// <summary>
    /// klasa okna slotow
    /// </summary>
    public partial class WindowSlot : Window, IGame, IStatistics
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public AppUser User { get; set; }

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="user"></param>
        public WindowSlot(AppUser user)
        {
            User = user;
  
            InitializeComponent();
            LblMoney.Content = $"{User.AppUserDetails.Balance}$";
        }

        #region Interface implementation
        public void Play()
        {
            Random rnd = new Random();

            int left = rnd.Next(1, 10);
            int center = rnd.Next(1, 10);
            int right = rnd.Next(1, 10);

            LblLeft.Content = left.ToString();
            LblRight.Content = right.ToString();
            LblCenter.Content = center.ToString();

            double bet = Convert.ToDouble(TbBet.Text);
            bool isGameWon = IsGameWon(left, center, right);

            if (isGameWon)
            {
                User.AppUserDetails.Balance = User.AppUserDetails.Balance + (bet * 10);
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
                GameId = 2,
                BetAmount = bet,
                IsWon = isGameWon,
            };

            context.History.Add(history);
            context.SaveChanges();
            LblMoney.Content = $"{User.AppUserDetails.Balance}$";
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
            WindowStatistics window = new WindowStatistics(User, 2);
            this.Hide();
            window.ShowDialog();
            this.ShowDialog();       
        }

        #endregion

        #region Buttons
        /// <summary>
        /// po kliknieciu przycisku Menu (otwiera sie okno z autorem)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            WindowAuthor window = new WindowAuthor();

            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

        /// <summary>
        ///  po kliknieciu przycisku Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// po kliknieciu przycisku Statistics 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void MenuStatistics_Click(object sender, RoutedEventArgs e)
        {
            ShowStatistics();
        }

        /// <summary>
        /// po kliknieciu przycisku Play (zaczyna sie gra jesli uzytkownik ma pieniadze)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (CheckUserBalance()) // if entered value was correct and user has enough money
            {
                Play();
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

        #endregion

        #region Other methods

        /// <summary>
        /// sprawdza czy uzytkownik wygral czy nie
        /// </summary>
        /// <param name="left">lewa liczba</param>
        /// <param name="center">srodkowa liczba</param>
        /// <param name="right">prawa liczba</param>
        /// <returns>true jesli uzytkownik wygral, false w przeciwnym azie</returns>
        public bool IsGameWon(int left, int center, int right)
        {
            if (left == right && left == center)
            {
                return true;
            }
            return false;
        }

        #endregion

    }
}
