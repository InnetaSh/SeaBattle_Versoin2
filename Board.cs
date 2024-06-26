using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle_Versoin2
{
    internal class Board
    {
        private int Size;
        private char[,] board;

        private bool SingleDeckerFlag = false;
        private bool DoubleDeckerFlag = false;
        private bool ThreeDeckerFlag = false;
        private bool FourDeckerFlag = false;
        bool isHorizontal;
        bool DeckerFlag = false;
        public Board(int size)
        {
            Size = size;
            board = new char[size, size];
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    board[i, j] = '-';
                }
            }
        }

        private void BoardPrint()
        {
            Console.WriteLine("   12345678910");
            for (int i = 0; i < Size - 1; i++)
            {
                Console.Write($"{i + 1}  ");
                for (int j = 0; j < Size; j++)
                {
                    if (board[i, j] == 'X')
                        Console.Write('x');
                    else if (board[i, j] == 'O')
                        Console.Write('O');
                    else if (board[i, j] == 'V')
                        Console.Write('V');
                    else if (board[i, j] == '-')
                        Console.Write('-');
                    else
                        Console.Write(' ');
                }
                Console.WriteLine();
            }
            for (int i = 9; i < 10; i++)
            {
                Console.Write($"{i + 1} ");
                for (int j = 0; j < Size; j++)
                {
                    if (board[i, j] == 'X')
                        Console.Write('x');
                    else if (board[i, j] == 'O')
                        Console.Write('O');
                    else if (board[i, j] == 'V')
                        Console.Write('V');
                    else if (board[i, j] == '-')
                        Console.Write('-');
                    else
                        Console.Write(' ');
                }
                Console.WriteLine();
            }
        }


        private void IsOccupiedCellsVer(int x, int y, int countDecker)
        {
            for (var i = x - 1; i <= x + countDecker; i++)
                for (var j = y - 1; j <= y + 1; j++)
                {
                    if (i < 0 || j < 0 || i >= Size || j >= Size) continue;
                    if (board[i, j] != 'X')
                    {
                        board[i, j] = '1';
                    }
                }
        }
        private void IsOccupiedCellsHor(int x, int y, int countDecker)
        {
            for (var i = x - 1; i <= x + 1; i++)
                for (var j = y - 1; j <= y + countDecker; j++)
                {
                    if (i < 0 || j < 0 || i >= Size || j >= Size) continue;
                    if (board[i, j] != 'X')
                        board[i, j] = '1';
                }
        }

        private bool IsValidPlaceVer(int x, int y, int countDecker, out string msg)
        {
            msg = String.Empty;
            if (x < 0 || x + countDecker > Size || y < 0 || y > Size)
            {
                msg = "Данная ячейка вышла за границы, введите еще раз:";
                return false;
            }
            bool flag = false;
            for (var i = x; i <= x + countDecker - 1; i++)
            {
                if (i < 0 || y < 0 || i >= Size || y >= Size) continue;
                if ((board[i, y] == 'X') || board[i, y] == '1')
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                msg = "Данная ячейка занята кораблем (или расположена возле корабля), введите еще раз:";
                return false;
            }

            return true;
        }

        private bool IsValidPlaceHor(int x, int y, int countDecker, out string msg)
        {
            msg = String.Empty;
            if (x < 0 || x > Size || y < 0 || y + countDecker > Size)
            {
                msg = "Данная ячейка вышла за границы, введите еще раз:";
                return false;
            }
            bool flag = false;
            for (var i = y; i <= y + countDecker - 1; i++)
            {
                if (i < 0 || x < 0 || i >= Size || x >= Size) continue;
                if ((board[x, i] == 'X') || board[x, i] == '1')
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                msg = "Данная ячейка занята кораблем (или расположена возле корабля), введите еще раз:";
                return false;
            }

            return true;
        }

        private void SingleDeckShips(int x, int y, int shipSize, int kol)
        {
           SingleDeckerFlag = true;

                DeckersShips(x, y, shipSize,kol);
            
        }

        private void DoubleDeckerShips(int x, int y, int shipSize, int kol)
        {
          
            DoubleDeckerFlag = true;

                DeckersShips(x, y, shipSize,kol);
            

        }

        private void ThreeDeckerShips(int x, int y, int shipSize, int kol)
        {
            ThreeDeckerFlag = true;

          
                   DeckersShips(x, y, shipSize,kol);
            

        }

        private void FourDeckerShips(int x, int y, int shipSize, int kol)
        {

            FourDeckerFlag = true;
            DeckersShips(x, y, shipSize, kol);
        }



        private void DeckersShips(int x, int y, int shipSize,int kol)
        {
            var msg = String.Empty;
            for (int i = 0; i < kol; i++)
            {
                try
                {
                    if (!isHorizontal)
                    {
                        while (true)
                        {
                            if (IsValidPlaceVer(x, y, shipSize, out msg))
                            {
                                for (int k = x; k < x + shipSize; k++)
                                {
                                    board[k, y] = 'X';
                                }
                                IsOccupiedCellsVer(x, y, shipSize);
                                break;
                            }
                            else
                            {
                                Random rnd = new Random();
                                x = rnd.Next(0, Size);
                                y = rnd.Next(0, Size);
                                if (!checkCells.Contains($"{x};{y}"))
                                    checkCells.Add($"{x};{y}");
                            }
                            if (checkCells.Count > 100) return;
                        } 
                     
                    }
                    if (isHorizontal)
                    {
                        while (true)
                        {
                            if (IsValidPlaceHor(x, y, shipSize, out msg))
                            {
                                for (int k = y; k < y + shipSize; k++)
                                {
                                    board[x, k] = 'X';
                                }
                                IsOccupiedCellsHor(x, y, shipSize);
                                break;
                            }
                            else
                            {
                                Random rnd = new Random();
                                x = rnd.Next(0, Size);
                                y = rnd.Next(0, Size);
                                if (!checkCells.Contains($"{x};{y}"))
                                    checkCells.Add($"{x};{y}");
                            }
                            if (checkCells.Count > 100) return;
                        }
                    }
               
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;
                }
            }
        }

        private List<string>checkCells = new List<string>();
        

        private void GetCoord(string str, out int x, out int y)
        {
            var parts = str.Split(";");
            int.TryParse(parts[0], out x);
            int.TryParse(parts[1], out y);
        }
        private void clear()
        {
            SingleDeckerFlag = false;
            DoubleDeckerFlag = false;
            ThreeDeckerFlag = false;
            FourDeckerFlag = false;
            for (var i = 0; i < Size; i++)
                for (var y = 0; y < Size; y++)
                    board[i, y] = '-';
        }
        public void GenerateShips()
        {
            Random rnd = new Random();
            checkCells.Clear();

            while (!SingleDeckerFlag || !DoubleDeckerFlag || !ThreeDeckerFlag || !FourDeckerFlag)
            {
                int ShipSize = rnd.Next(1, 5);
                int startX = rnd.Next(0, Size);
                int startY = rnd.Next(0, Size);
                if (!checkCells.Contains($"{startX};{startY}"))
                    checkCells.Add($"{startX};{startY}");

                isHorizontal = (rnd.Next(0, 2) == 0);

                if (ShipSize == 1 && !SingleDeckerFlag)
                {
                    SingleDeckShips(startX, startY, ShipSize, 4);
                    if (checkCells.Count > 100)
                    {
                        clear();
                    }
                }
                if (ShipSize == 2 && !DoubleDeckerFlag)
                {
                    DoubleDeckerShips(startX, startY, ShipSize, 3);
                    if (checkCells.Count > 100)
                    {
                        clear();
                    }
                }
                if (ShipSize == 3 && !ThreeDeckerFlag)
                {
                    ThreeDeckerShips(startX, startY, ShipSize, 2);
                    if (checkCells.Count > 100)
                    {
                        clear();
                    }
                }
                if (ShipSize == 4 && !FourDeckerFlag)
                {
                    FourDeckerShips(startX, startY, ShipSize,1);
                    if (checkCells.Count > 100)
                    {
                        clear();
                    }
                }
            }
            BoardPrint();
        }

        public void AIShot()
        {
            Random rnd = new Random();

            int ShotStartX = rnd.Next(0, Size);
            int ShotStartY = rnd.Next(0, Size);
            int CountDoker = 0;
            if (board[ShotStartX, ShotStartY] == 'X')
            {
                CountDoker++;
                board[ShotStartX, ShotStartY] = 'O';
                BoardPrint();
                Thread.Sleep(1000);


                for (int k = ShotStartX + 1; k < ShotStartX + 4; k++)
                {
                    if (k >= Size)
                        break;
                    if (board[k, ShotStartY] == '1')
                        break;
                    else if (board[k, ShotStartY] == 'X')
                    {
                        board[k, ShotStartY] = 'O';
                        CountDoker++;
                        Console.Clear();
                        BoardPrint();
                        Thread.Sleep(1000);
                    }
                }
                if (CountDoker != 4)
                {
                    for (int k = ShotStartX - 1; k > ShotStartX - 4; k--)
                    {
                        if (k >= Size || k<0)
                            break;
                        if (board[k, ShotStartY] == '1')
                            break;
                        else if (board[k, ShotStartY] == 'X')
                        {
                            board[k, ShotStartY] = 'O';
                            CountDoker++;
                            Console.Clear();
                            BoardPrint();
                            Thread.Sleep(1000);
                        }
                    }
                }
                if (CountDoker != 4)
                {
                    for (int k = ShotStartY + 1; k < ShotStartY + 4; k++)
                    {
                        if (k >= Size)
                            break;
                        if (board[ShotStartX, k] == '1')
                            break;
                        else if (board[ShotStartX, k] == 'X')
                        {
                            board[ShotStartX, k] = 'O';
                            CountDoker++;
                            Console.Clear();
                            BoardPrint();
                            Thread.Sleep(1000);
                        }
                    }
                }
                if (CountDoker != 4)
                {
                    for (int k = ShotStartY - 1; k > ShotStartY - 4; k--)
                    {
                        if (k >= Size || k < 0)
                            break;
                        if (board[ShotStartX, k] == '1')
                            break;
                        else if (board[ShotStartX, k] == 'X')
                        {
                            board[ShotStartX, k] = 'O';
                            CountDoker++;
                            Console.Clear();
                            BoardPrint();
                            Thread.Sleep(2000);

                        }
                    }
                }
                Console.WriteLine($"подбит {CountDoker}-палубный корабль");
                Thread.Sleep(5000);
                Console.Clear();
            }
            else
            {
                board[ShotStartX, ShotStartY] = '-';
                BoardPrint();
                Console.WriteLine("мимо");
                Thread.Sleep(500);
                Console.Clear();
            }
        }

        public void MyShot()
        {
            Console.Clear();
            BoardPrint();
            Console.WriteLine("Введите координату X для выстрела:");
            int ShotStartX;
            while (!Int32.TryParse(Console.ReadLine(), out ShotStartX) || ShotStartX < 0 || ShotStartX > 10)
            {
                Console.WriteLine("Некоректный ввод.Введите координату X для выстрела:");
            }
            ShotStartX -= 1;
            Console.WriteLine("Введите координату Y для выстрела:");
            int ShotStartY;
            while (!Int32.TryParse(Console.ReadLine(), out ShotStartY) || ShotStartY < 0 || ShotStartY > 10)
            {
                Console.WriteLine("Некоректный ввод.Введите координату Y для выстрела:");
            }
            ShotStartY -= 1;
            if (board[ShotStartX, ShotStartY] == 'X')
            {
                board[ShotStartX, ShotStartY] = 'V';
                Console.WriteLine($"Вы попали в корабль.");
                Thread.Sleep(1000);
                MyShot();
            }
            else
            {
                board[ShotStartX, ShotStartY] = '-';
                Console.WriteLine("Вы промахнулись.");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }

        public void Game()
        {
            Thread.Sleep(5000);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("         ИГРА НАЧАТА!");
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("             3");
            Thread.Sleep(300);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("             2");
            Thread.Sleep(300);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("             1");
            Thread.Sleep(300);
            Console.Clear();
            while (true)
            {
                AIShot();
                bool flag = false;
                for(var i = 0; i < Size; i++)
                {
                    for (var j = 0; j < Size; j++)
                    {
                        if (board[i, j] == 'X')
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                        break;
                }
                if (!flag)
                {
                    Console.WriteLine("ИГРА ОКОНЧЕНА!");
                    break;
                }
                MyShot();
            }
        }
    }
}
