using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20230715_HW_OOP_SeaBattle_
{
    public class FourDecksShip: ThreeDecksShip
    {
        public CellStatus CellX4Y4 { get; private set; }

        public FourDecksShip(Coordinate coord, bool isHorizontal)
            : base(coord, isHorizontal)
        {
            CellX4Y4 = CellStatus.Deck;

            _isHorizontal = isHorizontal;
        }

        public Coordinate CoordinateX4Y4
        {
            get
            {
                return new Coordinate(X4, Y4);
            }
        }

        public int X4
        {
            get
            {
                if (_isHorizontal)
                {
                    return _coord.X + 3;
                }
                else
                {
                    return _coord.X;
                }
            }
        }

        public int Y4
        {
            get
            {
                if (_isHorizontal)
                {
                    return _coord.Y;
                }
                else
                {
                    return _coord.Y + 3;
                }
            }
        }

        public override int LastX
        {
            get
            {
                return X4;
            }
        }

        public override int LastY
        {
            get
            {
                return Y4;
            }
        }

        public override CellStatus Status
        {
            get
            {
                return CellX4Y4;
            }
        }

        /// <summary>
        /// Выстрел в корабль
        /// </summary>
        /// <param name="coord">координата стрелка</param>
        /// <returns>Возвращает итоговое состояние палубы</returns>

        public override CellStatus Shoot(Coordinate coord)
        {
            CellStatus result = base.Shoot(coord);

            if (CoordinateX4Y4 == coord && CellX3Y3 != CellStatus.Killed)
            {
                result = CellX4Y4 = CellStatus.Shooted;
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
            if (CellX4Y4 == CellStatus.Shooted)
            {
                bool result = base.Kill();

                if (result)
                {
                    CellX4Y4 = CellStatus.Killed;
                    return true;
                }
            }

            return false;
        }
    }
}