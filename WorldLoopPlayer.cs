using Microsoft.Xna.Framework;
using WorldLoopEdge.Content.Subworlds;
using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;

namespace WorldLoopEdge;

public class WorldLoopPlayer : ModPlayer
{
    public static WorldLoopPlayer Instance => Main.LocalPlayer.GetModPlayer<WorldLoopPlayer>();
    public Vector2 subworldTransitionPosition = Vector2.Zero; // Used for subworld Entry x & Exit x
    public Vector2 subworldTransitionVelocity = Vector2.Zero; // Used for subworld Entry x & Exit x
    public bool hasSpawnpoint => new Vector2(-1,-1) != new Vector2(Main.LocalPlayer.SpawnX,Main.LocalPlayer.SpawnY);
    public bool onWorldRight => Instance.subworldTransitionPosition.X > (Main.maxTilesX * 16f / 2);
    public bool justJoined = true;

    public override void PreUpdateMovement()
    {
        if (justJoined)
        {
            WorldLoop.Instance.InitWorld();
            justJoined = false;
        }

        float rightBoundary = (Main.maxTilesX - 3) * 16f - WorldLoop.Instance.relogicWorldBoundaryPixels.X;
        if (Main.LocalPlayer.position.X > rightBoundary)
        {
            UpdateWorldSystems.Instance.MoveWorlds(1);
        }

        float leftBoundary = WorldLoop.Instance.relogicWorldBoundaryPixels.X + 1 * 16f;
        if (Main.LocalPlayer.position.X < leftBoundary)
        {
            UpdateWorldSystems.Instance.MoveWorlds(-1);
        }

        base.PreUpdateMovement();
    }

    public override void OnRespawn()
    {
        if (SubworldSystem.Current is ISubworldPost currentCustomSubworld)
        {
            currentCustomSubworld.BasePlayerDeath();
        }
    }

    public string lastWorld = "Main";

    public override void OnEnterWorld()
    {
        if (!string.IsNullOrEmpty(lastWorld) && lastWorld != "Main")
        {
            if (Mod.TryFind<Subworld>(lastWorld, out var previousSubworld))
            {
                if (previousSubworld is ISubworldPost previousCustomSubworld)
                {
                    previousCustomSubworld.BasePostExit();
                }
            }
        }

        if (WorldLoop.Instance.currentWorldID == "Main" && subworldTransitionPosition != Vector2.Zero && subworldTransitionVelocity != Vector2.Zero)
        {
            CopyWorld.Instance.FlipPosition(onWorldRight,true);
        }

        if (SubworldSystem.Current is ISubworldPost currentCustomSubworld)
        {
            currentCustomSubworld.BasePostEnter();
        }

        lastWorld = WorldLoop.Instance.currentWorldID;
    }
}