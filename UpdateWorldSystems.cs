using WorldLoopEdge.Common.Configs;
using Microsoft.Xna.Framework;
using WorldLoopEdge.Content.Subworlds;
using SubworldLibrary;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace WorldLoopEdge;

public class UpdateWorldSystems : ModSystem
{
    public static UpdateWorldSystems Instance => ModContent.GetInstance<UpdateWorldSystems>();
	public bool isChunkTransition = false;
	public override void PostDrawFullscreenMap(ref string mouseText)
    {
	    if (ModContent.GetInstance<ClientConfig>().EnableMapText && !ModContent.GetInstance<ServerConfig>().ForceDisableMapText)
	    {
	        string chunkText = $"Chunk Offset: {WorldLoop.Instance.seedOffset}";
	        Vector2 textPosition = new Vector2(57.5f, Main.screenHeight - 37.5f);

	        Utils.DrawBorderString(Main.spriteBatch, chunkText, textPosition, Color.Yellow);
	    }
    }

    public void MoveWorlds(int change)
    {
	    WorldLoopPlayer.Instance.subworldTransitionPosition = Main.LocalPlayer.position;
        WorldLoopPlayer.Instance.subworldTransitionVelocity = Main.LocalPlayer.velocity;
        isChunkTransition = true;

	    SubworldSystem.Exit();
	    WorldLoop.Instance.seedOffset += change;
    }

	public override void PreUpdateWorld()
	{
		if (WorldLoop.Instance.seedOffset != 0 && !SubworldSystem.IsActive<CopyWorld>())
		{
			// Only grab the Main World position if entering for the first time
            if (!isChunkTransition)
            {
                WorldLoopPlayer.Instance.subworldTransitionPosition = Main.LocalPlayer.position;
                WorldLoopPlayer.Instance.subworldTransitionVelocity = Main.LocalPlayer.velocity;
            }
            isChunkTransition = false;
			SubworldSystem.Enter<CopyWorld>();
		}
		if (SubworldSystem.Current is ISubworldPost currentCustomSubworld)
        {
            currentCustomSubworld.BasePreUpdate();
        }
		if (SubworldSystem.IsActive<CopyWorld>())
		{
			// Update mechanisms
			Wiring.UpdateMech();

			// Update tile entities
			TileEntity.UpdateStart();
			foreach (TileEntity te in TileEntity.ByID.Values)
			{
				te.Update();
			}
			TileEntity.UpdateEnd();

			// Update liquid entities
			if (++Liquid.skipCount > 1)
			{
				Liquid.UpdateLiquid();
				Liquid.skipCount = 0;
			}
		}
		base.PreUpdateWorld();
	}

	public override void PostUpdateWorld()
    {
        if (SubworldSystem.Current is ISubworldPost currentCustomSubworld)
        {
            currentCustomSubworld.BasePostUpdate();
        }
        base.PostUpdateWorld();
    }
}