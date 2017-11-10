using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XnBreak.XnSprite
{
    public class BalleMissedEventArgs : EventArgs
    {
        public Balle balle;

        public BalleMissedEventArgs () { }

        public BalleMissedEventArgs (Balle balle)
        {
            this.balle = balle;
        }
    }
}
