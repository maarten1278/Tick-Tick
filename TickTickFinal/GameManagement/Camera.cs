using System;

using Microsoft.Xna.Framework;

public class Camera : GameObject
{
    Player player;
    Vector2 windowSize;
    Vector2 scale;

    public Camera() : base()
    {
        position = new Vector2(windowSize.X / 2, windowSize.Y / 2);
    }

    public override void Update(GameTime gameTime)
    {
        position = windowSize / 2 - player.Position;
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

    public Vector2 Scale
    {
        set { scale = value; }
    }
}