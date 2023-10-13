using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BotField : Field, IEnumerable<Ship>
    {
        protected int _lastShotX;
        protected int _lastShotY;

        protected bool _hit = false;
        protected Ship _shootedShip;

        Direction lastDirection = Direction.None;

        /// <summary>
        /// Выстрел по полю игрока
        /// </summary>
        /// <param name="player">поле игрока</param>
        /// <returns>True если попадание, false если нет</returns>

        public bool AttackBot(PlayerField player)
        {
            int x = 0;
            int y = 0;

            do
            {
                x = rnd.Next(MIN_GENERATED_COORDINATE, MAX_GENERATED_COORDINATE);
                y = rnd.Next(MIN_GENERATED_COORDINATE, MAX_GENERATED_COORDINATE);
            } while (IsRepeatedShoot(new Coordinate(x, y)));



            Ship result = player.HittingTheShip(new Coordinate(x, y));

            _hit = (result != null);

            _shoots.Add(new Coordinate(x, y));

            return _hit;
        }

        private void ChooseDirection()
        {
            int x = 0;
            int y = 0;

            List<Direction> directions = new List<Direction> { Direction.Left,
                     Direction.Right, Direction.Up, Direction.Down };


            if (_hit)
            {
                if (_shootedShip.Kill())
                {
                    _hit = false;

                    directions = new List<Direction> { Direction.Left, Direction.Right,
                             Direction.Up, Direction.Down };
                }
                else
                {

                    if (_lastShotX > MAX_GENERATED_COORDINATE || lastDirection == Direction.Left)
                    {
                        directions.Remove(Direction.Left);
                    }

                    if (_lastShotY > MAX_GENERATED_COORDINATE || lastDirection == Direction.Down)
                    {
                        directions.Remove(Direction.Down);
                    }

                    if (_lastShotX <= MIN_GENERATED_COORDINATE || lastDirection == Direction.Right)
                    {
                        directions.Remove(Direction.Right);
                    }

                    if (_lastShotY <= MIN_GENERATED_COORDINATE || lastDirection == Direction.Up)
                    {
                        directions.Remove(Direction.Up);
                    }

                    Direction direction = (Direction)rnd.Next(1, directions.Count + 1);

                    //Direction direction = (Direction)rnd.Next(1, 5);

                    switch (direction)
                    {
                        case Direction.Left:
                            x = _lastShotX + 1;
                            y = _lastShotY;
                            lastDirection = Direction.Left;

                            break;
                        case Direction.Right:
                            x = _lastShotX - 1;
                            y = _lastShotY;
                            lastDirection = Direction.Right;

                            break;
                        case Direction.Up:
                            x = _lastShotX;
                            y = _lastShotY + 1;
                            lastDirection = Direction.Up;

                            break;
                        case Direction.Down:
                            x = _lastShotX;
                            y = _lastShotY - 1;
                            lastDirection = Direction.Down;

                            break;

                        default:
                            break;
                    }
                }

            }
        }

        /// <summary>
        /// Проверка попадания выстрела игрока в корабли бота
        /// </summary>
        /// <param name="coord">координата выстрела</param>
        /// <returns>True при попадании, false при промахе</returns>

        public bool IsHittingTheShip(Coordinate coord)
        {
            foreach (Ship s in _ships)
            {
                if (s.Shoot(coord) == CellStatus.Shooted)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
