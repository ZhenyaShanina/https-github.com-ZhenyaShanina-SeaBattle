using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20230715_HW_OOP_SeaBattle_
{
    public class OneDeckShip : Ship
    {
        public CellStatus CellX1Y1 { get; private set; }

        public OneDeckShip(Coordinate coord)
        {
            _coord = coord;

            CellX1Y1 = CellStatus.Deck;
        }

        public Coordinate CoordinateX1Y1
        {
            get
            {
                return new Coordinate(_coord.X, _coord.Y);
            }
        }

        public override int LastX
        {
            get
            {
                return _coord.X;
            }
        }

        public override int LastY
        {
            get
            {
                return _coord.Y;
            }
        }

        public override CellStatus Status
        {
            get
            {
                return CellX1Y1;
            }
        }

        /// <summary>
        /// Выстрел в корабль
        /// </summary>
        /// <param name="coord">координата стрелка</param>
        /// <returns>Возвращает итоговое состояние палубы</returns>

        public override CellStatus Shoot(Coordinate coord)
        {
            CellStatus result = CellStatus.Deck;

            if (CoordinateX1Y1 == coord && CellX1Y1 != CellStatus.Killed)
            {
                result = CellX1Y1 = CellStatus.Shooted;
            }

            Kill();

            return result;
        }

        /// <summary>
        /// Проверка убит ли корабль
        /// </summary>
        /// <returns>True если убит, false если нет</returns>

        public override bool Kill()
        {
            if (CellX1Y1 == CellStatus.Shooted)
            {
                CellX1Y1 = CellStatus.Killed; 
                return true;
            }

            return false;
        }
    }
}
