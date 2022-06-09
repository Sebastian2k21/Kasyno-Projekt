using Kasyno.Entities;
using System.Windows;

namespace Kasyno
{
    /// <summary>
    /// klasa okna ustawien uzytkownika
    /// </summary>
    public partial class WindowUserSettings : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public AppUser User { get; set; }

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="user">zalogowany uzytkownik</param>
        public WindowUserSettings(AppUser user)
        {
            InitializeComponent();
            User = user;

            LblLogin.Text = User.Login;
        }

        /// <summary>
        /// po kliknieciu przycisku Password (otwiera okno do nowego hasla)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPassword_Click(object sender, RoutedEventArgs e)
        {
            WindowNewPassword window = new WindowNewPassword(User);
            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

        /// <summary>
        /// po kliknieciu przycisku Deposit (otwiera okno do depozytu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeposit_Click(object sender, RoutedEventArgs e)
        {
            WindowDeposit window = new WindowDeposit(User);
            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
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
