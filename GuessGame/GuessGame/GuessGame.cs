using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessGame
{
    class GuessGame
    {
        private Random rand;
        private string[] phrases =
        {
            "You gonna do it!",
            "Everything is fine!",
            "You're doing great!",
            "You're moving in the right direction!",
            "Don't give up!"
        };
        public string Phrase
        {
            get => phrases[rand.Next(0, phrases.Length)];
        }

        static void Main(string[] args)
        {
            var guessGame = new GuessGame();
            guessGame.Start(guessGame);
        }

        public GuessGame()
        {
            rand = new Random((int)DateTime.Now.Ticks);
        }

        public void Start(GuessGame guessGame)
        {
            StringBuilder history = new StringBuilder("Your history:\n");

            Console.Write("Write yout name, please: ");
            var name = Console.ReadLine();
            Console.WriteLine("Hi, {0}! You will play a simple guess game. Rules are simple: I picked a number from 0 to 50, you just need to guess it!", name);

            var startTime = DateTime.Now;

            var num = rand.Next(0, 51);
            Console.Write("The game started! Make a guess:");

            var guess = 0;
            var iter = 0;

            var response = Console.ReadLine();
            if (response.Equals("q"))
            {
                guessGame.Finish(false, startTime, iter, history.ToString(), num);
            }
            else
            {
                try
                {
                    guess = Int32.Parse(response);
                }
                catch (Exception)
                {
                    Console.WriteLine("It seems that it is not a number, so I'll set it to 50");
                    guess = 50;
                }
                iter++;
            }

            while(guess != num)
            {
                if (guess < num && guess >= 0)
                {
                    Console.Write("Yout guess is less. ");
                    history.Append($"Iteration:{iter} Guess:{guess} Less\n");
                }
                else if (guess > num && guess <= 50)
                {
                    Console.Write("Yout guess is bigger. ");
                    history.Append($"Iteration:{iter} Guess:{guess} Bigger\n");
                }
                else
                {
                    Console.Write("Ooopsey, your guess is out of bounds, it is from 0 to 50, you remember, right? ");
                    history.Append($"Iteration:{iter} Guess:{guess} Out of bounds\n");
                }
                if (iter % 4 == 0)
                {
                    Console.WriteLine(guessGame.Phrase);
                }
                iter++;

                Console.Write("Please, make a guess: ");

                response = Console.ReadLine();
                if (response.Equals("q"))
                {
                    guessGame.Finish(false, startTime, iter, history.ToString(), num);
                }
                else
                {
                    try
                    {
                        guess = Int32.Parse(response);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("It seems that it is not a number, so I'll set it to 50");
                        guess = 50;
                    }
                }
            }
            history.Append($"Iteration:{iter} Guess:{guess} RIGHT\n");
            guessGame.Finish(true, startTime, iter, history.ToString(), num);
        }

        private void Finish(bool guessed, DateTime startTime, int iterations, String history, int num)
        {
            var interval = DateTime.Now - startTime;

            if (guessed)
            {
                Console.WriteLine("Congratulations, you won!");
                Console.WriteLine("You made {0} guesses", iterations);
                Console.WriteLine(history);
                Console.WriteLine("The game took {0} minutes!", interval.TotalMinutes);
                Console.WriteLine("Thanks for playing! Goodbye!");
            }
            else
            {
                Console.WriteLine("Sorry, game is over, number was {0}, bye!", num);
            }
        }
    }
}
