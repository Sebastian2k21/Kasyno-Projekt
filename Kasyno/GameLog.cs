using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kasyno
{
    public class GameLog
    {
        public string Username { get; set; }
        public string GameName { get; set; }
        public double BalanceBefore { get; set; }
        public double Bet { get; set; }
        public double BalanceAfter { get; set; }

        public GameLog(string username, string gamename, double before, double bet, double after)
        {
            Username = username;
            GameName = gamename;
            BalanceBefore = before;
            Bet = bet;
            BalanceAfter = after;
        }

    }
}
