using Terraria;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace WorldLoopEdge.Content.Subworlds;

public class CopyWorldGen
{
    public class CopyWorldGenPass : GenPass
	{
		public CopyWorldGenPass() : base("Terrain", 1) { }

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
		{
			GenerationProgress cache = WorldGenerator.CurrentGenerationProgress;
	        WorldGen.GenerateWorld(Main.ActiveWorldFileData.Seed + WorldLoop.Instance.seedOffset);
			WorldGenerator.CurrentGenerationProgress = cache;
		}
	}
}