using Kasyno.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Kasyno
{
    public partial class MainWindow : Window
    {
        private readonly CasinoDbContext context = new CasinoDbContext();
        public void InitializeDatabase()
        {
            context.Users.Load();
            context.Games.Load();
            context.UsersDetails.Load();
            context.History.Load();
            Seed();
        }
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
        public MainWindow()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            InitializeComponent();

            if (context.Database.EnsureCreated())
            {
                InitializeDatabase();
            }
        }

        private void BtnGoIn_Click(object sender, RoutedEventArgs e)
        {
            WindowLogin window = new WindowLogin();
            this.Hide();
            window.ShowDialog();
            this.Close();
        }
        private void BtnAuthor_Click(object sender, RoutedEventArgs e)
        {
            WindowAuthor window = new WindowAuthor();
            this.Hide();
            window.ShowDialog();
            this.Show();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
