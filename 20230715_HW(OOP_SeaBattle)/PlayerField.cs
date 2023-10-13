using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _20230715_HW_OOP_SeaBattle_
{
    public class PlayerField: Field, IEnumerable<Ship>
    {

        public List<Ship> ShipsPlayer
        {
            get
            {
                return _ships;
            }
        }

        /// <summary>
        /// Выстрев по полю компьютера
        /// </summary>
        /// <param name="bot">поле компьютера</param>
        /// <param name="coord"></param>
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