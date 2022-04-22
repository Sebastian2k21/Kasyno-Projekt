using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class WindowStatistics : Window
    {
        List<GameLog> Logs { get; set; }
        public WindowStatistics(List<GameLog> logs, string gameName)
        {
            InitializeComponent();
            InitializeLogs(logs, gameName);

            DGdata.ItemsSource = Logs;
        }

        /// <summary>
        /// initializes logs to display
        /// </summary>
        /// <param name="logs">list of logs</param>
        /// <param name="gameName">name of game</param>
        private void InitializeLogs(List<GameLog> logs, string gameName)
        {
            List<GameLog> slotLogs = new List<GameLog>();
            
            if(gameName == "Entrance")
            {
                slotLogs = logs;
            }
            else
            {
                foreach (var log in logs)
                {
                    if (log.GameName == gameName)
                    {
                        slotLogs.Add(log);
                    }
                }
            }

            Logs = slotLogs;
        }
    }
}
