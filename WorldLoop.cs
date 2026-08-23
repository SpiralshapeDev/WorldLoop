using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;
using WorldLoopEdge.Common.Configs;

namespace WorldLoopEdge;

// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
public class WorldLoop : Mod
{
    public static WorldLoop Instance => ModContent.GetInstance<WorldLoop>();
    public string currentWorldID => SubworldSystem.Current?.Name ?? "Main";
    public Vector2 relogicWorldBoundaryPixels = new Vector2(41 * 16f,42 * 16f);
    public int seedOffset = 0;

    public void InitWorld()
    {
        if (!ModContent.GetInstance<ClientConfig>().DisableTip)
        {
            Main.NewText("<{LoopedWorlds}>: Warning! This mod is not made for long-term playthroughs and may break world files. Remember to keep backups.",Color.Yellow);
            Main.NewText("<{LoopedWorlds}>: To disable this message, turn on \"Disable Tip\" in mod's client config settings.",Color.Yellow);
        }
    }
}
