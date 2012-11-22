using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class MZDebug
{
	static public void Log(string message)
	{
		if( Application.platform != RuntimePlatform.OSXEditor )
			return;

		StackTrace stackTrace = new System.Diagnostics.StackTrace( 1 );
		StackFrame stackFrame = stackTrace.GetFrame( 0 );

		string fullMsg = stackFrame.GetMethod().DeclaringType.FullName + "." + stackFrame.GetMethod().Name + "(): " + message;

		UnityEngine.Debug.Log( fullMsg );
	}
		
	static public bool Alert(bool condition, string message)
	{
		if( condition == true )
		{
			StackTrace stackTrace = new System.Diagnostics.StackTrace( 1 );
			StackFrame stackFrame = stackTrace.GetFrame( 0 );

			string fullMsg = stackFrame.GetMethod().DeclaringType.FullName + "." + stackFrame.GetMethod().Name + "(): " + message;

			UnityEngine.Debug.Log( fullMsg );
		}

		return condition;
	}

	static public void Assert(bool condition, string message)
	{
		if( condition == false )
		{
			StackTrace stackTrace = new System.Diagnostics.StackTrace( 1 );
			StackFrame stackFrame = stackTrace.GetFrame( 0 );

			string fullMsg = stackFrame.GetMethod().DeclaringType.FullName + "." + stackFrame.GetMethod().Name + "(): " + message;

			UnityEngine.Debug.LogError( fullMsg );
		}
	}
}
