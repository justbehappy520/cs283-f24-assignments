using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Reflection;
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
                GameOver();
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

        // methods to check for collisions
        public bool IntersectPoints(float px, float py, float pw, float ph, Obstacle obstacle)
        {
            float x = obstacle.GetX();
            float y = obstacle.GetY();
            float w = obstacle.GetWidth();
            float h = obstacle.GetHeight();

            // top left of obstacle in player
            if (x >= px && x <= px + pw && y <= py + ph && y >= py)
            {
                return true;
            }
            // top right of obstacle in player
            else if (x + w >= px && x + w <= px + pw && y <= py + ph && y >= py)
            {
                return true;
            }
            // bottom left of obstacle in player
            else if (x >= px && x <= px + pw && y + h <= py + ph && y + h >= py)
            {
                return true;
            }
            // bottom right of obstacle in player
            else if (x + w >= px && x + w <= px + pw &&
                y + h <= py + ph && y + h >= py)
            {
                return true;
            }
            return false;
        }

        // method to end game
        private void GameOver()
        {
            EndGame();
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

    public List<Obstacle> GetObstaclesList()
    {
        return obstacleList;
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
}