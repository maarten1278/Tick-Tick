using System;
using Microsoft.Xna.Framework;

public class Camera : GameObject
{
    Player player;

    public Camera() : base()
    {
        position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
        visible = false;
    }

    public override void Update(GameTime gameTime)
    {
        Console.WriteLine(player.Position.X);
        if (player != null)
        {
            position.X =  player.Position.X - GameEnvironment.Screen.X / 2;
            position.Y = player.Position.Y + GameEnvironment.Screen.Y / 2;
            Console.WriteLine(player.Position.X);
        }
    }

    public Player Player
    {
        set { value = player; }
    }
}