using Kasyno.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Kasyno
{
    /// <summary>
    /// klasa głównego okna
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();

        /// <summary>
        /// inicjalizacja bazy danych
        /// </summary>
        public void InitializeDatabase()
        {
            context.Users.Load();
            context.Games.Load();
            context.UsersDetails.Load();
            context.History.Load();
            Seed();
        }

        /// <summary>
        /// zapelnia baze danych
        /// </summary>
        public void Seed()
        {
            #region Games
            if (!context.Games.Any())
            {
                List<Game> Games = new List<Game>();

                Game p1 = new Game()
                {
                    Name = "Roulette",
                };
                Game p2 = new Game()
                {
                    Name = "Slots",
                };

                Games.Add(p1);
                Games.Add(p2);

                context.Games.AddRange(Games);
                context.SaveChanges();

            }
            #endregion
        }

        /// <summary>
        /// konstruktor
        /// </summary>
        public MainWindow()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            InitializeComponent();

            if (context.Database.EnsureCreated())
            {
                InitializeDatabase();
            }
        }

        /// <summary>
        /// klikniecie przycisku Go
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGoIn_Click(object sender, RoutedEventArgs e)
        {
            WindowLogin window = new WindowLogin();
            this.Hide();
            window.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// klikniecie przycisku Author
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAuthor_Click(object sender, RoutedEventArgs e)
        {
            WindowAuthor window = new WindowAuthor();
            this.Hide();
            window.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// klikniecie przycisku Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
