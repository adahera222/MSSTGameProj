using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharactersManager : MonoBehaviour
{
	public GUIText guiCharactersInfo;

	public int Add(MZCharacterType characterType, GameObject character)
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
		characterList.Add( character );

		return characterList.Count;
	}

	public GameObject GetPlayer()
	{
		return charactersListByType[ MZCharacterType.Player ][ 0 ];
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

	void Update()
	{
		RemoveUnActiveCharacters();
	}

	void RemoveUnActiveCharacters()
	{
		foreach( List<GameObject> list in charactersListByType.Values )
		{
			for( int i = 0; i < list.Count; i++ )
			{
				GameObject character = list[ i ];
				if( character.GetComponent<MZCharacter>().isActive == false )
				{
					list.Remove( character );
					Destroy( character );
					i--;
				}
			}
		}
	}

	void OnGUI()
	{
		if( guiCharactersInfo == null )
			return;

//		string infoText = "";
//
//		foreach( MZCharacterType type in charactersListByType.Keys )
//		{
//			List<GameObject> list = charactersListByType[ type ];
//			infoText += type.ToString() + ": count=" + list.Count.ToString() + "\n";
//		}
//
//		guiCharactersInfo.text = infoText;
	}
}