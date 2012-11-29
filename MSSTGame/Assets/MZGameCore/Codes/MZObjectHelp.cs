using System;

public class MZObjectHelp
{
	static public object CreateClass(string className)
	{
		return Activator.CreateInstance( Type.GetType( className ) );
	}
}
