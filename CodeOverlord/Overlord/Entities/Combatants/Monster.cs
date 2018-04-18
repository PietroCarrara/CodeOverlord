using Prime;

namespace Overlord
{
	public class Monster : Combatant
	{
		public static Monster FromScript(string s)
		{
			var lua = new MonsterLua();

			var m = (Monster) lua.Script.DoString(s).Table["base"];
			m.Lua.Content = s;

			return m;
		}

		public Monster()
		{
			base.Lua = new MonsterLua();
		}

		public override void OnDestroy()
		{
			BattleManager.Remove(this);
		}
	}
}
