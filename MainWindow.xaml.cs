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

namespace WpfApp1
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed = 0;
        int matchesFound = 0;

        public MainWindow()
        {
            InitializeComponent();
            timeLabel.Content = "0";
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            timer.Start();
            SetupGame();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLabel.Content = (tenthsOfSecondsElapsed++ / 10F).ToString("0.0s");
        }

        private void SetupGame()
        {
            List<string> lists = new List<string>()
            {
                "❤","❤",
                "🌹","🌹",
                "🌎","🌎",
                "💥","💥",
                "🎈","🎈",
                "💕","💕",
                "💎","💎",
                "🍓","🍓",
                "🐾","🐾",
                "🐢","🐢"
            };
            List<string> animalEmoji = lists;

            Random random = new Random();

            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                int index = random.Next(animalEmoji.Count);
                textBlock.Text = animalEmoji[index];
                animalEmoji.RemoveAt(index);
            }
        }

        private bool isSelecting = false;
        private TextBlock lastSelectedTextBlock;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if ( !isSelecting )
            {
                lastSelectedTextBlock = textBlock;
                textBlock.Opacity = 0.3;
                isSelecting = true;
            }
            else
            {
                if ( lastSelectedTextBlock.Text == textBlock.Text )
                {
                    lastSelectedTextBlock.Visibility = Visibility.Hidden;
                    textBlock.Visibility = Visibility.Hidden;
                    matchesFound++;

                    if ( matchesFound == 10 )
                    {
                        timer.Stop();
                    }
                } else
                {
                    lastSelectedTextBlock.Opacity = 1;
                }
                isSelecting = false;
            }
        }
    }
}
