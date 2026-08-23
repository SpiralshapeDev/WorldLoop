using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WorldLoopEdge.Common.Configs;

public class ClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [DefaultValue(true)]
    public bool EnableMapText;

    [DefaultValue(false)]
    public bool DisableTip;
}