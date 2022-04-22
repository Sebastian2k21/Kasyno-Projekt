using Kasyno.Entities;
using Kasyno.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Kasyno
{
    public partial class WindowEntry : Window, IStatistics
    {
        private const string GAME_NAME = "Entrance";
        public AppUser User { get; set; }
        public WindowEntry(AppUser user)
        {
            InitializeComponent();
            User = user;
            LblLogin.Text = User.Login;
        }

        public void ShowStatistics()
        {
            //WindowStatistics window = new WindowStatistics(Logs, GAME_NAME);

            this.Hide();
            //window.ShowDialog();
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
                this.Show();
            }
            else
            {
                WindowRoulette window = new WindowRoulette(User);
                this.Hide();
                window.ShowDialog();
                this.Show();
            }
        }

        private void BtnStatistics_Click(object sender, RoutedEventArgs e)
        {
            ShowStatistics();
        }


        /// <summary>
        /// initializes list of logs
        /// </summary>
        /// <returns>filled with values list of logs (if any log exists)</returns>
        private List<GameLog> InitializeLogs()
        {
            List<GameLog> result = new List<GameLog>();

            /*if (File.Exists(LOG_FILE_NAME))
            {
                using (StreamReader sr = new StreamReader(LOG_FILE_NAME))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(";");

                        GameLog gl = new GameLog(values[0], values[1], Convert.ToDouble(values[2]), Convert.ToDouble(values[3]), Convert.ToDouble(values[4]));

                        result.Add(gl);
                    }
                }
            }*/

            return result;
        }

        private void ImgProfile_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WindowUserSettings window = new WindowUserSettings(User);
            this.Hide();
            window.ShowDialog();
            this.Show();
        }
    }
}
