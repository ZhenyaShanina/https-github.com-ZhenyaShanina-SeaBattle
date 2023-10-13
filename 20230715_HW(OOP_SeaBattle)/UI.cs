using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL;

namespace _20230715_HW_OOP_SeaBattle_
{
    public class UI
    {
        const int CLEAR_CONSOLE = 69;

        const int DISTANCE_TEXT_X = 50;
        const int DISTANCE_TEXT_Y = 3;

        const int DISTANCE_INFO_TEXT = 1;

        const int MIN_NUMBER_TO_GET = 1;
        const int MAX_NUMBER_TO_GET = 10;

        const int MIN_NUMBER_TO_GET_BOOL = 0;
        const int MAX_NUMBER_TO_GET_BOOL = 1;


        #region --------------Print Deck--------------        

        /// <summary>
        /// Печатает корабли и выстрелы на поле
        /// </summary>
        /// <param name="ships">список всех корабле</param>
        /// <param name="coord">список всех выстрелов</param>
        /// <param name="offsetX">смещение по Х</param>
        /// <param name="showDeck">отображать ли корабль до попадания</param>

        public static void PrintShips(IEnumerable<Ship> ships, List<Coordinate> coord,
            int offsetX = 0, bool showDeck = true)
        {
            if (coord != null)
            {
                foreach (Coordinate i in coord)
                {
                    Console.SetCursorPosition(i.X + 1 + offsetX, i.Y);
                    Console.Write(".");
                }
            }

            foreach (Ship s in ships)
            {
                FourDecksShip s4 = s as FourDecksShip;

                if (s4 != null)
                {
                    PrintDeck(s4.X4 + offsetX, s4.Y4, s4.CellX4Y4, showDeck);
                }

                ThreeDecksShip s3 = s as ThreeDecksShip;

                if (s3 != null)
                {
                    PrintDeck(s3.X3 + offsetX, s3.Y3, s3.CellX3Y3, showDeck);
                }

                TwoDecksShip s2 = s as TwoDecksShip;

                if (s2 != null)
                {
                    PrintDeck(s2.X2 + offsetX, s2.Y2, s2.CellX2Y2, showDeck);
                }

                OneDeckShip s1 = s as OneDeckShip;

                if (s1 != null)
                {
                    PrintDeck(s1.X1 + offsetX, s1.Y1, s1.CellX1Y1, showDeck);
                }
            } 
        }

        private static void PrintDeck(int x, int y, CellStatus status, bool showDeck)
        {
            Console.SetCursorPosition(x + 1, y);

            switch (status)
            {
                case CellStatus.Deck:

                    if (showDeck)
                    {
                        Console.Write('#');
                    }
                    break;
                case CellStatus.Shooted:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write('X');
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case CellStatus.Killed:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('X');
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Печатает доску
        /// </summary>
        /// <param name="offsetX">смещение по Х</param>

        public static void PrintDeck(int offsetX = 0)
        {
            Console.SetCursorPosition(offsetX + 2, 0);
            Console.Write("ABCDEFGHIJ");

            for (int y = 1; y < 10; y++)
            {
                Console.SetCursorPosition(offsetX, y);

                Console.Write(" {0}", y);
            }

            Console.SetCursorPosition(offsetX, 10);
            Console.Write("10");
        }

        #endregion

        /// <summary>
        /// Установка кораблей пользователем
        /// </summary>
        /// <param name="player">поле игрока</param>
        /// <param name="deckCount">тип устанавливаемого корабля</param>

        public static void PlaceShips(PlayerField player, ShipType deckCount)
        {
            Console.SetCursorPosition(DISTANCE_TEXT_X, DISTANCE_INFO_TEXT);
            Console.Write("Установка {0} палубного корабля: ", (int)deckCount);

            bool successfullPlace = false;
            bool isHorizontal = false;

            do
            {

                int x = GetCoordinate("Введите координату X начала {0}-палубного корабля: ", 
                    DISTANCE_TEXT_X, DISTANCE_TEXT_Y, MIN_NUMBER_TO_GET, MAX_NUMBER_TO_GET, (int)deckCount);

                int y = GetCoordinate("Введите координату Y начала {0}-палубного корабля: ",
                    DISTANCE_TEXT_X, DISTANCE_TEXT_Y, MIN_NUMBER_TO_GET, MAX_NUMBER_TO_GET, (int)deckCount);

                if ((int)deckCount != 1)
                {
                    isHorizontal = GetCoordinate("Корабль расположен горизонтально?(Введите 0 если да, 1 - нет): ",
                        DISTANCE_TEXT_X, DISTANCE_TEXT_Y, MIN_NUMBER_TO_GET_BOOL, MAX_NUMBER_TO_GET_BOOL) == 0;
                }
                ClearConsole(DISTANCE_TEXT_X, DISTANCE_TEXT_Y, CLEAR_CONSOLE);

                successfullPlace = player.PlaceShip(Field.ChooseShip(new Coordinate(x, y), isHorizontal, deckCount));

                if (!successfullPlace)
                {
                    PrintText("Корабль не может быть так размешен, попробуйте еще раз", DISTANCE_TEXT_Y);
                }
                else
                {
                    PrintText("Успешно установлен!", DISTANCE_TEXT_Y);
                }

            } while (!successfullPlace);

            ClearConsole(DISTANCE_TEXT_X, DISTANCE_INFO_TEXT, CLEAR_CONSOLE);
        }

        private static void PrintText(string message, int distanceY)
        {
            Console.SetCursorPosition(DISTANCE_TEXT_X, distanceY);
            Console.Write(message);
            Console.ReadKey();
            ClearConsole(DISTANCE_TEXT_X, distanceY, CLEAR_CONSOLE);
        }

        private static int GetCoordinate(string message, int x, int y, int min = 1, int max = 10, int deckCount = 0)
        {
            int coordinate;

            do
            {
                ClearConsole(x, y, CLEAR_CONSOLE);
                Console.SetCursorPosition(x, y);
                Console.Write(message, deckCount);

            } while (!int.TryParse(Console.ReadLine(), out coordinate) || coordinate < min || coordinate > max);

            return coordinate;
        }

        /// <summary>
        /// Получить координату для выстрела у игрока
        /// </summary>
        /// <returns>координату выстрела</returns>

        public static Coordinate GetCoordinate()
        {
            int x = GetCoordinate("Введите координату X для стрелка: ", DISTANCE_TEXT_X, DISTANCE_INFO_TEXT);
            int y = GetCoordinate("Введите координату Y для стрелка: ", DISTANCE_TEXT_X, DISTANCE_INFO_TEXT + 1);

            ClearConsole(DISTANCE_TEXT_X, DISTANCE_INFO_TEXT, CLEAR_CONSOLE);
            ClearConsole(DISTANCE_TEXT_X, DISTANCE_INFO_TEXT + 1, CLEAR_CONSOLE);

            return new Coordinate(x, y);
        }

        private static void ClearConsole(int x, int y, int width)
        {
            for (int i = 0; i < width; i++)
            {
                Console.SetCursorPosition(x + i, y);
                Console.Write(' ');
            }
        }

        /// <summary>
        /// Получить победителя
        /// </summary>
        /// <param name="winner">победитель</param>

        public static void GetWinner(GameStatus winner)
        {
            if (winner == GameStatus.WinBot)
            {
                PrintText("Победил: Бот", DISTANCE_INFO_TEXT);
            }
            else if (winner == GameStatus.WinPlayer)
            {
                PrintText("Победил: Игрок", DISTANCE_INFO_TEXT);
            }
        }
    }
}
