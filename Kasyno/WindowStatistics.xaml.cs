using Kasyno.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Kasyno
{
    public partial class WindowStatistics : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public List<History> Logs { get; set; }
        public AppUser User { get; set; }

        // gameId = 0 -> Both
        // gameId = 1 -> Roulette
        // gameId = 2 -> Slots
        public WindowStatistics(AppUser user, int gameId)
        {
            InitializeComponent();
            InitializeLogs(gameId);

            DGdata.ItemsSource = Logs;
        }

        private void InitializeLogs(int gameId)
        {
            Logs = new List<History>();

            if (gameId == 0)
            {
                Logs = context.History.Where(h => h.AppUserId == User.Id).ToList();
            }
            else
            {
                Logs = context.History.Where(h => h.AppUserId == User.Id && h.GameId == gameId).ToList();
            } 
        }
    }
}
