using System.Windows;

namespace Kasyno
{
    public partial class WindowLogin : Window
    {
        public WindowLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // walidacja uzytkownika 
            // do glownego menu
            WindowLogin window = new WindowLogin();
            this.Hide();
            window.ShowDialog();
            this.Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            WindowRegister window = new WindowRegister();
            this.Hide();
            window.ShowDialog();
            this.Close();
        }
    }
}
