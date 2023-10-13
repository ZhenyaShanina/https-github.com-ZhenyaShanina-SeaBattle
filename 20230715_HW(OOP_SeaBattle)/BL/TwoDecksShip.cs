using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TwoDecksShip : OneDeckShip
    {
        public CellStatus CellX2Y2 { get; private set; }

        public TwoDecksShip(Coordinate coord, bool isHorizontal)
            : base(coord)
        {
            CellX2Y2 = CellStatus.Deck;

            _isHorizontal = isHorizontal;
        }

        public Coordinate CoordinateX2Y2
        {
            get
            {
                return new Coordinate(X2, Y2);
            }
        }

        public int X2
        {
            get
            {
                if (_isHorizontal)
                {
                    return _coord.X + 1;
                }
                else
                {
                    return _coord.X;
                }
            }
        }

        public int Y2
        {
            get
            {
                if (_isHorizontal)
                {
                    return _coord.Y;
                }
                else
                {
                    return _coord.Y + 1;
                }
            }
        }

        public override int LastX
        {
            get
            {
                return X2;
            }
        }

        public override int LastY
        {
            get
            {
                return Y2;
            }
        }

        public override CellStatus Status
        {
            get
            {
                return CellX2Y2;
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

            if (CoordinateX2Y2 == coord && CellX2Y2 != CellStatus.Killed)
            {
                result = CellX2Y2 = CellStatus.Shooted;
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
            if (CellX2Y2 == CellStatus.Shooted)
            {
                bool result = base.Kill();

                if (result)
                {
                    CellX2Y2 = CellStatus.Killed;
                    return true;
                }
            }

            return false;
        }
    }
}
