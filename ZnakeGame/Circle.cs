using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZnakeGame
{
    //Getting the X and Y location of the snake-object
    class Circle
    {

        public int X { get; set; }
        public int Y { get; set; }

        //resetting X and Y to 0
        public Circle()
        {
            X = 0;
            Y = 0;
        }
    }
}
