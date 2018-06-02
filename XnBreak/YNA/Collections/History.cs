using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YNA.Collections.Generic
{
    public class History<T> : CircularArray<T>
    {
        private int count;
        private int pointer;

        public T Current 
        {
            get { return this[pointer - 1]; }
        }

        public History (int size)
            : base (size)
        {
            pointer = 0;
        }
    }
}
