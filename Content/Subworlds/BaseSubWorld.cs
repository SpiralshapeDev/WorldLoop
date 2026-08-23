using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;

namespace WorldLoopEdge.Content.Subworlds;

public abstract class BaseSubWorld<T> : Subworld, ISubworldPost where T : BaseSubWorld<T>
{
    public static T Instance => ModContent.GetInstance<T>();
    public virtual bool hideMapRespawn => false;
    public virtual bool allowRespawn => false;

    public override void OnExit()
	{
		if (hideMapRespawn)
		{
			Main.spawnTileX = Main.maxTilesX/2;
			Main.spawnTileY = Main.maxTilesY/2;
		}
		base.OnExit();
	}

    public void BasePostEnter()
    {
	    if (hideMapRespawn)
	    {
			Main.spawnTileX = (int)Main.LocalPlayer.position.X;
			Main.spawnTileY = Main.maxTilesY;
	    }
	    PostEnter();
    }
    public virtual void PostEnter() { }

    public void BasePostExit()
    {
	    PostExit();
    }
    public virtual void PostExit() { }
    public void BasePreUpdate()
    {
	    PreUpdate();
    }
    public virtual void PreUpdate() { }
    public void BasePostUpdate()
    {
	    if (!allowRespawn && WorldLoopPlayer.Instance.hasSpawnpoint)
		{
			Main.LocalPlayer.RemoveSpawn();
			Main.NewText("Can't set spawn point here!", Color.Red);
		}
	    PostUpdate();
    }
    public virtual void PostUpdate() { }
    public void BasePlayerDeath()
    {
	    if (!allowRespawn || !WorldLoopPlayer.Instance.hasSpawnpoint)
	    {
		    WorldLoop.Instance.seedOffset = 0;
			SubworldSystem.Exit();
	    }
	    OnPlayerDeath();
    }
    public virtual void OnPlayerDeath() { }
}