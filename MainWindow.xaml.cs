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
using System.Windows.Threading;

namespace MatchGame1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // create a new timer and keep track of time elapsed and number of matches player has found
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();

        }

        //updates bottom textblock with the elapsed time and stops the timer once all matches found
        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play Again?";
            }
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
                if (textBlock.Name != "timeTextBlock") // so it skips the textBlock timeTextBlock
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count); //pick random num 0 and num if emoji left in list and call it index
                    string nextEmoji = animalEmoji[index]; //use reandom num called index to get random emoji from list
                    textBlock.Text = nextEmoji; //update the textBlock with random emoji from list
                    animalEmoji.RemoveAt(index); //remove random emoji from the list
                }
            }
               
            //to start the timer and reset the fields
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }
        
        TextBlock lastTextBlockClicked;
        private bool findingMatch = false; //keeps track of whther player clicked on first animal in a pair
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false) //player clicked first animal in a pair, so makes that animal invisible and keeps track of text block if it needs to be visible again
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text) //player found match so makes the pair invisible and resets findingMatch to the next animal clicked
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else // player clicked animal that doesn't match, makes first animal visible again and resets findaingMatch
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //resets the game if all 8 matches are found
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
