using UnityEngine;
using System.Collections;

public class MZGameComponents : MZSingleton<MZGameComponents>
{
	MZCharactersManager _charactersManager = null;

	public MZCharactersManager charactersManager
	{
		set{ _charactersManager = value; }
		get{ return _charactersManager; }
	}
}
