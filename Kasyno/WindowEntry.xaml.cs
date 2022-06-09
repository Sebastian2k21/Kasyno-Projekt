using Kasyno.Entities;
using Kasyno.Interfaces;
using System.Windows;

namespace Kasyno
{
    /// <summary>
    /// klasa okna wejscia
    /// </summary>
    public partial class WindowEntry : Window, IStatistics
    {
        public AppUser User { get; set; }

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="user"></param>
        public WindowEntry(AppUser user)
        {
            InitializeComponent();
            User = user;
            LblLogin.Text = User.Login;
        }

        /// <summary>
        /// otwiera okno statystyk
        /// </summary>
        public void ShowStatistics()
        {
            WindowStatistics window = new WindowStatistics(User, 0);
            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

        /// <summary>
        /// po kliknieciu przycisku Start (sprawdzamy czy ktos wybral gre i przechodzimy do odpowiedniego okna)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (User.AppUserDetails.Balance <= 0) // if deposit is less or equal 0
            {
                MessageBox.Show("You cannot play any game with negative balance!", "Error", MessageBoxButton.OK);
                return;
            }

            // game option
            if (BtnChoiceRoulette.IsChecked == false && BtnChoiceSlot.IsChecked == false)
            {
                MessageBox.Show("You have to choose a game!", "Error", MessageBoxButton.OK);
                return;
            }
            else if(BtnChoiceSlot.IsChecked != false)
            {
                WindowSlot window = new WindowSlot(User);
                this.Hide();
                window.ShowDialog();
                this.ShowDialog();
            }
            else
            {
                WindowRoulette window = new WindowRoulette(User);
                this.Hide();
                window.ShowDialog();
                this.ShowDialog();
            }
        }

        /// <summary>
        /// po kliknieciu przycisku Statistics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStatistics_Click(object sender, RoutedEventArgs e)
        {
            ShowStatistics();
        }

        /// <summary>
        /// po kliknieciu w obrazek profilowy, przechodzimy do okna z szczegolami uzytkownika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgProfile_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowUserSettings window = new WindowUserSettings(User);
            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

        /// <summary>
        /// po kliknieciu Logout - wyloguje nas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
