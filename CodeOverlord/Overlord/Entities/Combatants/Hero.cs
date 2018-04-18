using Prime;

namespace Overlord
{
	public class Hero : Combatant
	{
		public Hero()
		{
			base.Lua = new HeroLua();
		}

		public override void OnDestroy()
		{
			BattleManager.Remove(this);
		}

		public static Hero FromScript(string s)
		{
			var lua = new HeroLua();

			var m = (Hero) lua.Script.DoString(s).Table["base"];

			m.Lua.Content = s;

			return m;
		}
	}
}
