using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20230715_HW_OOP_SeaBattle_
{
    public abstract class Ship
    {
        protected bool _isHorizontal;

        protected Coordinate _coord;

        public bool IsHorizontal
        {
            get
            {
                return _isHorizontal;
            }
        }

        public int X1
        {
            get
            {
                return _coord.X;
            }
        }

        public int Y1
        {
            get
            {
                return _coord.Y;
            }
        }

        public abstract int LastX
        {
            get;
        }

        public abstract int LastY
        {
            get;
        }

        public abstract CellStatus Status
        {
            get;
        }

        /// <summary>
        /// Выстрел в корабль
        /// </summary>
        /// <param name="coord">координата стрелка</param>
        /// <returns>Возвращает итоговое состояние палубы</returns>

        public abstract CellStatus Shoot(Coordinate coord);

        /// <summary>
        /// Проверка убит ли корабль
        /// </summary>
        /// <returns>True если убит, false если нет</returns>

        public abstract bool Kill();
    }
}