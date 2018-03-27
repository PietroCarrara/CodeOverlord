using Prime;

namespace Overlord
{
	public class Monster : Combatant
	{
		public static Monster FromScript(string s)
		{
			var lua = new Lua();

			var m = (Monster) lua.DoString(s).Table["base"];

			if (m is Slime)
				return new Slime();

			return null;
		}

		public override void OnDestroy()
		{
			BattleManager.Remove(this);
		}
	}
}
