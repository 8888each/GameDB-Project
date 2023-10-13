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
using System.Windows.Shapes;

namespace GameDB_recode {
    /// <summary>
    /// Interaction logic for AddGameForm.xaml
    /// </summary>
    public partial class AddGameForm : Window {
        public AddGameForm() {
            InitializeComponent();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            string title = titleTextBox.Text;
            string creator = creatorTextBox.Text;
            string genre = genreTextBox.Text;
            DateTime releaseDate = releaseDatePicker.SelectedDate ?? DateTime.MinValue;
            string mode = modeTextBox.Text;

            Game newGame = new Game {
                Title = title,
                Creator = creator,
                Genre = genre,
                Release = releaseDate,
                Mode = mode
            };

            using (AppContext db = new AppContext()) {
                db.Games.Add(newGame);
                db.SaveChanges();
            }

            this.Close();
        }

    }
}
