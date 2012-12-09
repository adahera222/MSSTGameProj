using UnityEngine;
using System.Collections;

public class MZGameComponents
{
	static MZGameComponents _instance = null;
	MZCharactersManager _charactersManager = null;

	static public MZGameComponents GetInstance()
	{
		if( _instance == null )
			_instance = new MZGameComponents();

		return _instance;
	}

	public MZCharactersManager charactersManager
	{
//		get
//		{
//			if( _charactersManager == null )
//				_charactersManager = GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>();
//
//			return _charactersManager;
//		}


		set{ _charactersManager = value; }
		get{ return _charactersManager; }
	}

	public GameObject spritesPool
	{
		get{ return (GameObject)GameObject.Find( "MZSpritesPool" ); }
	}

	private MZGameComponents()
	{
	}
}
