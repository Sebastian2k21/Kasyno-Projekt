using Kasyno.Entities;
using Kasyno.Interfaces;
using System.Windows;

namespace Kasyno
{
    public partial class WindowEntry : Window, IStatistics
    {
        public AppUser User { get; set; }
        public WindowEntry(AppUser user)
        {
            InitializeComponent();
            User = user;
            LblLogin.Text = User.Login;
        }

        public void ShowStatistics()
        {
            WindowStatistics window = new WindowStatistics(User, 0);
            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

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

        private void BtnStatistics_Click(object sender, RoutedEventArgs e)
        {
            ShowStatistics();
        }

        private void ImgProfile_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowUserSettings window = new WindowUserSettings(User);
            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
