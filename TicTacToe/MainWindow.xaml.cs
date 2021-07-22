
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Holds the current results of cells in the game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player 1's turn (X)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        /// <summary>
        /// Starting new game
        /// </summary>
        private void NewGame()
        {
            // Create a new blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Make sure Player 1 starts the game
            mPlayer1Turn = true;

            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Changing background, foreground and content to the default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Ensuring the game is still going
            mGameEnded = false;
        }

        /// <summary>
        /// Button Click Event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Restarting the game
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to button
            var button = (Button)sender;

            // Find the button's position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Don't override already occupied cells
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on which player's turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // Change nought color to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            // Toggle player turns
            mPlayer1Turn ^= true;

            // Check for the winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks every possible winning combination
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins
            // Check for horizontal wins
            // Row 0:
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.LightGreen;
            }

            // Row 1:
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.LightGreen;
            }

            // Row 2:
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.LightGreen;
            }
            #endregion

            #region Vertical Wins
            // Check for vertical wins
            // Column 0:
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.LightGreen;
            }

            // Column 1:
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.LightGreen;
            }

            // Column 2:
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.LightGreen;
            }
            #endregion

            #region No Winner
            // Check for draw
            if (!mResults.Any(result => result == MarkType.Free))
            {
                // Game ends
                mGameEnded = true;

                // Highlight all cells 
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Lavender;
                });
            }
            #endregion

            #region Diagonal Wins
            // Check for diagonal wins
            // Diagonal left:
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.LightGreen;
            }

            // Diagonal right:
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.LightGreen;
            }
            #endregion
        }
    }
}
