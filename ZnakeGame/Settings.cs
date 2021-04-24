using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZnakeGame
{
    //Makes it easier to classify directions with enum
    //NOTE: enum is a kind of "uppräkning" of values
    public enum Directions
    {
        Left,
        Right,
        Up,
        Down
    }
    class Settings
    {
        //The following 7 properties makes us set and get their value
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Points { get; set; }
        public static bool GameOver { get; set; }
        public static Directions direction { get; set; }

        public Settings()
        {
            //The default settings
            Width = 16;
            Height = 16;
            Speed = 10;
            Score = 0;
            Points = 1;
            GameOver = false;
            direction = Directions.Down;
        }
    }
}
