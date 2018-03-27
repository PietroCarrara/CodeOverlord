using Prime;

namespace Overlord
{
	public class Hero : Combatant
	{
		public override void OnDestroy()
		{
			BattleManager.Remove(this);
		}
	}
}
