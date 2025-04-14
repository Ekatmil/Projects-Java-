using System;
using static System.Console;
using static System.Math;

class Board
{
    public int[,] square;
    int moves = 0;
    //public int player = 1;   // whose turn it is
    public int winner = 0;   // who has won
    public int win_x, win_y, win_dx, win_dy;  // vector indicating winning squares

    public Board() { square = new int[3, 3]; }

    public int size => square.Length;

    bool all(int player, int x, int y, int dx, int dy)
    {
        for (int i = 0; i < 3; ++i)
            if (square[x + i * dx, y + i * dy] != player)
                return false;

        win_x = x; win_y = y; win_dx = dx; win_dy = dy;
        return true;
    }
    // Return 1 if X wins, -1 if O wins, 0 if a draw.
    public int? result(int player)
    {
        // check rows and columns
        for (int i = 0; i < 3; ++i)
            if (all(player,i, 0, 0, 1) || all(player, 0, i, 1, 0))
            {
                if (player == 1)
                    return 1;
                if (player == 2)
                    return -1;
            }                

        // check diagonals
        if (all(player,0, 0, 1, 1) || all(player,2, 0, -1, 1))
        {
            if (player == 1)
                return 1;
            if (player == 2)
                return -1;
        }
        int count = 0;
        for (int i = 0; i < 3; ++i)
            for (int j = 0; j < 3; ++j)
                if (square[i, j] == 1 || square[i, j] == 2)
                    count += 1;
        if (count == 9)
        return 0;
        return null;
    }

    public bool move1(int player, int i, int j)
    {
        if (square[i, j] == 0)
        {
            square[i, j] = player;
            ++moves;
            return true;
        }
        else return false;
    }

    public bool move(int player, int i, int j)
    {
        if (square[i,j] == 0)
        {
            square[i,j] = player;
            ++moves;
            return true;
        }
        else return false;
    }

    public void unmove(int i, int j)
    {
        square[i,j] = 0;
        --moves;
    }
}

class Minimax
{
    // Return the best possible score and best move for the current player.
    public static int minimax(Board b, int player, out int move, out int move1)
    {
        move = -1;
        move1 = -1;
        if (b.result(player) is int r)
            return r;  // game is done

        int m = player == 1 ? int.MinValue : int.MaxValue;
        for (int i = 0; i < 3; ++i)
            for(int j = 0; j <3; ++j)
            if (b.move(player, i,j))
            {
                int n = minimax(b, 3 - player, out int dummy,out int dummy1);
                b.unmove(i,j);

                if (player == 1)
                {
                    if (n > m)
                    {
                        m = n;
                        move = i;
                        move1 = j;// the best move so far
                    }
                }
                else
                { // player 2
                    if (n < m)
                    {
                        m = n;
                        move1 = j;
                        move = i;  // the best move so far
                    }
                }
            }  // end if
        return m;
    }
}

class Game
{
    const string Symbols = ".XO";
    static void Main()
    {
        Board b = new Board();
        for (int i = 0; i < 3; ++i)
        {
            string s = ReadLine();
            for (int k = 0; k < 3; k++)
            switch (s[k])
            {
                case 'X': b.square[i, k] = 1;  break;
                case 'O': b.square[i, k] = 2;  break;
                case '.': b.square[i, k] = 0; break;
                default: throw new Exception("bad response");
            }
        }
        int player = 0;
        string r = ReadLine();
        int player1;
        if (r[0] == 'X') { player = 1;  player1 =1; }
        else { player = 2;  player1 = -1; }
        while (true)
        {
            if (b.result(player) is int winner)
            {
                if (winner == player1) WriteLine("VYHRAJE");
                else if (winner == 0) WriteLine("REMIZUJE");
                else if (winner != player1) WriteLine("PROHRAJE");

                return;
            }
            int move;
            int move1;
            Minimax.minimax(b, player, out move, out move1);
            b.move(player, move, move1);
            if (b.result(player) is int winner1)
            {
                if (winner1 == player1) WriteLine("VYHRAJE");
                else if (winner1 == 0) WriteLine("REMIZUJE");
                else if (winner1 != player1) WriteLine("PROHRAJE");
                
                return;
            }

            player = 3 - player;
        }
    }
}
