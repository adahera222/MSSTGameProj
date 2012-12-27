using UnityEngine;
using System.Collections;

public class MZTime : MZSingleton<MZTime>
{
	static float _deltaTime;
	//

	static public float deltaTime
	{
		get{ return _deltaTime; }
	}
	//

	public void Reset()
	{
		_deltaTime = 0;
	}

	public void Update()
	{
		_deltaTime = Time.deltaTime;
	}
}
