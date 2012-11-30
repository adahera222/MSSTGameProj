using System;

public class MZObjectHelp
{
	static public object CreateClass(string className)
	{
		object newObject = Activator.CreateInstance( Type.GetType( className ) );
		MZDebug.Assert( newObject != null, "Create new class fail, name=" + className );

		return newObject;
	}
}
