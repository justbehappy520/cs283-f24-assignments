using System;
using System.Drawing;
using System.Media;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Windows.Forms;

public class Game
{
    private Player player = new Player();
    private Obstacles obstacles = new Obstacles();
    private bool isBoxVisible = true;
    private int score = 0;
    private GameState currentState = GameState.GameStart;

    public void Setup()
    {
        player.Reset();
        obstacles.Reset();
        score = 0;
    }

    public void Update(float dt)
    {
        player.Update(dt);
        obstacles.Update(dt);
        Intersects();
    }

    public void Draw(Graphics g)
    {
        if (currentState == GameState.GameStart)
        {
            DrawGameStart(g);
            return;
        }

        if (currentState == GameState.GameStop)
        {
            DrawGameStop(g);
            return;
        }

        DrawGround(g);
        DrawBoard(g);

        if (isBoxVisible)
        {
            DrawBox(g);
        }

        obstacles.Draw(g);
        player.Draw(g);
    }

    public void MouseClick(MouseEventArgs mouse)
    {
        if (currentState == GameState.GameStart)
        {
            currentState = GameState.GameRun;
        }
        else if (currentState == GameState.GameStop)
        {
            currentState = GameState.GameRun;
            ResetGame();
        }
    }

    public void KeyDown(KeyEventArgs key)
    {
        if (key.KeyCode == Keys.Oemplus || key.KeyCode == Keys.Add)
        {
            ToggleBoxVisibility();
        }

        player.KeyDown(key);
    }

    // methods to draw and toggle name,  year, title box
    private void DrawBox(Graphics g)
    {
        // draw box
        float boxWidth = 100;
        float boxHeight = 44;
        float boxX = 1;
        float boxY = Window.height - boxHeight;

        Brush boxBrush = new SolidBrush(Color.Black);
        g.FillRectangle(boxBrush, boxX, boxY, boxWidth, boxHeight);

        // write name, class year, game title
        float nameX = (float) (boxWidth * 0.5);
        float nameY = Window.height - 33;
        float titleX = (float) (boxWidth * 0.5);
        float titleY = Window.height - 11;

        Font font = new Font("Arial", 10);
        Brush textBrush = new SolidBrush(Color.White);

        StringFormat format = new StringFormat();
        format.LineAlignment = StringAlignment.Center;
        format.Alignment = StringAlignment.Center;

        g.DrawString("Glory Zhang '26", font, textBrush, nameX, nameY, format);
        g.DrawString("Snacc Attacc", font, textBrush, titleX, titleY, format);
    }

    public void ToggleBoxVisibility()
    {
        isBoxVisible = !isBoxVisible;
    }

    // methods to draw ground
    private void DrawGround(Graphics g)
    {
        float groundHeight = Window.height - 100;

        Color groundColor = ColorTranslator.FromHtml("#964B00");
        Brush groundBrush = new SolidBrush(groundColor);
        g.FillRectangle(groundBrush, 0, groundHeight, Window.width, Window.height);
    }

    // methods to draw scoreboard
    private void DrawBoard(Graphics g)
    {
        // draw scoreboard
        float boardWidth = 100;
        float boardHeight = 30;
        float boardX = Window.width - boardWidth;
        float boardY = 1;

        Brush boardBrush = new SolidBrush(Color.Black);
        g.FillRectangle(boardBrush, boardX, boardY, boardWidth, boardHeight);

        // write score
        float scoreX = Window.width - 50;
        float scoreY = (float)(boardHeight * 0.5);

        Font font = new Font("Arial", 10);
        Brush textBrush = new SolidBrush(Color.White);

        StringFormat format = new StringFormat();
        format.LineAlignment = StringAlignment.Center;
        format.Alignment = StringAlignment.Center;

        g.DrawString("Score: " + score, font, textBrush, scoreX, scoreY, format);
    }

    // methods to check for collisions or intersections
    public void Intersects()
    {
        float px = player.GetX();
        float py = player.GetY();
        float pw = player.GetWidth();
        float ph = player.GetHeight();

        foreach (var obstacle in obstacles.GetObstaclesList())
        {
            if (obstacle.IntersectPoints(px, py, pw, ph, obstacle))
            {
                obstacle.Reset();
                score++;
            }
        }
    }

    // different states of the game
    public enum GameState
    {
        GameStart,
        GameRun,
        GameStop
    }

    public void EndGame()
    {
        currentState = GameState.GameStop;
    }

    private void ResetGame()
    {
        player.Reset();
        obstacles.Reset();
        score = 0;
    }

    public void DrawGameStart(Graphics g)
    {
        float startX = (float)(Window.width * 0.5);
        float startY = (float)(Window.height * 0.5);

        Font font = new Font("Arial", 24);
        Brush textBrush = new SolidBrush(Color.Black);

        StringFormat format = new StringFormat();
        format.LineAlignment = StringAlignment.Center;
        format.Alignment = StringAlignment.Center;

        g.DrawString("Click anywhere to begin.", font, textBrush, startX, startY, format);
    }

    public void DrawGameStop(Graphics g)
    {
        float stopX = (float)(Window.width * 0.5);
        float stopY = (float)(Window.height * 0.5);

        Font font = new Font("Arial", 24);
        Brush textBrush = new SolidBrush(Color.Black);

        StringFormat format = new StringFormat();
        format.LineAlignment = StringAlignment.Center;
        format.Alignment = StringAlignment.Center;

        g.DrawString("GAME OVER.", font, textBrush, stopX, stopY - 50, format);
        g.DrawString("Your Score Is: " + score, font, textBrush, stopX, stopY, format);
        g.DrawString("Click anywhere to begin.", font, textBrush, stopX, stopY + 50, format);
    }
}
