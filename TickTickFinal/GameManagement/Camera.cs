using Microsoft.Xna.Framework;

public class Camera : GameObject
{
    bool isActive;

    public Camera()
        : base()
    {
        isActive = false;
        position = new Vector2(300, 300);
    }

    public void Update()
    {
        Player player = GameWorld.Find("player") as Player;
        position.X = GameEnvironment.Screen.X / 2 - player.Position.X;
        position.Y = GameEnvironment.Screen.Y / 2 - player.Position.Y;
    }

    public bool IsActive
    {
        get { return isActive; }
        set { value = isActive; }
    }
}