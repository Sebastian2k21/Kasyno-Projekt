namespace Kasyno
{
    /// <summary>
    /// interfejs gry (ruletka/sloty)
    /// </summary>
    interface IGame
    {
        /// <summary>
        /// zaczyna gre
        /// </summary>
        void Play();

        /// <summary>
        /// sprawdza czy uzytkownik ma odpowiednia ilosc piniedzy do postawienia zakladu
        /// </summary>
        /// <returns>true jesli ma wystarczajaca ilosc pieniedze, false w przeciwnym razie</returns>
        bool CheckUserBalance();
    }
}
