using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharpbeg
{
    public partial class Form1 : Form
    {

        // below is the player class which has two value
        // X and O
        // by doing this we can control the player and the AI symbols
        public enum Player
        {
            X, O
        }

        Player currentPlayer; //calling the player class

        List<Button> buttons; //creating a LIST or array of buttons
        Random rand = new Random(); //importing the random number generator class
        int playerWins = 0; //set the player win integer to 0
        int computerWins = 0; //set the computer win integer to 0

        public Form1()
        {
            InitializeComponent();
            resetGame();
        }

        private void playerClick(object sender, EventArgs e)
        {
            var button = (Button)sender; //find which button was clicked
            currentPlayer = Player.X; // set the player to X
            button.Text = currentPlayer.ToString(); //change the button text to player X
            button.Enabled = false; // disable the button
            button.BackColor = System.Drawing.Color.Cyan; //change the player color
            buttons.Remove(button); // remove the button from the list buttons so AI cant click again
            Check(); // check player win
            AImoves.Start(); //start the AI timer
        }

        private void AImove(object sender, EventArgs e)
        {
            if (buttons.Count > 0)
            {
                int index = rand.Next(buttons.Count);

                buttons[index].Enabled = false;

                currentPlayer = Player.O;
                buttons[index].Text = currentPlayer.ToString();
                buttons[index].BackColor = System.Drawing.Color.DarkSalmon;

                buttons.RemoveAt(index);
                Check();
                AImoves.Stop();
            }
        }

        private void restartGame(object sender, EventArgs e)
        {
            //function for resetting the game board with the created 
            // restart button
            resetGame();
        }

        private void loadbuttons()
        {
            // adds all created form buttons into a buttons list
            buttons = new List<Button> { button1, button2, button3, button4,
                button5, button6, button7, button8, button9 };
        }

        private void resetGame()
        {
            // reset function
            foreach(Control X in this.Controls)
            {
                if (X is Button && X.Tag == "play")
                {
                    ((Button)X).Enabled = true; //change them all back to enabled or clickable
                    ((Button)X).Text = " "; // set the text to question marks
                    ((Button)X).BackColor = default(Color); // change the background color to default colors
                }
            }

            loadbuttons(); //run the load buttons function so all the buttons are inserted back in the array
        }

        private void Check()
        {
            // in this function we will check if the player or the AI has won
            // 2 if statements, 1 for 'O' checks(player win), 1 for 'X' checks(AI wins)
            if (button1.Text == "X" && button2.Text == "X" && button3.Text == "X"
                || button4.Text == "X" && button5.Text == "X" && button6.Text == "X"
                || button7.Text == "X" && button8.Text == "X" && button9.Text == "X"
                || button2.Text == "X" && button4.Text == "X" && button7.Text == "X"
                || button1.Text == "X" && button5.Text == "X" && button8.Text == "X"
                || button3.Text == "X" && button6.Text == "X" && button9.Text == "X"
                || button2.Text == "X" && button5.Text == "X" && button9.Text == "X"
                || button3.Text == "X" && button5.Text == "X" && button7.Text == "X")
            {
                // if any of the above conditions are met
                AImoves.Stop();
                MessageBox.Show("You Win");
                playerWins++;
                label1.Text = "Player Wins- " + playerWins;
                resetGame();
            }

            else if(button1.Text == "O" && button2.Text == "O" && button3.Text == "O"
                || button4.Text == "O" && button5.Text == "O" && button6.Text == "O"
                || button7.Text == "O" && button8.Text == "O" && button9.Text == "O"
                || button2.Text == "O" && button4.Text == "O" && button7.Text == "O"
                || button1.Text == "O" && button5.Text == "O" && button8.Text == "O"
                || button3.Text == "O" && button6.Text == "O" && button9.Text == "O"
                || button2.Text == "O" && button5.Text == "O" && button9.Text == "O"
                || button3.Text == "O" && button5.Text == "O" && button7.Text == "O")
            {
                AImoves.Stop();
                MessageBox.Show("I Win");
                computerWins++;
                label2.Text = "AI Wins- " + computerWins;
                resetGame();
            }
        }
    }
}
