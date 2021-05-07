using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZnakeGame
{
    public partial class Form1 : Form
    {
        //NOTENOTE: GÖR DESSA TILL FÄLT SEDAN
        //Will be the "snake body"
        private List<Circle> Snake = new List<Circle>();
        //Will be the "food"
        private Circle food = new Circle();

        public Form1()
        {
            InitializeComponent();

            //Links to the Class "Settings"
            new Settings();

            //The timers settings
            //NOTE: Hur fort vet blir en "ny bild", kan ses som hastigheten
            gameTimer.Interval = 1000 / Settings.Speed;
            //Linking updateScreen funtion to the timer
            gameTimer.Tick += updateScreen;
            //Starting the timer
            gameTimer.Start();

            //Makes the game start
            startGame();
        }

        //the function that will start the game
        private void startGame()
        {
            //Makes the lable3 (GameOver) test invisible
            label3.Visible = false;

            //"Restarts" the settings
            new Settings();

            //Clear the previous snake body
            Snake.Clear();

            //Creating a new snake head
            Circle head = new Circle { X = 10, Y = 5 };
            //Adding it to the snake array
            Snake.Add(head);


            label2.Text = Settings.Score.ToString();

            //Run the food function
            generateFood();
        }

        private void updateScreen(object sender, EventArgs e)
        {
            //Timers updatefunction
            //The ticks will run its function
            if(Settings.GameOver == true)
            {
                //If the game is over and the player presses "Enter" the game will run
                if (Input.KeyPress(Keys.Enter))
                {
                    startGame();
                }
            }
            else
            {
                //Makes the code follow which key you press
                if (Input.KeyPress(Keys.Right) && Settings.direction != Directions.Left)
                {
                    Settings.direction = Directions.Right;
                }
                else if (Input.KeyPress(Keys.Left) && Settings.direction != Directions.Right)
                {
                    Settings.direction = Directions.Left;
                }
                else if (Input.KeyPress(Keys.Up) && Settings.direction != Directions.Down)
                {
                    Settings.direction = Directions.Up;
                }
                else if (Input.KeyPress(Keys.Down) && Settings.direction != Directions.Up)
                {
                    Settings.direction = Directions.Down;
                }

                //Runs the movePlayer function
                movePlayer();

            }
            //Refreshing the picture box everytime something changes...
            //...so it looks like the snake is moving.
            pbCanvas.Invalidate();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            //When the down key is pressed, it will trigger the change state
            Input.changeState(e.KeyCode, true);
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            //When the up key is pressed it will trigger the change state
            Input.changeState(e.KeyCode, false);
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            //Creating a new graphics called canvas
            Graphics canvas = e.Graphics;

            //If the game is not over then this if statment will run
            if(Settings.GameOver == false)
            {
                //Creating a Brush for the snake so the color of it can be set.
                //NOTE: Brush makes it possible to fill things with color
                Brush snakeColour; 


                for(int i = 0; i < Snake.Count; i++)
                {
                    //Color the diffrent parts of the snake
                    if(i == 0)
                    {
                        //Making the head of the snake black
                        snakeColour = Brushes.Purple;
                    }
                    else
                    {
                        //Making the body of the snake green
                        snakeColour = Brushes.LightBlue;
                    }
                    //"Draws" the snakes body and head
                    canvas.FillEllipse(snakeColour, new Rectangle(
                                            Snake[i].X * Settings.Width,
                                            Snake[i].Y * Settings.Height,
                                            Settings.Width, Settings.Height
                                            ));

                    //Draws the food
                    canvas.FillEllipse(Brushes.Red,
                                        new Rectangle(
                                            food.X * Settings.Width,
                                            food.Y * Settings.Height,
                                            Settings.Width, Settings.Height
                                            ));

                }
            }
            //If the game is over, then this else statement will run
            else
            {
                //Will show the GameOver text to the player, and the players score
                string gameOver = "Game Over \n" + "Final Score is " + Settings.Score + "\n Press enter to Restart \n";
                label3.Text = gameOver;
                label3.Visible = true;
            }
        }
        private void movePlayer()
        {
            //for the snakes head and parts
            for(int i = Snake.Count - 1; i >= 0; i--)
            {

                //If the snakes head moves, do this if-statment
                if (i == 0)
                {

                    //Switch-Case statement, moves the snakes body the way its head moves
                    switch (Settings.direction)
                    {

                        //Case 1 (Directions.Right) runs if the direction of the head goes right
                        //Then it moves along the X-axis +1
                        case Directions.Right:
                            Snake[i].X++;
                            break;

                        //Case 2 (Directions.Left) runs if the direction of the head goes left
                        //Then it moves along the X-axis -1
                        case Directions.Left:
                            Snake[i].X--;
                            break;

                        //Case 1 (Directions.Up) runs if the direction of the head goes up
                        //Then it moves along the Y-axis -1
                        case Directions.Up:
                            Snake[i].Y--;
                            break;

                        //Case 1 (Directions.Down) runs if the direction of the head goes down
                        //Then it moves along the Y-axis +1
                        case Directions.Down:
                            Snake[i].Y++;
                            break;

                    }
                    //Setting the "wall" that stops the snake from going out of the screen
                    int maxXpos = pbCanvas.Size.Width / Settings.Width;
                    int maxYpos = pbCanvas.Size.Height / Settings.Height;

                    //If the snakes "crashes into the wall", then it dies 
                    if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X > maxXpos || Snake[i].Y > maxYpos)
                    {
                        die();
                    }

                    //Checks if the snake crosses its own body
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        //If the snake crosses its own body, then it dies
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            die();
                        }
                    }
                    //If the snakes head and the food is in the same place...
                    //...then the snake eats the food
                    if(Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        eat();
                    }
      
                }
                //if the snake doesnt crash in its own body, the wall or the food...
                //...then it just continues moving
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        //Creates the food at random places
        private void generateFood()
        {
            //Creating the maxium positions
            int maxXpos = pbCanvas.Size.Width / Settings.Width;
            int maxYpos = pbCanvas.Size.Height / Settings.Height;

            //Implements the random method
            Random rnd = new Random();
            //Creating a "food-cricle" at a random place on the canvas
            food = new Circle { X = rnd.Next(0, maxXpos), Y = rnd.Next(0, maxYpos) };
            
        }
      
        //Making the snake longer when it eats the food
        private void eat()
        {
            //Everytime the snake "eats the food" the snake will grow one circle longer.
            Circle body = new Circle{ X = Snake[Snake.Count - 1].X, Y = Snake[Snake.Count - 1].Y };
            Snake.Add(body);

            //Changing the score to points and updates the lable2 (points score) to how long the body is
            Settings.Score += Settings.Points;
            label2.Text = Settings.Score.ToString();

            //runs the genetateFood function - to create a new food.
            generateFood();
        }

        //When the snake dies the GameOver sign shows
        private void die()
        {
            Settings.GameOver = true;
        }
    }
}
