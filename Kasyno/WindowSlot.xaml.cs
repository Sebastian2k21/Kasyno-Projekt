using Kasyno.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kasyno
{
    public partial class WindowSlot : Window, IGame, IStatistics
    {
        private const string GAME_NAME = "Slot";

        private const string LOG_FILE_NAME = "logs.txt";
        public User User1 { get; set; }
        public List<GameLog> Logs { get; set; }
        public WindowSlot(ref User user1)
        {
            Logs = InitializeLogs();
            User1 = user1;

            InitializeComponent();
            LblMoney.Content = $"{User1.Money.ToString()}$";
        }

        #region Interface implementation

        public void Play()
        {
            Random rnd = new Random();

            int left = rnd.Next(1, 10);
            int center = rnd.Next(1, 10);
            int right = rnd.Next(1, 10);

            LblLeft.Content = left.ToString();
            LblRight.Content = right.ToString();
            LblCenter.Content = center.ToString();

            double bet = Convert.ToDouble(TbBet.Text);
            double balanceBeforeGame = User1.Money;

            if(IsGameWon(left, center, right))
            {
                User1.Money = User1.Money + (bet * 10);
                MessageBox.Show($"You have won {bet}$!", "WIN", MessageBoxButton.OK);
            }
            else
            {
                User1.Money = User1.Money - bet;
                MessageBox.Show($"You have lost {bet}$!", "LOSE", MessageBoxButton.OK);
            }

            Logs.Add(new GameLog(User1.Username, GAME_NAME, balanceBeforeGame, bet, User1.Money));
            LblMoney.Content = $"{User1.Money.ToString()}$";

            SaveStatisticsToFile();
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
            WindowStatistics window = new WindowStatistics(Logs, GAME_NAME);

            this.Hide();
            window.ShowDialog();
            this.ShowDialog();       
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
        /// checks whether user won money or not
        /// </summary>
        /// <param name="left">left number</param>
        /// <param name="center">middle number</param>
        /// <param name="right">right number</param>
        /// <returns></returns>
        public bool IsGameWon(int left, int center, int right)
        {
            if (left == right && left == center)
            {
                return true;
            }
            return false;
        }

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

        #endregion

    }
}
