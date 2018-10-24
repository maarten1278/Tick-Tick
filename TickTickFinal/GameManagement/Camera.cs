using Microsoft.Xna.Framework;

class Camera : GameObject
{
    Vector2 offset, origin;

    public Camera() : base(0, "Camera")
    {
        Player player = GameWorld.Find("player") as Player;
        //position = Vector2.Zero;
        origin  = new Vector2 (GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
        position = player.Position;
        //foreach (GameObject x in Children)
        
    }
}

