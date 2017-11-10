using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XnBreak.XnSprite;

namespace XnBreak.XnSprite.Event
{
    public class BrickDestroyedEventArgs : EventArgs
    {
        public Brick brick;
        public int index;

        public BrickDestroyedEventArgs () { }

        public BrickDestroyedEventArgs (Brick brick)
        {
            this.brick = brick;
        }

        public BrickDestroyedEventArgs (Brick brick, int indexCollection)
            : this(brick)
        {
            this.index = indexCollection;
        }
    }
}
