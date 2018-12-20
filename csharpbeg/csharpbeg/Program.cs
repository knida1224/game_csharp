using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace csharpbeg
{
    class Program
    {
        //Function for game choices with answer retrieved in main
        static void gameChoice(string answer)
        {
            if (answer == "yes")
            {
                Console.WriteLine("\nGreat!\n");
                Console.Write("What game would you like to play?"); //ask game

                Console.Write("\n1) Hangman\n2) TicTacToe\n");

                Console.Write("\nPlease choose the number or name\nof the game you would like to play: ");

                string game = Console.ReadLine().ToLower(); //get game


                if (game == "1" || game == "hangman")
                {
                    Console.WriteLine("\nHangman it is!\n");
                    Console.WriteLine("\n________________________________________\n");
                    //create definition for hangman game
                    Hangman();
                }
                else if (game == "2" || game == "tictactoe")
                {
                    Console.WriteLine("\nTicTacToe it is!\n");
                    Console.WriteLine("\n________________________________________\n");
                    //create definition for tictactoe
                    TicTacToe();
                }
                else
                {
                    Console.Write("\nThat's not an option ");
                    Console.Write("\nWould you like to play a game? ");

                    string new_answer = Console.ReadLine().ToLower();
                    gameChoice(new_answer);
                }
            }
            else
            {
                Console.WriteLine("\nOkay, maybe next time...");
            }
        }

        //hangman function
        static void Hangman() 
        {
            Console.WriteLine("\t   Welcome to Hangman\n");

            //word bank
            IList<string> wordList = new List<string>()
            {
                "chicken",//0
                "apple",//1
                "bicycle",//2
                "playground",//3
                "speedboat",//4
                "pencil",//5
                "coffee",//6
                "home",//7
                "theatre",//8
                "football",//9
            };

            List<string> correctGuesses = new List<string>();
            List<string> incorrectGuesses = new List<string>();

            bool win = false;
            //get random int from 1-10(included) for choice of word
            Random random = new Random();
            int randomNumber = random.Next(0, 10);
            //Console.Write(randomNumber);

            //match random value with value in list 
            string secretWord = wordList[randomNumber];
            //Console.Write(secretWord+ "\n");

            //create string of "_" matching length of secret word for display
            StringBuilder maskWord = new StringBuilder(secretWord);
                  
            for (int i = 0; i < maskWord.Length; i++)
            {
                maskWord[i] = '_';
            }

            Console.WriteLine(" Let's begin!");

            drawMan(0);//draw empty board

            Console.WriteLine(" I have chosen my secret word\n");
            Console.WriteLine(" " + maskWord + "\n");

            int correctLetters = 0;
            int wrongGuess = 0;
            while (wrongGuess < 6 && win != true)
            {
                //ask for guess
                Console.Write("\n What is your letter guess?: ");
                string guess = Console.ReadLine().ToLower();
                char letter = guess[0];

                //check letter guess
                //if correct,continue
                //if incorrect, drawMan(wrongGuess+1)
                
                if (secretWord.Contains(guess))
                {
                    if (correctGuesses.Contains(guess))
                    {
                        Console.WriteLine(" You have already guessed that letter.\n");
                        continue;
                    }
                    else
                    {
                        correctGuesses.Add(guess);
                        Console.WriteLine("\n Correct!\n");
                        
                        for (int i = 0; i < secretWord.Length; i++)
                        {
                            if (secretWord[i] == letter)
                            {
                                maskWord[i] = letter;      
                                correctLetters++;
                            }
                            //Console.WriteLine(" " + maskWord + "\n");
                        }
                        Console.WriteLine(" " + maskWord + "\n");

                        Console.Write(" Would you like to guess the word?(yes or no): ");
                        string choice = Console.ReadLine().ToLower();
                        if (choice == "yes")
                        {
                            Console.Write("\n What is your word guess?: ");
                            string wordGuess = Console.ReadLine().ToLower();

                            if (wordGuess == secretWord)
                            {
                                Console.WriteLine(" Congratulations, you win!");
                                Console.WriteLine("\n ________________________________________\n");
                                win = true;
                            }
                            else
                            {
                                Console.WriteLine("\n Incorrect Word");
                                continue;
                            }
                        }
                        else if (choice == "no")
                        {
                            Console.WriteLine("\n No problem, keep playing!");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("\n That is not a choice but keep going! ");
                            continue;
                        }

                        if (correctLetters == secretWord.Length)
                        {
                            Console.WriteLine(" Congratulations, you win!");
                            Console.WriteLine("\n ________________________________________\n");
                            win = true;
                        }
                    }                                                                          
                }
                else
                {
                    if (incorrectGuesses.Contains(guess))
                    {
                        Console.WriteLine(" You have already guess that letter.\n");
                        continue;
                    }
                    else
                    {
                        incorrectGuesses.Add(guess);
                        Console.WriteLine("\n Incorrect letter\n");
                        wrongGuess++;
                        drawMan(wrongGuess);
                        Console.WriteLine(" " + maskWord + "\n");
                        if (wrongGuess == 6)
                        {
                            Console.WriteLine(" Secret word was: " + secretWord);
                            Console.WriteLine(" Game over!\n");
                        }
                    }                   
                }
                //Console.WriteLine(" " + maskWord + "\n");
            }
        }

        // Function drawing hangman depending on wrong guesses from Hangman function
        static void drawMan(int wrongGuess)
        {
            if(wrongGuess == 0)//empty board
            {
                Console.WriteLine("  +______+");
                Console.WriteLine("  |      |");
                Console.WriteLine(" [ ]     |");
                Console.WriteLine("         |");
                Console.WriteLine("         |");
                Console.WriteLine("         |");
                Console.WriteLine(" ==========\n");
            }
            else if (wrongGuess == 1)//head
            {
                Console.WriteLine("  +______+");
                Console.WriteLine("  |      |");
                Console.WriteLine(" [O]     |");
                Console.WriteLine("         |");
                Console.WriteLine("         |");
                Console.WriteLine("         |");
                Console.WriteLine(" ==========\n");
            }
            else if(wrongGuess == 2)//body
            {
                Console.WriteLine("  +______+");
                Console.WriteLine("  |      |");
                Console.WriteLine(" [O]     |");
                Console.WriteLine("  |      |");
                Console.WriteLine("         |");
                Console.WriteLine("         |");
                Console.WriteLine(" ==========\n");
            }
            else if (wrongGuess == 3)//right arm
            {
                Console.WriteLine("  +______+");
                Console.WriteLine("  |      |");
                Console.WriteLine(" [O]     |");
                Console.WriteLine(" /|      |");
                Console.WriteLine("         |");
                Console.WriteLine("         |");
                Console.WriteLine(" ==========\n");
            }
            else if (wrongGuess == 4)//left arm
            {
                Console.WriteLine("  +______+");
                Console.WriteLine("  |      |");
                Console.WriteLine(" [O]     |");
                Console.WriteLine(" /|\\     |");
                Console.WriteLine("         |");
                Console.WriteLine("         |");
                Console.WriteLine(" ==========\n");
            }
            else if (wrongGuess == 5)//right leg
            {
                Console.WriteLine("  +______+");
                Console.WriteLine("  |      |");
                Console.WriteLine(" [O]     |");
                Console.WriteLine(" /|\\     |");
                Console.WriteLine(" /       |");
                Console.WriteLine("         |");
                Console.WriteLine(" ==========\n");
            }
            else if (wrongGuess == 6)//left leg
            {
                Console.WriteLine("  +______+");
                Console.WriteLine("  |      |");
                Console.WriteLine(" [O]     |");
                Console.WriteLine(" /|\\     |");
                Console.WriteLine(" / \\     |");
                Console.WriteLine("         |");
                Console.WriteLine(" ==========\n");
            }
        }

        //TicTacToe definition
        [STAThread]
        static void TicTacToe()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }

        //Main function for testing
        static void Main(string[] args)
        {
            Console.Write("Hello, what is your name: "); //ask name
            
            string userName = Console.ReadLine(); //get name
            userName = char.ToUpper(userName[0]) + userName.Substring(1); //cap first letter

            Console.Write("\nHi " + userName + " my name is Killian, would \nyou like to play a game?(yes or no): "); //intro and ask for choice

            string answer = Console.ReadLine().ToLower(); //get choice, make lower case

            gameChoice(answer);

            Console.ReadLine();
        }
    }
}
