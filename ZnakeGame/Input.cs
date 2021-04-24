using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//"Implementerade" classes
using System.Collections;
using System.Windows.Forms;

namespace ZnakeGame
{
    class Input
    {
        //Makes it possible to use the keys 
        //NOTE: Hashtable is a collection of key and vaule pairs
        private static Hashtable keyTable = new Hashtable();

        //NOTE: bool is a true or false statement
        public static bool KeyPress(Keys key)
        {
            //Will return a key or not back to the class
            if (keyTable[key] == null)
            {
                //If none key is pressed it will return false 
                return false;
            }
            //if the hashtable is not empty it will return true.
            return (bool)keyTable[key];
        }

        //will change the state of the keys and the player
        public static void changeState(Keys key, bool state)
        {
            keyTable[key] = state;
        }
    }
}
