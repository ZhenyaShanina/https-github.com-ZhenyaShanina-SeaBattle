using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace _20230715_HW_OOP_SeaBattle_
{
    internal class Program
    {
        static Random rnd = new Random();

        const int DISTANCE_BOT_FIELD = 30;

        static void Main(string[] args)
        {
            PlayerField player = new PlayerField();
            BotField bot = new BotField();

            UI.PrintDeck();

#if DEBUG
            for (ShipType t = ShipType.FourDecks; t >= ShipType.OneDeck; t--)
            {
                int shipCount = ShipType.FourDecks - t + 1;

                for (int i = 0; i < shipCount; i++)
                {
                    player.PlaceShipInDeck(t);
                }
            }

            UI.PrintShips(player, bot.MissCoordinateOnField);
#else
            for (ShipType t = ShipType.FourDecks; t >= ShipType.OneDeck; t--)
            {
                int shipCount = ShipType.FourDecks - t + 1;

                for (int i = 0; i < shipCount; i++)
                {
                    UI.PlaceShips(player, t);
                    UI.PrintShips(player, bot.MissCoordinateOnField);
                }
            }

            UI.PrintShips(player, bot.MissCoordinateOnField);
#endif


            UI.PrintDeck(DISTANCE_BOT_FIELD);

            for (ShipType t = ShipType.FourDecks; t >= ShipType.OneDeck; t--)
            {
                int shipCount = ShipType.FourDecks - t + 1;

                for (int i = 0; i < shipCount; i++)
                {
                    bot.PlaceShipInDeck(t);
                }
            }

#if DEBUG
            UI.PrintShips(bot, player.MissCoordinateOnField, DISTANCE_BOT_FIELD);
#else
            UI.PrintShips(bot, player.MissCoordinateOnField, DISTANCE_BOT_FIELD, false);
#endif

            GameStatus gameIsOver = GameStatus.GameContinue;
            bool playerMove = true;

            do
            {
                if (playerMove)
                {
                    playerMove = player.AttackPlayer(bot, UI.GetCoordinate());

#if DEBUG
                    UI.PrintShips(bot, player.MissCoordinateOnField, DISTANCE_BOT_FIELD);
#else
                    UI.PrintShips(bot, player.MissCoordinateOnField, DISTANCE_BOT_FIELD, false);      
#endif                    
                }
                else
                {
                    playerMove = !bot.AttackBot(player);

                    UI.PrintShips(player, bot.MissCoordinateOnField);
                }

                gameIsOver = IsGameOver(player, bot);
            }
            while (gameIsOver == GameStatus.GameContinue);

            UI.GetWinner(gameIsOver);

            Console.ReadKey();
        }

        /// <summary>
        /// Проверка на продолжение игры
        /// </summary>
        /// <param name="player">поле игрока</param>
        /// <param name="bot">поле бота</param>
        /// <returns>статус игры</returns>

        public static GameStatus IsGameOver(PlayerField player, BotField bot)
        {
            GameStatus status = GameStatus.GameContinue;

            bool playerShipsKilled = player.IsShipsDestroyed();
            bool botShipsKilled = bot.IsShipsDestroyed();            

            if (playerShipsKilled)
            {
                status = GameStatus.WinBot;
            }
            else if (botShipsKilled)
            {
                status = GameStatus.WinPlayer;
            }

            return status;
        }
    }
}
