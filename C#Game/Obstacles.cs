using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

public class Obstacles
{
    public class Obstacle
    {
        private float x;
        private float y;
        private float speed;
        private float width = 20;
        private float height = 20;
        private static Random rng = new Random();

        public Obstacle()
        {
            Reset();
        }

        public void Reset()
        {
            x = (float)(rng.Next(0, (int)(Window.width) - (int)(width)));
            y = -height;

            speed = 100.0f;
        }

        public void Update(float dt)
        {
            y += speed * dt;

            if (FallenOffScreen())
            {
                Reset();
            }
        }

        public void Draw(Graphics g)
        {
            DrawObstacle(g);
        }

        // method to draw obstacles
        private void DrawObstacle(Graphics g)
        {
            Color color = ColorTranslator.FromHtml("#107AB0");
            Brush brush = new SolidBrush(color);
            g.FillRectangle(brush, x, y, width, height);
        }

        // method to clean up obstacles
        public bool FallenOffScreen()
        {
            return y > Window.height;
        }

        // getter methods??
        public float GetX()
        {
            return x;
        }

        public float GetY()
        {
            return y;
        }

        public float GetW()
        {
            return width;
        }

        public float GetH()
        {
            return height;
        }
    }
    // for the management of more than one obstacle, currently only have one
    private List<Obstacle> obstacleList;
    private int maxObstacles = 1;

    public Obstacles()
    {
        obstacleList = new List<Obstacle>();
        for (int i = 0; i < maxObstacles; i++)
        {
            obstacleList.Add(new Obstacle());
        }
    }

    public void Reset()
    {
        foreach (var obstacle in obstacleList)
        {
            obstacle.Reset();
        }
    }

    public void Draw(Graphics g)
    {
        foreach (var obstacle in obstacleList)
        {
            obstacle.Draw(g);
        }
    }

    public void Update(float dt)
    {
        // updates individual obstacles in the list
        foreach (var obstacle in obstacleList)
        {
            obstacle.Update(dt);
        }

        // removes the obstacle at the end
        for (int i = obstacleList.Count - 1; i >= 0; i--)
        {
            if (obstacleList[i].FallenOffScreen())
            {
                obstacleList.RemoveAt(i);
            }
        }
    }

    // getter methods??
    public float GetX()
    {
        return Obstacle.GetX();
    }

    public float GetY()
    {
        return Obstacle.GetY();
    }

    public float GetW()
    {
        return Obstacle.GetW();
    }

    public float GetH()
    {
        return Obstacle.GetH();
    }
}