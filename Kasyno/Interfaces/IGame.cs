using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kasyno
{
    interface IGame
    {
        /// <summary>
        /// game starts here
        /// </summary>
        void Play();

        /// <summary>
        /// checks whether user entered valid bet or not and if user has enough amount of money
        /// </summary>
        /// <returns>true if bet is valid, false otherwise</returns>
        bool CheckUserBalance();
    }
}
