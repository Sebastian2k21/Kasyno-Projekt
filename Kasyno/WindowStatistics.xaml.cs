using Kasyno.Entities;
using Kasyno.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Kasyno
{
    /// <summary>
    /// klasa okna statystyk
    /// </summary>
    public partial class WindowStatistics : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public List<History> Logs { get; set; }
        public AppUser User { get; set; }

        // gameId = 0 -> Both
        // gameId = 1 -> Roulette
        // gameId = 2 -> Slots

        /// <summary>
        /// konstruktor
        /// </summary>
        /// <param name="user"></param>
        /// <param name="gameId"></param>
        public WindowStatistics(AppUser user, int gameId)
        {
            InitializeComponent();
            User = user;
            InitializeLogs(gameId);

            List<StatisticsData> newLogs = new List<StatisticsData>();

            for (int i = 0; i < Logs.Count; i++)
            {
                var newLog = new StatisticsData() // jeden wiersz z kolumny
                {
                    WasGameWon = Logs[i].IsWon ? "Won" : "Lost",
                    BetAmount = Logs[i].BetAmount,
                    Game = context.Games.Where(g => g.Id == Logs[i].GameId)
                        .Select(g => g.Name)
                        .ToList()[0]
                };

                newLogs.Add(newLog);
            }

            DGdata.ItemsSource = newLogs;
        }

        /// <summary>
        /// inicjalizuje rekordy z bazy danych
        /// </summary>
        /// <param name="gameId"></param>
        private void InitializeLogs(int gameId)
        {
            Logs = new List<History>();

            if (gameId == 0)
            {
                Logs = context.History.Include(h => h.AppUser)
                    .Where(h => h.AppUserId == User.Id).ToList();
            }
            else
            {
                Logs = context.History.Include(h => h.AppUser)
                    .Where(h => h.AppUserId == User.Id && h.GameId == gameId).ToList();
            } 
        }
    }
}
