﻿using System;
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
        public int Size;
        public char[,] board;

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
                    board[i, j] = ' ';
                }
            }
        }

        private void BoardPrint(string str)
        {
            Console.WriteLine($"   {str}");
            Console.WriteLine("-------------------");
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

        void VvodCoordinates(int i, out int x, out int y, string Decker)
        {
            Console.WriteLine($"Введите координату X {i + 1}-го {Decker} корабля:"); ;
            if (!int.TryParse(Console.ReadLine(), out x))
            {
                throw new Exception("Введенная строка не является числом.");
            }
            else x -= 1;
            Console.WriteLine($"Введите координату Y {i + 1}-го {Decker} корабля:"); ;
            if (!int.TryParse(Console.ReadLine(), out y))
            {
                throw new Exception("Введенная строка не является числом.");
            }
            else y -= 1;
        }

        bool Direction()
        {
            Console.WriteLine("Выберите расположение корабля (вертикальное - v, горизонтальное - h):");
            var direction = Console.ReadLine();
            while (!(direction == "v" || direction == "h"))
            {
                Console.WriteLine("Не верный ввод.Введите расположение корабля (вертикальное - v, горизонтальное - h):");
                direction = Console.ReadLine();
            }
            return direction == "h";
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

        private void AISingleDeckShips(int x, int y, int shipSize, int kol)
        {
           SingleDeckerFlag = true;

            AIDeckersShips(x, y, shipSize,kol);
            
        }

        private void AIDoubleDeckerShips(int x, int y, int shipSize, int kol)
        {
          
            DoubleDeckerFlag = true;

            AIDeckersShips(x, y, shipSize,kol);
            

        }

        private void AIThreeDeckerShips(int x, int y, int shipSize, int kol)
        {
            ThreeDeckerFlag = true;
            AIDeckersShips(x, y, shipSize,kol);
            
        }

        private void AIFourDeckerShips(int x, int y, int shipSize, int kol)
        {

            FourDeckerFlag = true;
            AIDeckersShips(x, y, shipSize, kol);
        }



        private void MySingleDeckShips(int shipSize, int kol)
        {
            SingleDeckerFlag = true;

            MyDeckersShips(shipSize, kol);

        }

        private void MyDoubleDeckerShips(int shipSize, int kol)
        {

            DoubleDeckerFlag = true;

            MyDeckersShips(shipSize, kol);


        }

        private void MyThreeDeckerShips( int shipSize, int kol)
        {
            ThreeDeckerFlag = true;
            MyDeckersShips(shipSize, kol);

        }

        private void MyFourDeckerShips( int shipSize, int kol)
        {

            FourDeckerFlag = true;
            MyDeckersShips(shipSize, kol);
        }



        private void AIDeckersShips(int x, int y, int shipSize,int kol)
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
        private List<string> checkCells = new List<string>();
        void MyDeckersShips(int shipSize, int kol)
        {
            for (int i = 0; i < kol; i++)
            {
                int startX;
                int startY;

                try
                {
                    VvodCoordinates(i, out startX, out startY, $"{shipSize} -палубного");
                    
                    if (shipSize == 1)
                        isHorizontal = true;
                    else
                        isHorizontal = Direction();
                    var msg = "";
                    if (!isHorizontal)
                    {
                        if (IsValidPlaceVer(startX, startY, shipSize, out msg))
                        {
                            for (int k = startX; k < startX + shipSize; k++)
                            {
                                board[k, startY] = 'X';
                            }
                            IsOccupiedCellsVer(startX, startY, shipSize);
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                    else if (isHorizontal)
                    {
                        if (IsValidPlaceHor(startX, startY, shipSize, out msg))
                        {
                            for (int k = startY; k < startY + shipSize; k++)
                            {
                                board[startX, k] = 'X';
                            }
                            IsOccupiedCellsHor(startX, startY, shipSize);
                        }
                        else
                        {
                            throw new Exception(msg);
                        }
                    }
                    Console.Clear();
                    BoardPrint("Ваше поле.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    i--;
                }
            }

        }

        
        

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
        public void AIGenerateShips()
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
                    AISingleDeckShips(startX, startY, ShipSize, 4);
                    if (checkCells.Count > 100)
                    {
                        clear();
                    }
                }
                if (ShipSize == 2 && !DoubleDeckerFlag)
                {
                    AIDoubleDeckerShips(startX, startY, ShipSize, 3);
                    if (checkCells.Count > 100)
                    {
                        clear();
                    }
                }
                if (ShipSize == 3 && !ThreeDeckerFlag)
                {
                    AIThreeDeckerShips(startX, startY, ShipSize, 2);
                    if (checkCells.Count > 100)
                    {
                        clear();
                    }
                }
                if (ShipSize == 4 && !FourDeckerFlag)
                {
                    AIFourDeckerShips(startX, startY, ShipSize,1);
                    if (checkCells.Count > 100)
                    {
                        clear();
                    }
                }
            }
            BoardPrint("Поле соперника.");
        }

        public void MyGenerateShips()
        {
            InitializeBoard();
            BoardPrint("Ваше поле.");

            while (!SingleDeckerFlag || !DoubleDeckerFlag || !ThreeDeckerFlag || !FourDeckerFlag)
            {
                Console.Clear();
                BoardPrint("Ваше поле.");
                Thread.Sleep(1000);
                if (SingleDeckerFlag && DoubleDeckerFlag && ThreeDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (4):");
                }
                else if (SingleDeckerFlag && DoubleDeckerFlag && FourDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (3):");
                }
                else if (SingleDeckerFlag && ThreeDeckerFlag && FourDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (2):");
                }
                else if (DoubleDeckerFlag && ThreeDeckerFlag && FourDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (1):");
                }
                else if (SingleDeckerFlag && DoubleDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (3, 4):");
                }
                else if (SingleDeckerFlag && ThreeDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (2, 4):");
                }
                else if (SingleDeckerFlag && FourDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (2, 3):");
                }
                else if (DoubleDeckerFlag && ThreeDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (1, 4):");
                }
                else if (DoubleDeckerFlag && FourDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (1, 3):");
                }
                else if (ThreeDeckerFlag && FourDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (1, 2):");
                }
                else if (SingleDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (2, 3, 4):");
                }
                else if (DoubleDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (1, 3, 4):");
                }
                else if (ThreeDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (1, 2, 4):");
                }
                else if (FourDeckerFlag)
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (1, 2, 3):");
                }

                else
                {
                    Console.WriteLine("Выберете палубность корабля, которые хотите расставить (1 - 4):");
                    Console.WriteLine("однопалубный корабль - 4шт.");
                    Console.WriteLine("двухпалубный корабль - 3шт.");
                    Console.WriteLine("трехпалубный корабль - 2шт.");
                    Console.WriteLine("четырехпалубный корабль - 1шт.");
                }

                int count;
                Console.Write("count = ");
                while (!Int32.TryParse(Console.ReadLine(), out count) || count < 1 || count > 4)
                {
                    Console.WriteLine("Не верный ввод.Введите число:");
                    Console.Write("count = ");
                }
                
                switch (count)
                {
                    case 1:
                        if (!SingleDeckerFlag)
                        {
                            MySingleDeckShips( 1, 4);
                        }
                        else
                            Console.WriteLine("Такой корабль уже расставлен.");
                        break;
                    case 2:
                        if (!DoubleDeckerFlag)
                        {
                            
                            MyDoubleDeckerShips( 2, 3);
                        }
                        else
                            Console.WriteLine("Такой корабль уже расставлен.");
                        break;
                    case 3:
                        if (!ThreeDeckerFlag)
                        {
                            MyThreeDeckerShips( 3, 2);
                        }
                        else
                            Console.WriteLine("Такой корабль уже расставлен.");
                        break;
                    case 4:
                        if (!FourDeckerFlag)
                        {
                            MyFourDeckerShips( 4, 1);
                        }
                        else
                            Console.WriteLine("Такой корабль уже расставлен.");
                        break;
                }
                BoardPrint("Ваше поле.");
            }
            Console.Clear();
        }


        public void AIShot(Board obj)
        {
            Random rnd = new Random();

            int ShotStartX = rnd.Next(0, Size);
            int ShotStartY = rnd.Next(0, Size);
            int CountDoker = 0;
            if (obj.board[ShotStartX, ShotStartY] == 'X')
            {
                CountDoker++;
                obj.board[ShotStartX, ShotStartY] = 'O';
                obj.BoardPrint("Ваше поле.");
                Console.WriteLine("\n--------------------------------------------\n");
                BoardPrint("Поле соперника.");
                Thread.Sleep(1000);


                for (int k = ShotStartX + 1; k < ShotStartX + 4; k++)
                {
                    if (k >= Size)
                        break;
                    if (obj.board[k, ShotStartY] == '1')
                        break;
                    else if (obj.board[k, ShotStartY] == 'X')
                    {
                        obj.board[k, ShotStartY] = 'O';
                        CountDoker++;
                        Console.Clear();
                        obj.BoardPrint("Ваше поле.");
                        Console.WriteLine("\n--------------------------------------------\n");
                        BoardPrint("Поле соперника.");
                        Thread.Sleep(1000);
                    }
                }
                if (CountDoker != 4)
                {
                    for (int k = ShotStartX - 1; k > ShotStartX - 4; k--)
                    {
                        if (k >= Size || k<0)
                            break;
                        if (obj.board[k, ShotStartY] == '1')
                            break;
                        else if (obj.board[k, ShotStartY] == 'X')
                        {
                            obj.board[k, ShotStartY] = 'O';
                            CountDoker++;
                            Console.Clear();
                            obj.BoardPrint("Ваше поле.");
                            Console.WriteLine("\n--------------------------------------------\n");
                            BoardPrint("Поле соперника.");
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
                        if (obj.board[ShotStartX, k] == '1')
                            break;
                        else if (obj.board[ShotStartX, k] == 'X')
                        {
                            obj.board[ShotStartX, k] = 'O';
                            CountDoker++;
                            Console.Clear();
                            obj.BoardPrint("Ваше поле.");
                            Console.WriteLine("\n--------------------------------------------\n");
                            BoardPrint("Поле соперника.");
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
                        if (obj.board[ShotStartX, k] == '1')
                            break;
                        else if (obj.board[ShotStartX, k] == 'X')
                        {
                            obj.board[ShotStartX, k] = 'O';
                            CountDoker++;
                            Console.Clear();
                            obj.BoardPrint("Ваше поле.");
                            Console.WriteLine("\n--------------------------------------------\n");
                            BoardPrint("Поле соперника.");
                            Thread.Sleep(2000);

                        }
                    }
                }
                Console.Clear();
                obj.BoardPrint("Ваше поле.");
                Console.WriteLine($"подбит Ваш {CountDoker}-палубный корабль");
                Console.WriteLine("\n--------------------------------------------\n");
                BoardPrint("Поле соперника.");
                Thread.Sleep(5000);
                Console.Clear();
            }
            else
            {
                obj.board[ShotStartX, ShotStartY] = '-';
                obj.BoardPrint("Ваше поле.");
                Console.WriteLine("МИМО!");
                Console.WriteLine("\n--------------------------------------------\n");
                BoardPrint("Поле соперника.");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }

        public void MyShot(Board obj)
        {
            Console.Clear();
            BoardPrint("Ваше поле.");
            Console.WriteLine("\n--------------------------------------------\n");
            obj.BoardPrint("Поле соперника.");
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
            if (obj.board[ShotStartX, ShotStartY] == 'X')
            {
                obj.board[ShotStartX, ShotStartY] = 'O';
                Console.WriteLine($"Вы попали в корабль.");
                Thread.Sleep(1000);
                MyShot(obj);
            }
            else
            {
                obj.board[ShotStartX, ShotStartY] = '-';
                Console.WriteLine("Вы промахнулись.");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }

    }
}
