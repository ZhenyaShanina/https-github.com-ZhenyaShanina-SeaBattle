using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PlayerField : Field, IEnumerable<Ship>
    {

        public List<Ship> ShipsPlayer
        {
            get
            {
                return _ships;
            }
        }

        /// <summary>
        /// Выстрел по полю компьютера
        /// </summary>
        /// <param name="bot">поле компьютера</param>
        /// <param name="coord">координата выстрела</param>
        /// <returns>True если попадание, false если нет</returns>

        public bool AttackPlayer(BotField bot, Coordinate coord)
        {
            if (IsRepeatedShoot(coord))
            {
                return false;
            }

            _shoots.Add(coord);

            return bot.IsHittingTheShip(coord);
        }

        /// <summary>
        /// Проверка попадания выстрела бота в корабли игрока
        /// </summary>
        /// <param name="coord">координата выстрела</param>
        /// <returns>Корабль при попадании, null при промахе</returns>

        public Ship HittingTheShip(Coordinate coord)
        {
            foreach (Ship s in _ships)
            {
                if (s.Shoot(coord) == CellStatus.Shooted)
                {
                    return s;
                }
            }

            return null;
        }
    }
}
