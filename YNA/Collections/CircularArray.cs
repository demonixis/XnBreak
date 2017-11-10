using System;
using System.Collections.Generic;
using System.Text;

namespace YNA.Collections.Generic
{
    public class CircularArray<T>
    {
        T[] items;
        int capacity;

        public CircularArray (int size)
        {
            SetSize (size);
        }

        /// <summary>
        /// Indexeur
        /// </summary>
        /// <param name="index">index de l'objet</param>
        /// <returns>objet d'index "index"</returns>
        public T this[int index]
        {
            get
            {
                int _index = ((index % Capacity) + Capacity) % Capacity;
                return items[_index];
            }
            set
            {
                int _index = ((index % Capacity) + Capacity) % Capacity;
                items[_index] = value;
            }
        }

        public int Capacity
        {
            get { return items.Length; }
        }

        public void Clear ()
        {
            SetSize (Capacity);
        }

        public void SetSize(int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException ("La taille de la collection ne peux être négative ou nulle");
            items = new T[size];
        }
    }
}
