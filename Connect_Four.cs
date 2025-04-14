using System;

using static System.Console;

using System.Drawing;

using static System.Math;

using System.Windows.Forms;



delegate void Notify();



class Board

{

    int[,] square = new int[9, 8];  // 0 = empty, 1 = player 1, 2 = player 2



    public int player = 1;   // whose turn it is

    public int winner = 0;   // who has won

    public int win_x, win_y, win_dx, win_dy;  // vector indicating winning squares



    public event Notify changed;  // fires whenever the board changes



    public int this[int x, int y]

    {

        get => square[x, y];

    }



    // Return true if the current player has four in a row

    // starting at (x, y) and moving in direction (dx, dy).

    bool all(int x, int y, int dx, int dy)

    {

        for (int i = 0; i < 4; ++i)

            if (square[x + i * dx, y + i * dy] != player)

                return false;

        win_x = x; win_y = y; win_dx = dx; win_dy = dy;

        return true;

    }

    // Return true if the current player has won.

    bool checkWin()

    {

        for (int i = 0; i < 9; i++) //check row

            for (int j = 0; j < 6; j++)

            {

                if (all(i, j, 1, 0))

                    return true;

            }

        for (int i = 0; i < 8; ++i) //check column

            for (int j = 0; j < 7; ++j)

            {

                if (all(j, i, 0, 1))

                    return true;

            }

        // check diagonals

        for (int j = 0; j <= 3; ++j)

            for (int i = 0; i < 3; ++i)

                    if (all(j, i, 1, 1))

                        return true;

        for (int i = 0; i <= 3; ++i)

            for (int j = 3; j < 6; ++j)

                if (all(i, j, 1, -1))

                    return true;

        return false;

    }



    // Make the current player play at (x, y).  Return true if the move was legal.

    public bool move(int x, int y)

    {

        if (square[x, y] > 0)  // square is occupied

            return false;



        square[x, y] = player;

        if (checkWin())

            winner = player;

        else player = 3 - player;

        changed();

        return true;

    }

}



// A graphical interface for the game



class View : Form

{

    const int SquareSize = 100;

    const int MarginSize = SquareSize / 4;



    Board board;

    Pen pen = new Pen(Color.Black, 0);



    View()

    {

        Text = "Connect four";

        ClientSize = new Size(800, 800);

        StartPosition = FormStartPosition.CenterScreen;



        MouseDown += onMouseDown;

        Paint += onPaint;

        init();

    }



    void init()

    {

        board = new Board();

        Invalidate();                 // redraw the board now

        board.changed += Invalidate;  // and whenever the board state changes

    }



    void drawRed(Graphics g, RectangleF r, float x, float y) // drawing red circles

    {

        g.DrawEllipse(pen, r);

        g.FillEllipse(Brushes.Red, x + 0.1f, y + 0.1f, 0.8f, 0.8f);

    }

    void drawWhite(Graphics g, RectangleF r, float x, float y) // drawing "empty" circles

    {

        g.DrawEllipse(pen, r);

        g.FillEllipse(Brushes.White, x + 0.1f, y + 0.1f, 0.8f, 0.8f);

    }

    void drawYellow(Graphics g, RectangleF r, float x, float y) // drawing yellow circles

    {

        g.DrawEllipse(pen, r);

        g.FillEllipse(Brushes.Gold, x + 0.1f, y + 0.1f, 0.8f, 0.8f);

    }



    void onMouseDown(object sender, MouseEventArgs args)

    {

        if (board.winner > 0)

            init();   // start a new game

        else

        {

            int x = ((int)(args.X - MarginSize)) / SquareSize;

            if (board[x, 0] == 0) // falling of circles down

                for (int y = 0; y < 6; y++)

                    if (board[x, y] != 0)

                    {

                        board.move(x, y-1);

                    }

            board.move(x, 5);

        }

    }



    void onPaint(object sender, PaintEventArgs args)

    {

        Graphics g = args.Graphics;



        // Set a transformation matrix so that squares are 1 unit tall/wide, with a

        // margin around the drawing area.

        g.TranslateTransform(MarginSize, MarginSize);

        g.ScaleTransform(SquareSize, SquareSize);

       Rectangle rect1 = new Rectangle(0, 0, 7, 6); // draw big blue rectangle, the field

       g.FillRectangle(Brushes.Blue, rect1);

         for (int x = 0; x < 7; ++x)

           for (int y = 0; y < 6; ++y)

           {

             RectangleF rect = new RectangleF(x + 0.1f, y + 0.1f, 0.8f, 0.8f);

             drawWhite(g, rect, x, y);

           }

        if (board.winner > 0) // showing winning combination

            for (int i = 0; i < 4; ++i)

                g.FillRectangle(Brushes.LightGreen,

                  board.win_x + i * board.win_dx, board.win_y + i * board.win_dy,

                  1, 1);



        // Draw the X's and O's.

        for (int x = 0; x < 7; ++x)

            for (int y = 0; y < 6; ++y)

            {

                RectangleF rect = new RectangleF(x + 0.1f, y + 0.1f, 0.8f, 0.8f);



                switch (board[x, y])

                {

                    case 1: drawRed(g, rect, x, y); break;

                    case 2: drawYellow(g, rect, x, y); break;

                }

            }

    }

    static void Main()

    {

        Application.Run(new View());

    }

}
