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
        private float width = 10;
        private float height = 10;
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

        // method to draw boundaries
        public Rectangle GetBounds()
        {
            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }
    }
    private List<Obstacle> obstacleList;
    private int maxObstacles = 1;
    private float spawnDelay = 7.0f;
    private float timeSinceLastSpawn = 0.0f;

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
        timeSinceLastSpawn = 0.0f;
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

        // spawns in new obstacles once delay time has eclipsed
        timeSinceLastSpawn += dt;
        if (timeSinceLastSpawn >= spawnDelay)
        {
            obstacleList.Add(new Obstacle());
            timeSinceLastSpawn = 0.0f;
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

    // methods to check for collisions
    public static bool CheckCollision(Player player)
    {
        Rectangle boundaries = Obstacle.GetBounds();
        return boundaries.IntersectsWith(player.GetBounds());
    }
}