using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharactersManager : MonoBehaviour
{
	public GUIText guiCharactersInfo;

	public int Add(MZCharacterType characterType, GameObject characterObject)
	{
		if( charactersListByType == null )
		{
			charactersListByType = new Dictionary<MZCharacterType, List<GameObject>>();
		}

		if( charactersListByType.ContainsKey( characterType ) == false )
		{
			List<GameObject> newCharacterList = new List<GameObject>();
			charactersListByType.Add( characterType, newCharacterList );
		}

		List<GameObject> characterList = charactersListByType[ characterType ];
		characterList.Add( characterObject );

		GameObject charactersContainer = GameObject.Find( GetContainerNameByType( characterType ) );
		MZDebug.Assert( charactersContainer != null, "charactersContainer is null, type=" + characterType.ToString() );
		characterObject.transform.parent = charactersContainer.transform;

		return characterList.Count;
	}

	public GameObject GetPlayer()
	{
		if( charactersListByType.ContainsKey( MZCharacterType.Player ) == false || charactersListByType[ MZCharacterType.Player ].Count == 0 )
			return null;

		return charactersListByType[ MZCharacterType.Player ][ 0 ];
	}

	public Vector2 GetPlayerPosition()
	{
		GameObject player = GetPlayer();
		return ( player != null )? player.GetComponent<MZCharacter>().position : Vector2.zero;
	}

	public string GetContainerNameByType(MZCharacterType type)
	{
		switch( type )
		{
			case MZCharacterType.Player:
				return "MZPlayers";

			case MZCharacterType.PlayerBullet:
				return "MZPlayerBullets";

			case MZCharacterType.EnemyAir:
				return "MZEnemiesAir";

			case MZCharacterType.EnemyBullet:
				return "MZEnemyBullets";

			default:
				MZDebug.Assert( false, "Undefine type: " + type.ToString() );
				return "";
		}
	}

	public List<GameObject> GetList(MZCharacterType type)
	{
		if( charactersListByType.ContainsKey( type ) == false )
			return new List<GameObject>();

		return charactersListByType[ type ];
	}

	public void Remove(MZCharacterType type, GameObject character)
	{
		List<GameObject> list = GetList( type );
		MZDebug.Assert( list.Contains( character ) == true, "character not in list, name=" + character.name + ", type=" + type.ToString() );

		list.Remove( character );
		Destroy( character );
	}

	Dictionary<MZCharacterType, List<GameObject>> charactersListByType;

	void Start()
	{

	}

	void LateUpdate()
	{
		RemoveUnActiveCharacters();
	}

	void RemoveUnActiveCharacters()
	{
		if( charactersListByType == null || charactersListByType.Count == 0 )
			return;

		foreach( List<GameObject> list in charactersListByType.Values )
		{
			for( int i = 0; i < list.Count; i++ )
			{
				GameObject characterObject = list[ i ];
				if( characterObject.GetComponent<MZCharacter>().isActive == false )
				{
					characterObject.GetComponent<MZCharacter>().BeforeDestory();
					list.Remove( characterObject );
					Destroy( characterObject );
					i--;
				}
			}
		}
	}

	void OnGUI()
	{
		if( guiCharactersInfo == null || charactersListByType == null || charactersListByType.Count == 0 )
			return;

		string infoText = "";

		foreach( MZCharacterType type in charactersListByType.Keys )
		{
			List<GameObject> list = charactersListByType[ type ];
			infoText += type.ToString() + ": count=" + list.Count.ToString() + "\n";
		}

		guiCharactersInfo.text = infoText;
	}
}