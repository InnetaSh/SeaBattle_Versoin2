using SeaBattle_Versoin2;
using System.Drawing;

int boardSize = 10;
Board AIboard = new Board(boardSize);
Board Myboard = new Board(boardSize);


Myboard.MyGenerateShips();

AIboard.AIGenerateShips();

Console.WriteLine();
Game(AIboard, Myboard);

void Game(Board AIboard, Board Myboard)
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
        AIboard.AIShot(Myboard);
        bool flag = false;
        for (var i = 0; i < AIboard.Size; i++)
        {
            for (var j = 0; j < AIboard.Size; j++)
            {
                if (AIboard.board[i, j] == 'X')
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
        Myboard.MyShot(AIboard);
    }
}