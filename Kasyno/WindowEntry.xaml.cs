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
    public partial class WindowEntry : Window, IStatistics
    {
        private const string GAME_NAME = "Entrance";

        private const string LOG_FILE_NAME = "logs.txt";
        public User User1 { get; set; }
        public bool IsSet { get; set; }

        public List<GameLog> Logs { get; set; }
        public WindowEntry()
        {
            InitializeComponent();
            Logs = InitializeLogs();
            User1 = new User();
            IsSet = false;
        }

        public void ShowStatistics()
        {
            WindowStatistics window = new WindowStatistics(Logs, GAME_NAME);

            this.Hide();
            window.ShowDialog();
            this.ShowDialog();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // reading field values
                string name = TbUsername.Text;
                double deposit = Convert.ToDouble(TbDeposit.Text);

                if (String.IsNullOrWhiteSpace(name)) // if username field is empty
                {
                    throw new Exception("You have to enter username!");
                }
                else if(name.Length < 3) // if username does not contain 3 characters
                {
                    throw new Exception("Your nickname has to contain at least 3 characters");
                }

                if(deposit <= 0) // if deposit is less or equal 0
                {
                    throw new Exception("Your deposit has to be higher than 0");
                }

                if(IsSet == false)
                {
                    // assigning values from textboxes to User object
                    User1.Money = deposit;
                    User1.Username = name;
                    IsSet = true;
                }

                User UserCopy = User1;

                // game option
                if (BtnChoiceRoulette.IsChecked == false && BtnChoiceSlot.IsChecked == false)
                {
                    throw new ArgumentNullException();
                }
                else if(BtnChoiceSlot.IsChecked != false)
                {
                    WindowSlot window = new WindowSlot(ref UserCopy);
                    this.Hide();

                    TbDeposit.IsEnabled = false;
                    TbUsername.IsEnabled = false;
                    window.ShowDialog();

                    User1 = UserCopy;
                    TbDeposit.Text = User1.Money.ToString();

                    this.ShowDialog();
                }
                else
                {
                    WindowRoulette window = new WindowRoulette(ref UserCopy);
                    this.Hide();

                    TbDeposit.IsEnabled = false;
                    TbUsername.IsEnabled = false;

                    window.ShowDialog();

                    User1 = UserCopy;
                    TbDeposit.Text = User1.Money.ToString();

                    this.ShowDialog();
                }

            }
            catch(FormatException ex)
            {
                MessageBox.Show("Enter valid numerical value!", "Error", MessageBoxButton.OK);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("You have to choose a game!", "Error", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
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
    }
}
