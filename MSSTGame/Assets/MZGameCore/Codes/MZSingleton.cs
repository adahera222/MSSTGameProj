using UnityEngine;
using System.Collections;

public class MZSingleton<T> where T : class, new()
{
	static T _instance = null;

	protected MZSingleton()
	{
	}

	static public T instance
	{
		get{ return GetInstance(); }
	}

	static public T GetInstance()
	{
		if( _instance == null )
			_instance = new T();

		return _instance;
	}

}