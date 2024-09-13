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

    public void Setup()
    {
        player.Reset();
        obstacles.Reset();
    }

    public void Update(float dt)
    {
        player.Update(dt);
        obstacles.Update(dt);
    }

    public void Draw(Graphics g)
    {
        DrawGround(g);

        if (isBoxVisible)
        {
            DrawBox(g);
        }

        obstacles.Draw(g);
        player.Draw(g);
    }

    public void MouseClick(MouseEventArgs mouse)
    {
        if (mouse.Button == MouseButtons.Left)
        {
            System.Console.WriteLine(mouse.Location.X + ", " + mouse.Location.Y);
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
        float groundHeight = Window.height - 50;

        Color groundColor = ColorTranslator.FromHtml("#964B00");
        Brush groundBrush = new SolidBrush(groundColor);
        g.FillRectangle(groundBrush, 1, groundHeight, Window.width, Window.height);
    }
}
