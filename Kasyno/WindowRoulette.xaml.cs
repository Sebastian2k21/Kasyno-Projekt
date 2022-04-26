using Kasyno.Entities;
using Kasyno.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace Kasyno
{
    public partial class WindowRoulette : Window, IGame, IStatistics
    {
        private const string GAME_NAME = "Roulette";

        private const string LOG_FILE_NAME = "logs.txt";
        public User User1 { get; set; }
        public List<GameLog> Logs { get; set; }
        public AppUser User { get; set; }
        public WindowRoulette(AppUser user)
        {
            User = user;
            Logs = InitializeLogs();

            InitializeComponent();
            LblMoney.Content = $"{User1.Money.ToString()}$";
        }

        #region Interface implementation

        public void Play()
        {
            int option = CheckOption(); // 0 - not selected, 1 - black, 2 - red, 3 - green

            if (option != 0) // if any option is checked (black/red/green)
            {
                Random rnd = new Random();

                int number = rnd.Next(0, 36 + 1);

                LblRoulette.Content = number.ToString();
                if (number == 0)
                {
                    LblRoulette.Foreground = Brushes.Black;
                    RctngRoulette.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                }
                else if (number % 2 == 0)
                {
                    LblRoulette.Foreground = Brushes.Black;
                    RctngRoulette.Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
                }
                else
                {
                    LblRoulette.Foreground = Brushes.White;
                    RctngRoulette.Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                }

                // checks if user won
                IsGameWon(number, option);

                SaveStatisticsToFile();
            }
        }

        public bool CheckUserBalance()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(TbBet.Text))
                {
                    throw new Exception("You have to a bet value");
                }

                if (Convert.ToDouble(TbBet.Text) <= 0) // if bet is less or equal 0
                {
                    throw new Exception("Your bet has to be higher than 0");
                }

                if (User1.Money - Convert.ToDouble(TbBet.Text) < 0)
                {
                    throw new Exception("You do not have enough money!");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Enter valid numerical value!", "Error", MessageBoxButton.OK);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }

            return true;
        }

        public void ShowStatistics()
        {
            /*WindowStatistics window = new WindowStatistics(Logs, GAME_NAME);

            this.Hide();
            window.ShowDialog();
            this.ShowDialog();*/
        }

        #endregion

        #region Class initializer methods

        /// <summary>
        /// initializes list of logs
        /// </summary>
        /// <returns>filled with values list of logs (if any log exists)</returns>
        private List<GameLog> InitializeLogs()
        {
            List<GameLog> result = new List<GameLog>();

            if (File.Exists(LOG_FILE_NAME))
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
            }

            return result;
        }

        #endregion

        #region Buttons
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            WindowAuthor window = new WindowAuthor();

            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void MenuStatistics_Click(object sender, RoutedEventArgs e)
        {
            ShowStatistics();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (CheckUserBalance()) // if entered value was correct and user has enough money
            {
                Play();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Other methods
        /// <summary>
        /// method saves all logs from Logs field to the logs.txt file
        /// </summary>
        private void SaveStatisticsToFile()
        {
            using (StreamWriter sr = new StreamWriter(LOG_FILE_NAME))
            {
                foreach (var log in Logs)
                {
                    sr.WriteLine($"{log.Username};{log.GameName};{log.BalanceBefore};{log.Bet};{log.BalanceAfter}");
                }
            }
        }

        /// <summary>
        /// checks whether user chose an option or not
        /// </summary>
        /// <returns>number of an option</returns>
        private int CheckOption()
        {
            if (RadioBlack.IsChecked == false && RadioGreen.IsChecked == false && RadioRed.IsChecked == false)
            {
                MessageBox.Show("You have to choose a color", "Error", MessageBoxButton.OK);
                return 0;
            }
            else if (RadioBlack.IsChecked == true)
            {
                return 1;
            }
            else if (RadioRed.IsChecked == true)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        /// <summary>
        /// checks whether game is won or not
        /// </summary>
        /// <param name="number">random number on roulette</param>
        /// <param name="option">color value regarding to chosen radio button</param>
        private void IsGameWon(int number, int option)
        {
            bool isWon = false;
            double bet = Convert.ToDouble(TbBet.Text);

            if (number == 0 && option == 3) // green
            {
                bet = bet * 6;
                isWon = true;
            }
            else if (number % 2 == 0 && option == 2) // black
            {
                bet = bet * 2;
                isWon = true;
            }
            else if (number % 2 == 1 && option == 1) // red
            {
                bet = bet * 2;
                isWon = true;
            }
            else
            {
                isWon = false;
            }

            double balanceBeforeGame = User1.Money;

            if (isWon)
            {
                User1.Money = User1.Money + bet;
                MessageBox.Show($"You have won {bet}$!", "WIN", MessageBoxButton.OK);
            }
            else
            {
                User1.Money = User1.Money - bet;
                MessageBox.Show($"You have lost {bet}$!", "LOSE", MessageBoxButton.OK);
            }

            Logs.Add(new GameLog(User1.Username, "Roulette", balanceBeforeGame, bet, User1.Money));

            LblMoney.Content = $"{User1.Money.ToString()}$";
        }

        #endregion
    }
}
