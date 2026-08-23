using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WorldLoopEdge.Common.Configs;

public class ServerConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [DefaultValue(false)]
    public bool ForceDisableMapText;

    [DefaultValue(true)]
    public bool AllowRespawn;

    [DefaultValue(true)]
    public bool AllowReturn;
}