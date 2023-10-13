using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameDB_recode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetAllGames();
            actionComboBox.SelectedIndex = 0;
        }

        private void GetAllGames()
        {
            using(AppContext db = new AppContext())
            {
                db.Games.Load();
                dataGrid.ItemsSource = db.Games.Local.ToObservableCollection();
            }
        }

        private void addExampleGames_Click(object sender, RoutedEventArgs e) {
            using (AppContext db = new AppContext()) {
                Game game1 = new Game { Title = "TES 5: Skyrim", Creator = "Bethesda", Genre = "Action RPG", Release = new DateTime(2011, 11, 11) };
                Game game2 = new Game { Title = "Witcher 3", Creator = "CD Project RED", Genre = "Action RPG", Release = new DateTime(2015, 05, 18) };
                Game game3 = new Game { Title = "Divinity Original Sin 2", Creator = "Larian Studios", Genre = "Action RPG", Release = new DateTime(2017, 09, 14) };
                Game game4 = new Game { Title = "Red Dead Redemption 2", Creator = "Rockstar Games", Genre = "Action Adventure", Release = new DateTime(2018, 10, 26) };
                Game game5 = new Game { Title = "Minecraft", Creator = "Mojang", Genre = "Sandbox", Release = new DateTime(2011, 11, 18) };
                Game game6 = new Game { Title = "Counter-Strike: Global Offensive", Creator = "Valve", Genre = "Shooter", Release = new DateTime(2012, 08, 21) };
                Game game7 = new Game { Title = "Among Us", Creator = "InnerSloth", Genre = "Social Deduction", Release = new DateTime(2018, 06, 15) };
                Game game8 = new Game { Title = "The Legend of Zelda: Breath of the Wild", Creator = "Nintendo", Genre = "Action Adventure", Release = new DateTime(2017, 03, 03) };
                Game game9 = new Game { Title = "GTA V", Creator = "Rockstar Games", Genre = "Action Adventure", Release = new DateTime(2013, 09, 17) };
                Game game10 = new Game { Title = "Fortnite", Creator = "Epic Games", Genre = "Battle Royale", Release = new DateTime(2017, 07, 25) };
                Game game11 = new Game { Title = "Overwatch", Creator = "Blizzard Entertainment", Genre = "First-Person Shooter", Release = new DateTime(2016, 05, 24), Mode = "Multiplayer" };
                Game game12 = new Game { Title = "Rocket League", Creator = "Psyonix", Genre = "Sports", Release = new DateTime(2015, 07, 07), Mode = "Multiplayer" };
                Game game13 = new Game { Title = "League of Legends", Creator = "Riot Games", Genre = "Multiplayer Online Battle Arena", Release = new DateTime(2009, 10, 27), Mode = "Multiplayer" };
                Game game14 = new Game { Title = "Dota 2", Creator = "Valve", Genre = "Multiplayer Online Battle Arena", Release = new DateTime(2013, 07, 09), Mode = "Multiplayer" };

                db.Games.Add(game1);
                db.Games.Add(game2);
                db.Games.Add(game3);
                db.Games.Add(game4);
                db.Games.Add(game5);
                db.Games.Add(game6);
                db.Games.Add(game7);
                db.Games.Add(game8);
                db.Games.Add(game9);
                db.Games.Add(game10);
                db.Games.Add(game11);
                db.Games.Add(game12);
                db.Games.Add(game13);
                db.Games.Add(game14);

                db.SaveChanges();
                GetAllGames();
            }
        }

        private void saveDatabase_Click(object sender, RoutedEventArgs e) {
            using (AppContext db = new AppContext()) {
                db.SaveChanges();
            }
        }

        private void forceUpdate_Click(object sender, RoutedEventArgs e) {
            GetAllGames();
        }

        private void info_Click(object sender, RoutedEventArgs e) {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.ShowDialog();
        }

        private void clearDatabase_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear the entire database?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes) {
                using (AppContext db = new AppContext()) {
                    db.Games.RemoveRange(db.Games);
                    db.SaveChanges();
                    GetAllGames();
                }
            }
        }


        private void executeBtn_Click(object sender, RoutedEventArgs e) {
            string selectedAction = ((ComboBoxItem)actionComboBox.SelectedItem).Content.ToString();

            using (AppContext db = new AppContext()) {
                switch (selectedAction) {
                    case "All Games":
                        GetAllGames();
                        break;
                    
                    case "Single Player Games":
                        var singlePlayerGames = db.Games.Where(g => g.Mode == "Single Player").ToList();
                        dataGrid.ItemsSource = singlePlayerGames;
                        break;

                    case "Multiplayer Games":
                        var multiplayerGames = db.Games.Where(g => g.Mode != "Single Player").ToList();
                        dataGrid.ItemsSource = multiplayerGames;
                        break;

                    case "Top Selling Game":
                        var topSellingGame = db.Games.OrderByDescending(g => g.Sales).FirstOrDefault();
                        if (topSellingGame != null) {
                            dataGrid.ItemsSource = new List<Game> { topSellingGame };
                        }
                        break;

                    case "Least Selling Game":
                        var leastSellingGame = db.Games.OrderBy(g => g.Sales).FirstOrDefault();
                        if (leastSellingGame != null) {
                            dataGrid.ItemsSource = new List<Game> { leastSellingGame };
                        }
                        break;

                    case "Top 3 Selling Games":
                        var top3SellingGames = db.Games.OrderByDescending(g => g.Sales).Take(3).ToList();
                        dataGrid.ItemsSource = top3SellingGames;
                        break;

                    case "Top 3 Least Selling Games":
                        var top3LeastSellingGames = db.Games.OrderBy(g => g.Sales).Take(3).ToList();
                        dataGrid.ItemsSource = top3LeastSellingGames;
                        break;
                }
            }
        }


        private void addBtn_Click(object sender, RoutedEventArgs e) {
            AddGameForm addGameForm = new AddGameForm();
            addGameForm.ShowDialog();
            GetAllGames();
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) {
            if (e.EditAction == DataGridEditAction.Commit) {
                var editedGame = (Game)e.Row.Item;
                using (AppContext db = new AppContext()) {
                    db.Entry(editedGame).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        private void SearchBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                string searchQuery = searchBox.Text;

                using (AppContext db = new AppContext()) {
                    var searchResult = db.Games.Where(game =>
                        game.Title.Contains(searchQuery) ||
                        game.Creator.Contains(searchQuery) ||
                        game.Genre.Contains(searchQuery) ||
                        game.Mode.Contains(searchQuery)
                    ).ToList();

                    dataGrid.ItemsSource = searchResult;
                }
            }
        }


        private void deleteBtn_Click(object sender, RoutedEventArgs e) {
            if (dataGrid.SelectedItem != null) {
                Game selectedGame = (Game)dataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the game '{selectedGame.Title}'?", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) {
                    using (AppContext db = new AppContext()) {
                        db.Games.Remove(selectedGame);
                        db.SaveChanges();
                        GetAllGames();
                    }
                }
            }
            else {
                MessageBox.Show("Select a game to delete.");
            }
        }


    }
}
