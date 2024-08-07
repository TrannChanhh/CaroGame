using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;

namespace GameCaro;

class Ref
{
    static int n = 15;
    static string[,] Caro_Table = new string[n, n];
    static int player = 1;
    static bool isWin = false;
    static void Main(string[] args)
    {
        InitCaroTable();
        DrawTable();

        do
        {
            PlayGame();
        } while (!isWin);
    }
    // caro-table có kích cỡ là n x n (với n = 15)
    static void InitCaroTable()
    {
        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < n; col++)
            {
                if (row == 0)
                {
                    if (col < 10)
                    {
                        Caro_Table[row, col] = $" {col}";
                    }
                    else
                    {
                        Caro_Table[row, col] = $"{col}";
                    }
                }
                else if (col == 0)
                {
                    if (row < 10)
                    {
                        Caro_Table[row, col] = $" {row}";
                    }
                    else
                    {
                        Caro_Table[row, col] = $"{row}";
                    }
                }
                else
                {
                    Caro_Table[row, col] = $"-";
                }
            }
        }
    }
    static void DrawTable()
    {
        //Console.Clear();
        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < n; col++)
            {
                if (Caro_Table[row, col] == "-" || Caro_Table[row, col] == "X" || Caro_Table[row, col] == "O")
                {
                    Console.Write($"  {Caro_Table[row, col]} ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" {Caro_Table[row, col]} ");
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }
    }
    static void PlayGame()

    {
        do
        {
            int x, y;
            do
            {
                string p = player == 1 ? "Player 1:" : player == 2 ? "Player 2:" : "";
                Console.WriteLine($"{p}");
                Console.Write($"Input X: ");
                x = Convert.ToInt32(Console.ReadLine());
                Console.Write($"Input Y: ");
                y = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Position");
                Console.ResetColor();
            }
            while (x < 0 || x >= n || y < 0 || y >= n || Caro_Table[x, y] != "-");
            if (player == 1)
            {
                Caro_Table[x, y] = "X";
                player = 2;
            }
            else
            {
                Caro_Table[x, y] = "O";
                player = 1;
            }
            DrawTable();
            if (CheckHorizontal(x, y, "X")) isWin = true;
            if (CheckHorizontal(x, y, "O")) isWin = true;
            if (CheckVertical(x, y, "X")) isWin = true;
            if (CheckVertical(x, y, "O")) isWin = true;
            if (CheckMainDiagonal(x, y, "X")) isWin = true;
            if (CheckMainDiagonal(x, y, "O")) isWin = true;
            if (CheoPhu(x, y, ""))
            {
                Console.WriteLine("Win");
            }
        }
        while (!isWin);
    }

    static bool CheckHorizontal(int row, int col, string player)
    {
        int count = 0;
        for (int i = 1; i < n; i++)
        {
            if (Caro_Table[row, i] == player)
            {
                count++;
                if (count >= 3)
                {
                    Console.Write($"{player} win");
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }
    static bool CheckVertical(int row, int col, string player)
    {
        int count = 0;
        for (int i = 1; i < n; i++)
        {
            if (Caro_Table[i, col] == player)
            {
                count++;
                if (count >= 3)
                {
                    Console.Write($"{player} win");
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }
    static bool CheckMainDiagonal(int row, int col, string player)
    {
        int start_row = 0;
        int start_col = 0;
        int end_row = 0;
        int end_col = 0;
        for (int r = row, c = col; r > 0 && c > 0; r--, c--)
        {
            start_col = c;
            start_row = r;
        }
        for (int r = row, c = col; r < n && c < n; r++, c++)
        {
            end_row = r;
            end_col = c;
        }
        int count = 0;
        for (int r = start_row, c = start_col; r <= end_row && c <= end_col; r++, c++)
        {
            if (Caro_Table[r,c] == player)
            {
                count++;
                if (count == 3)
                {
                    Console.WriteLine($"{player} Win");
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }
    static bool CheoPhu(int row, int col, string playerHam)
    {
        bool check = false;
        int down = 0;
        int up = 0;
        string checkXOrO = playerHam;
        for (int i = 0; i < n; i++)
        {
            if (Caro_Table[row + i, col - i] == checkXOrO)
            {
                down++;
            }else break;
        }
        for (int i = 1; i < n; i++)
        {
            if (Caro_Table[row - i, col + i] == checkXOrO)
            {
                up++;
            }else break;
        }
        if((down + up) >= 3) check = true;
        return check;
    }
}