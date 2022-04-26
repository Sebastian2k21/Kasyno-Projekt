using Kasyno.Entities;
using System.Windows;

namespace Kasyno
{
    public partial class WindowUserSettings : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public AppUser User { get; set; }
        public WindowUserSettings(AppUser user)
        {
            InitializeComponent();
            User = user;

            LblLogin.Text = User.Login;
        }

        private void BtnPassword_Click(object sender, RoutedEventArgs e)
        {
            WindowNewPassword window = new WindowNewPassword(User);
            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

        private void BtnDeposit_Click(object sender, RoutedEventArgs e)
        {
            WindowDeposit window = new WindowDeposit(User);
            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
