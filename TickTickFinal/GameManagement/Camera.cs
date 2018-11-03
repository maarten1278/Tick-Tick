using System;

using Microsoft.Xna.Framework;

public class Camera : GameObject
{
    Player player;
    Vector2 windowSize;
    float scale;

    public Camera(float scale) : base()
    {
        position = new Vector2(windowSize.X / 2, windowSize.Y / 2);
    }

    public override void Update(GameTime gameTime)
    {
        Console.WriteLine(player.Center);
           position = (player.Position + player.Center * scale) - windowSize / 2;
    }

    public override void Reset()
    {
        position = Vector2.Zero;
    }

    public Player Player
    {
        set { player = value; }
    }

    public Vector2 WindowSize
    {
        set { windowSize = value; }
    }
}