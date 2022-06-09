using Kasyno.Entities;
using Kasyno.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace Kasyno
{
    /// <summary>
    /// klasa okna logowania
    /// </summary>
    public partial class WindowLogin : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();

        /// <summary>
        /// konstruktor
        /// </summary>
        public WindowLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// sprawdza czy pola sa puste
        /// </summary>
        /// <param name="username">pole uzytkownika</param>
        /// <param name="password">pole z haslem</param>
        /// <returns>true jesli ktores z pol jest puste, false w przeciwnym razie</returns>
        private bool AreFieldsEmpty(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
                return true;

            return false;
        }

        /// <summary>
        /// sprawdza czy podane haslo jest dobre
        /// </summary>
        /// <param name="user">login uzytkownika</param>
        /// <param name="enteredPassword">wprowadzone haslo</param>
        /// <returns>true jesli haslo jest poprawne, false w przeciwnym razie</returns>
        private bool ValidatePassword(AppUser user, string enteredPassword)
        {
            string hashedPassword = Encryptor.Sha256(enteredPassword);

            if (user.Passsword == hashedPassword)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// metoda loguje nas do programu
        /// </summary>
        /// <param name="username">login</param>
        /// <param name="password">hasło</param>
        /// <returns>w razie sukscesu zwraca uzytkownika, w przeciwnym razie null</returns>
        private AppUser Login(string username, string password)
        {
            if (AreFieldsEmpty(username, password))
            {
                MessageBox.Show($"You have to fill all the fields!", "Error", MessageBoxButton.OK);
                return null;
            }

            AppUser user = context.Users.Include(u => u.AppUserDetails)
                            .FirstOrDefault(u => u.Login == username);

            if (user == null)
            {
                MessageBox.Show($"There is no such a user!", "Error", MessageBoxButton.OK);
                return null;
            }

            if (ValidatePassword(user, password))
            {
                return user;
            }
            else
            {
                MessageBox.Show($"Wrong password!!", "Error", MessageBoxButton.OK);
            }

            return null;
        }

        /// <summary>
        /// po kliknieciu w przycisk Login (zaczyna sie walidacja)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TbLogin.Text;
            string password = TbPassword.Password;

            AppUser user = Login(username, password);

            if (user != null)
            {
                WindowEntry window = new WindowEntry(user);
                this.Hide();
                window.ShowDialog();
                this.ShowDialog();
            }
        }

        /// <summary>
        /// po kliknieciu w przycisk Register (przejscie do okna rejestracji)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            WindowRegister window = new WindowRegister();
            this.Hide();
            window.ShowDialog();
            this.Show();
        }
    }
}
