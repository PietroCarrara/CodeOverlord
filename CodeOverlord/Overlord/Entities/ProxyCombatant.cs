using Prime;
using System;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System.Linq;

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
			var res = Combatant.Lua.This.Get(index);

			var func = res.Function;
            if (func != null)
            {
				res = DynValue.FromObject(s, caller(func));
				return res;
            }
            
			return res;
		}
        
        // HACK: Receive many args. 10 is the limit
        // Oh god please forgive me, for I have done
        // terrible things
        private Func<object, object, object, object, object, object, object, object, object, object, DynValue> caller(Closure func)
        {
			return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) =>
			{
				Console.WriteLine(arg2);
                
                if (arg10 != null) {
					return func.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                } else if (arg9 != null) {
                    return func.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
                } else if (arg8 != null) {
                    return func.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
                } else if (arg7 != null) {
                    return func.Call(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
                } else if (arg6 != null) {
                    return func.Call(arg1, arg2, arg3, arg4, arg5, arg6);
                } else if (arg5 != null) {
                    return func.Call(arg1, arg2, arg3, arg4, arg5);
                } else if (arg4 != null) {
                    return func.Call(arg1, arg2, arg3, arg4);
                } else if (arg3 != null) {
                    return func.Call(arg1, arg2, arg3);
                } else if (arg2 != null) {
                    return func.Call(arg1, arg2);
                } else if (arg1 != null) {
                    return func.Call(arg1);
                } else {
					return func.Call();
                }
			};
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

		public override string ToString()
		{
			var res = "{ ";
			var pairs = Combatant.Lua.This.Pairs;
            
            if (pairs.Count() > 0)
            {
				res += pairs.ElementAt(0).Key + " = " + pairs.ElementAt(0).Value;
            }
            
            foreach(var field in pairs.Skip(1))
            {
				res += ", " + field.Key + " = " + field.Value;
            }

			return res + " }";
		}
	}
}
