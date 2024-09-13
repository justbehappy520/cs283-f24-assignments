using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

public class Player
{
    // instance variables for Player's position and size
    private float x;
    private float y;
    private float width = 50;
    private float height = 50;
    private float speed; // speed of the player's character


    public Player()
    {
        Reset();
    }

    public void Reset()
    {
        x = (float)(Window.width * 0.5) - (float)(width * 0.5);
        y = (Window.height - 100) - (float)(height * 0.75);

        speed = 10.0f;
    }

    public void Update(float dt)
    {
    }

    public void Draw(Graphics g)
    {
        Brush brush = new SolidBrush(Color.Red);
        g.FillRectangle(brush, x, y, width, height);
    }

    // methods for movement
    public void KeyDown(KeyEventArgs key)
    {
        if (key.KeyCode == Keys.D || key.KeyCode == Keys.Right)
        {
            x += speed;
            if (x + width > Window.width)
            {
                x = Window.width - width; // prevent from moving off screen
            }
        }
        else if (key.KeyCode == Keys.A || key.KeyCode == Keys.Left)
        {
            x -= speed;
            if (x < 0)
            {
                x = 0; // prevent from moving off screen
            }
        }
    }

    // getter methods for collision checking
    public float GetX()
    {
        return x;
    }

    public float GetY()
    {
        return y;
    }

    public float GetWidth()
    {
        return width;
    }

    public float GetHeight()
    {
        return height;
    }
}