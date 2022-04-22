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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
