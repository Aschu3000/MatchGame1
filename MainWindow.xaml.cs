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

namespace MatchGame1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetUpGame();
        }

        private void SetUpGame()
        {
            //create a list of 8 emoji pairs
            List<string> animalEmoji = new List<string>()
            {
                "🐻", "🐻",
                "🐸", "🐸",
                "🐷", "🐷",
                "🦝", "🦝",
                "🦁", "🦁",
                "💩", "💩",
                "🐶", "🐶",
                "👽", "👽",
            };

            //create a new random num generator
            Random random = new Random();

            //find every textBlick in main grid and repeat following statements for each
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                int index = random.Next(animalEmoji.Count); //pick random num 0 and num if emoji left in list and call it index
                string nextEmoji = animalEmoji[index]; //use reandom num called index to get random emoji from list
                textBlock.Text = nextEmoji; //update the textBlock with random emoji from list
                animalEmoji.RemoveAt(index); //remove random emoji from the list
            }
        }
    }
}
