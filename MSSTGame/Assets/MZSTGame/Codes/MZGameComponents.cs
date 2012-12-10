using UnityEngine;
using System.Collections;

public class MZGameComponents
{
	static MZGameComponents _instance = null;
	MZCharactersManager _charactersManager = null;
	GameObject spritesPoolObject = null;

	static public MZGameComponents GetInstance()
	{
		if( _instance == null )
			_instance = new MZGameComponents();

		return _instance;
	}

	public MZCharactersManager charactersManager
	{
		set{ _charactersManager = value; }
		get{ return _charactersManager; }
	}

	public GameObject spritesPool
	{
		get
		{
			if( spritesPoolObject == null )
				spritesPoolObject = (GameObject)GameObject.Find( "MZSpritesPool" );

			return spritesPoolObject;
		}
	}

	private MZGameComponents()
	{
	}
}
