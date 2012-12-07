using UnityEngine;
using System.Collections;

public class MZGameComponents
{
	static MZGameComponents _instance = null;

	static public MZGameComponents GetInstance()
	{
		if( _instance == null )
			_instance = new MZGameComponents();

		return _instance;
	}

	public MZCharactersManager charactersManager
	{
		get{ return GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>(); }
	}

	public GameObject spritesPool
	{
		get{ return (GameObject)GameObject.Find( "MZSpritesPool" ); }
	}

	private MZGameComponents()
	{
	}
}
