using Prime;
using System;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace Overlord
{
	public class ProxyCombatant : IUserDataType
	{
		public Combatant Combatant;

		public ProxyCombatant(Combatant c)
		{
			this.Combatant = c;
		}

		public DynValue Index(Script s, DynValue index, bool isDirectIndexing)
		{
			return Combatant.Lua.This.Get(index);
		}

		public bool SetIndex(Script s, DynValue index, DynValue value, bool isDirectIndexing)
		{
			Combatant.Lua.This.Set(index, value);

			return true;
		}

		public DynValue MetaIndex(Script s, string metaname)
		{
			if (Combatant.Lua.This.MetaTable != null)
			{
				return Combatant.Lua.This.MetaTable.Get(metaname);
			}

			return null;
		}
	}
}
