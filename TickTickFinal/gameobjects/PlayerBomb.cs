using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


class PlayerBomb : AnimatedGameObject
{
    float previousYPosition;
    float bombTime;
    bool exploding;

    public PlayerBomb(Player player)
    {
        LoadAnimation("Sprites/Player/spr_idle", "default", true);
        LoadAnimation("Sprites/Player/spr_explode@5x5", "explode", false, 0.04f);
        PlayAnimation("default");
        velocity.Y = -900;
        velocity.X = 400;
        if (player.Mirror)
        {
            velocity.X *= -1;
        }
        Mirror = player.Mirror;
        bombTime = 1.5f;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (exploding)
            return;
        velocity.Y += 55;
        HandleCollision();
        CheckEnemyCollision();
        // check if we are outside the screen
        Rectangle screenBox = new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y);
        if (!screenBox.Intersects(this.BoundingBox))
        {
            MeGone();
        }
        if (bombTime <= 0)
        {
            PlayAnimation("explode");
            velocity = Vector2.Zero;
            exploding = true;
        }
        bombTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    private void HandleCollision()
    {
        TileField tiles = GameWorld.Find("tiles") as TileField;
        int xFloor = (int)position.X / tiles.CellWidth;
        int yFloor = (int)position.Y / tiles.CellHeight;

        for (int y = yFloor - 2; y <= yFloor + 1; ++y)
        {
            for (int x = xFloor - 1; x <= xFloor + 1; ++x)
            {
                TileType tileType = tiles.GetTileType(x, y);
                if (tileType == TileType.Background)
                {
                    continue;
                }
                Tile currentTile = tiles.Get(x, y) as Tile;
                Rectangle tileBounds = new Rectangle(x * tiles.CellWidth, y * tiles.CellHeight,
                                                        tiles.CellWidth, tiles.CellHeight);
                Rectangle boundingBox = this.BoundingBox;
                boundingBox.Height += 1;
                if (((currentTile != null && !currentTile.CollidesWith(this)) || currentTile == null) && !tileBounds.Intersects(boundingBox))
                {
                    continue;
                }
                Vector2 depth = Collision.CalculateIntersectionDepth(boundingBox, tileBounds);
                if (Math.Abs(depth.X) < Math.Abs(depth.Y))
                {
                    continue;
                }
                if (previousYPosition <= tileBounds.Top && tileType != TileType.Background)
                {
                    velocity.Y = 0;
                    velocity.X = 0;
                }
            }
        }
        position = new Vector2((float)Math.Floor(position.X), (float)Math.Floor(position.Y));
        previousYPosition = position.Y;
    }

    void MeGone()
    {
        (parent as GameObjectList).Remove(this);
    }

    public void CheckEnemyCollision()
    {
        GameObjectList enemies = GameWorld.Find("enemies") as GameObjectList;
        foreach (GameObject enemy in enemies.Children)
            if (CollidesWith(enemy as AnimatedGameObject) && visible)
            {
                enemy.BeGone = true;
                enemy.Visible = false;
                PlayAnimation("explode");
                velocity = Vector2.Zero;
                exploding = true;
            }
    }
}
