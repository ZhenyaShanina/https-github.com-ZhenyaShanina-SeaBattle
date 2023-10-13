using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20230715_HW_OOP_SeaBattle_
{
    public abstract class Field : IEnumerable<Ship>, IEnumerable<Coordinate>
    {
        protected static Random rnd = new Random();

        protected const int MIN_GENERATED_COORDINATE = 1;
        protected const int MAX_GENERATED_COORDINATE = 11;

        const int MIN_GENERATED_COORDINATE_BOOL = 0;
        const int MAX_GENERATED_COORDINATE_BOOL = 2;

        const int MIN_SIZE_FIELD = 1;
        const int MAX_SIZE_FIELD = 10;

        protected List<Coordinate> _shoots = new List<Coordinate>();
        protected List<Ship> _ships = new List<Ship>();

        public List<Coordinate> MissCoordinateOnField
        {
            get
            {
                return _shoots;
            }
        }


        #region --------------Try Place Ship--------------

        /// <summary>
        /// Попытка установить корабль на поле
        /// </summary>
        /// <param name="s">корабль, который устанавливают</param>
        /// <returns>true если успешно установлен, false если нет</returns>

        public bool PlaceShip(Ship s)
        {
            if (s.X1 < MIN_SIZE_FIELD || s.X1 > MAX_SIZE_FIELD || s.Y1 < 0 || s.Y1 > MAX_SIZE_FIELD)
            {
                return false;
            }

            if (s.LastX < MIN_SIZE_FIELD || s.LastX > MAX_SIZE_FIELD || s.LastY < 0 || s.LastY > MAX_SIZE_FIELD)
            {
                return false;
            }

            bool freePlace = true;

            freePlace = IsEmptyDeck(s.X1, s.Y1, _ships);

            if (!freePlace)
            {
                return false;
            }

            if (s is TwoDecksShip)
            {
                freePlace = IsEmptyDeck(((TwoDecksShip)s).X2, ((TwoDecksShip)s).Y2, _ships);
            }

            if (!freePlace)
            {
                return false;
            }

            if (s is ThreeDecksShip)
            {
                freePlace = IsEmptyDeck(((ThreeDecksShip)s).X3, ((ThreeDecksShip)s).Y3, _ships);
            }

            if (!freePlace)
            {
                return false;
            }

            if (s is FourDecksShip)
            {
                freePlace = IsEmptyDeck(((FourDecksShip)s).X4, ((FourDecksShip)s).Y4, _ships);
            }

            if (!freePlace)
            {
                return false;
            }
            else
            {
                _ships.Add(s);
            }

            return freePlace;
        }      

        private bool IsEmptyDeck(int x, int y, List<Ship> ships)
        {
            bool freePlace = true;

            foreach (Ship i in ships)
            {
                FourDecksShip s4 = i as FourDecksShip;

                if (s4 != null)
                {
                    if (s4.X4 == x && s4.Y4 == y || CheckAround(s4.X4, s4.Y4, x, y))
                    {
                        freePlace = false;
                    }
                }

                ThreeDecksShip s3 = i as ThreeDecksShip;

                if (s3 != null)
                {
                    if (s3.X3 == x && s3.Y3 == y || CheckAround(s3.X3, s3.Y3, x, y))
                    {
                        freePlace = false;
                    }
                }

                TwoDecksShip s2 = i as TwoDecksShip;

                if (s2 != null)
                {
                    if (s2.X2 == x && s2.Y2 == y || CheckAround(s2.X2, s2.Y2, x, y))
                    {
                        freePlace = false;
                    }
                }

                OneDeckShip s1 = i as OneDeckShip;

                if (s1 != null)
                {
                    if (s1.X1 == x && s1.Y1 == y || CheckAround(s1.X1, s1.Y1, x, y))
                    {
                        freePlace = false;
                    }
                }
            }

            return freePlace;
        }

        private bool CheckAround(int ShipX, int ShipY, int x, int y)
        {
            if (ShipX + 1 == x && ShipY == y)
            {
                return true;
            }

            if (ShipX - 1 == x && ShipY == y)
            {
                return true;
            }

            if (ShipX == x && ShipY + 1 == y)
            {
                return true;
            }

            if (ShipX == x && ShipY - 1 == y)
            {
                return true;
            }

            if (ShipX + 1 == x && ShipY + 1 == y)
            {
                return true;
            }

            if (ShipX - 1 == x && ShipY + 1 == y)
            {
                return true;
            }

            if (ShipX + 1 == x && ShipY - 1 == y)
            {
                return true;
            }

            if (ShipX - 1 == x && ShipY - 1 == y)
            {
                return true;
            }

            return false;
        }

        #endregion


        #region --------------Automatic Place Ship--------------

        /// <summary>
        /// Самостоятельно раставляет корабли на поле
        /// </summary>
        /// <param name="deckCount">Тип корабля, который устонавливаем</param>

        public void PlaceShipInDeck(ShipType deckCount)
        {
            bool result = false;

            while (!result)
            {
                int x = rnd.Next(MIN_GENERATED_COORDINATE, MAX_GENERATED_COORDINATE);
                int y = rnd.Next(MIN_GENERATED_COORDINATE, MAX_GENERATED_COORDINATE);

                bool isHorizontal = rnd.Next(MIN_GENERATED_COORDINATE_BOOL, MAX_GENERATED_COORDINATE_BOOL) == 0;

                result = PlaceShip(ChooseShip(new Coordinate(x, y), isHorizontal, deckCount));
            }
        }

        /// <summary>
        /// Возвращает корабль
        /// </summary>
        /// <param name="coord">Координата расположения первой палубы</param>
        /// <param name="isHorizontal">Горизонтально ли размещен</param>
        /// <param name="deckCount">Тип корабля</param>
        /// <returns>Корабль</returns>

        public static Ship ChooseShip(Coordinate coord, bool isHorizontal, ShipType deckCount)
        {
            Ship newShip = null;

            switch (deckCount)
            {
                case ShipType.OneDeck:
                    newShip = new OneDeckShip(coord);
                    break;
                case ShipType.TwoDecks:
                    newShip = new TwoDecksShip(coord, isHorizontal);
                    break;
                case ShipType.ThreeDecks:
                    newShip = new ThreeDecksShip(coord, isHorizontal);
                    break;
                case ShipType.FourDecks:
                    newShip = new FourDecksShip(coord, isHorizontal);
                    break;
                default:
                    newShip = null;
                    break;
            }

            return newShip;
        }

        #endregion

        /// <summary>
        /// Проверка все ли корабли убиты
        /// </summary>
        /// <returns>True если все убиты, false если нет</returns>

        public bool IsShipsDestroyed()
        {
            bool shipsKilled = true;

            foreach (Ship s in _ships)
            {
                if (s.Status != CellStatus.Killed)
                {
                    shipsKilled = false;
                    break;
                }
            }

            return shipsKilled;
        }

        /// <summary>
        /// Проверка на повторение координаты
        /// </summary>
        /// <param name="currentCoord">координата которую проверяем</param>
        /// <returns>True если такая уже была, false если нет</returns>

        protected bool IsRepeatedShoot(Coordinate currentCoord)
        {
            bool repeatedCoordinate = false;

            foreach (Coordinate lastCoord in _shoots)
            {
                if (lastCoord == currentCoord)
                {
                    repeatedCoordinate = true;
                    break;
                }
            }

            return repeatedCoordinate;
        }


        #region -=-=- IEnumerator -=-=-

        public IEnumerator<Ship> GetEnumerator()
        {
            return _ships.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _ships.GetEnumerator();
        }

        IEnumerator<Coordinate> IEnumerable<Coordinate>.GetEnumerator()
        {
            return _shoots.GetEnumerator();
        }

        #endregion
    }
}