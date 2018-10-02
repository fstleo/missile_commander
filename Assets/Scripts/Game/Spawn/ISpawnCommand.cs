using UnityEngine;

namespace Game.Spawn
{
	public interface ISpawnCommand
	{
		void Spawn(Vector3 from, Vector3 to, int level);
	}
}
