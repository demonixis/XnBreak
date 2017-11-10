using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XnBreak.XnLevel.Event
{
    public enum LevelFinish { win, loose }

    public class LevelFinishEventArgs : EventArgs
    {
        public LevelFinish endType;

        public LevelFinishEventArgs () { }

        public LevelFinishEventArgs (LevelFinish endType)
        {
            this.endType = endType;
        }
    }
}
