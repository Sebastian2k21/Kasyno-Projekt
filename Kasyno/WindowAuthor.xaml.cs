using System.Windows;

namespace Kasyno
{
    /// <summary>
    /// klasa okna autora
    /// </summary>
    public partial class WindowAuthor : Window
    {

        /// <summary>
        /// konstrutkro
        /// </summary>
        public WindowAuthor()
        {
            InitializeComponent();
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
