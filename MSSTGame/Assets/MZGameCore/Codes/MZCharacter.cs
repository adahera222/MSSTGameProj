using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MZCharacterType = MZCharacterFactory.MZCharacterType;

public class MZCharacter : MonoBehaviour
{
	public bool isActive
	{ get { return _isActive; } }

	public MZCharacterType characterType
	{
		set{ _characterType = value; }
		get{ return _characterType; }
	}

	public Dictionary<string, MZCharacterPart> partsByNameDictionary
	{ get { return _partsByNameDictionary; } }

	public Vector2 position
	{
		set{ gameObject.transform.position = new Vector3( value.x, value.y, gameObject.transform.position.z ); }
		get{ return new Vector2( gameObject.transform.position.x, gameObject.transform.position.y ); }
	}

	public void Init(MZCharacterSetting setting)
	{
		foreach( MZCharacterPartSetting partSetting in setting.partSettings )
		{
			AddPart( partSetting );
		}
	}

	public int AddPart(MZCharacterPartSetting setting)
	{
		MZDebug.Assert( characterType != MZCharacterType.Unknow, "character type is unknow, must assgn it first" );

		GameObject part = MZResources.InstantiateMZGameCoreObject( "MZCharacterPart" );
		part.transform.parent = gameObject.transform;

		MZCharacterPart partBehaviour = part.GetComponent<MZCharacterPart>();
		partBehaviour.Init( setting, gameObject );

		if( _partsByNameDictionary == null )
			_partsByNameDictionary = new Dictionary<string, MZCharacterPart>();

		_partsByNameDictionary.Add( setting.name, partBehaviour );

		return _partsByNameDictionary.Count;
	}

	public int AddPart(string name, MZCharacterPart part)
	{
		MZDebug.Assert( characterType != MZCharacterType.Unknow, "character type is unknow, must assgn it first" );
		part.parentGameObject = gameObject;

		if( _partsByNameDictionary == null )
			_partsByNameDictionary = new Dictionary<string, MZCharacterPart>();

		_partsByNameDictionary.Add( name, part );

		return _partsByNameDictionary.Count;

	}

	public void Disable()
	{
		_isActive = false;
	}

	public bool IsCollide(MZCharacter other)
	{
		foreach( MZCharacterPart selfPart in _partsByNameDictionary.Values )
		{
			foreach( MZCharacterPart otherPart in other._partsByNameDictionary.Values )
			{
				if( selfPart.IsCollide( otherPart ) )
					return true;
			}
		}

		return false;
	}

	bool _isActive;
	Dictionary<string, MZCharacterPart> _partsByNameDictionary;
	MZCharacterType _characterType = MZCharacterType.Unknow;

	void Start()
	{
		_isActive = true;
	}

	void Update()
	{

	}
}