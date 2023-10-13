using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ThreeDecksShip : TwoDecksShip
    {
        public CellStatus CellX3Y3 { get; private set; }

        public ThreeDecksShip(Coordinate coord, bool isHorizontal)
            : base(coord, isHorizontal)
        {
            CellX3Y3 = CellStatus.Deck;

            _isHorizontal = isHorizontal;
        }

        public Coordinate CoordinateX3Y3
        {
            get
            {
                return new Coordinate(X3, Y3);
            }
        }

        public int X3
        {
            get
            {
                if (_isHorizontal)
                {
                    return _coord.X + 2;
                }
                else
                {
                    return _coord.X;
                }
            }
        }

        public int Y3
        {
            get
            {
                if (_isHorizontal)
                {
                    return _coord.Y;
                }
                else
                {
                    return _coord.Y + 2;
                }
            }
        }

        public override int LastX
        {
            get
            {
                return X3;
            }
        }

        public override int LastY
        {
            get
            {
                return Y3;
            }
        }

        public override CellStatus Status
        {
            get
            {
                return CellX3Y3;
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

            if (CoordinateX3Y3 == coord && CellX3Y3 != CellStatus.Shooted && CellX3Y3 != CellStatus.Killed)
            {
                result = CellX3Y3 = CellStatus.Shooted;
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
            if (CellX3Y3 == CellStatus.Shooted)
            {
                bool result = base.Kill();

                if (result)
                {
                    CellX3Y3 = CellStatus.Killed;
                    return true;
                }
            }

            return false;
        }
    }
}
