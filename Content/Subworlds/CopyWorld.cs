using System.Collections.Generic;
using WorldLoopEdge.Common.Configs;
using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace WorldLoopEdge.Content.Subworlds;

public class CopyWorld : BaseSubWorld<CopyWorld>
{
	public override int Width => Main.maxTilesX;
	public override int Height => Main.maxTilesY;

	public override bool ShouldSave => true;
	public override bool NoPlayerSaving => false;
	public override bool hideMapRespawn => !ModContent.GetInstance<ServerConfig>().AllowRespawn;
	public override bool allowRespawn => ModContent.GetInstance<ServerConfig>().AllowRespawn;
	public override string Name => WorldLoop.Instance.seedOffset.ToString();

	public override List<GenPass> Tasks => new List<GenPass>()
	{
		new CopyWorldGen.CopyWorldGenPass()
	};
	public override void PostUpdate()
	{
	    Main.time++;

	    if (Main.dayTime)
	    {
	        if (Main.time >= 54000.0)
	        {
	            Main.time = 0;
	            Main.dayTime = false;
	        }
	    }
	    else
	    {
	        if (Main.time >= 32400.0)
	        {
	            Main.time = 0;
	            Main.dayTime = true;
	        }
	    }

	    base.PostUpdate();
	}

	public override void OnEnter()
    {
        SubworldSystem.hideUnderworld = false;
        SubworldSystem.noReturn = !ModContent.GetInstance<ServerConfig>().AllowReturn;
        base.OnEnter();
    }
	public override void OnExit()
    {
        if (!UpdateWorldSystems.Instance.isChunkTransition)
        {
		    WorldLoopPlayer.Instance.subworldTransitionPosition = Main.LocalPlayer.position;
	        WorldLoopPlayer.Instance.subworldTransitionVelocity = Main.LocalPlayer.velocity;
            WorldLoop.Instance.seedOffset = 0;
        }

        base.OnExit();
    }

	public void FlipPosition(bool rightEnter, bool saveVelocity)
	{
		if (rightEnter)
		{
			// from right side
			Vector2 spawnPosition = new Vector2(5 * 16 + WorldLoop.Instance.relogicWorldBoundaryPixels.X, WorldLoopPlayer.Instance.subworldTransitionPosition.Y);
			Main.LocalPlayer.Teleport(spawnPosition,TeleportationStyleID.DebugTeleport);
		} else
		{
			// from left side
			Vector2 spawnPosition = new Vector2((Main.maxTilesX - 5) * 16 - WorldLoop.Instance.relogicWorldBoundaryPixels.X, WorldLoopPlayer.Instance.subworldTransitionPosition.Y);
			Main.LocalPlayer.Teleport(spawnPosition,TeleportationStyleID.DebugTeleport);
		}
		if (saveVelocity)
		{
			Main.LocalPlayer.velocity = WorldLoopPlayer.Instance.subworldTransitionVelocity;
		} else
		{
			Main.LocalPlayer.velocity = Vector2.Zero;
		}
	}

	public override void PostExit()
	{
		base.PostExit();
		if (WorldLoop.Instance.seedOffset == 0)
		{
			if (!UpdateWorldSystems.Instance.isChunkTransition)
			{
				FlipPosition(WorldLoop.Instance.seedOffset > 0,false);
			} else
			{
				FlipPosition(WorldLoopPlayer.Instance.onWorldRight,true);
			}
		}
	}

	public override void PostEnter()
	{
		base.PostEnter();
		FlipPosition(WorldLoopPlayer.Instance.onWorldRight,true);
	}
}