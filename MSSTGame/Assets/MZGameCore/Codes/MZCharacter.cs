using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MZCharacterType = MZCharacterFactory.MZCharacterType;

public class MZCharacter : MonoBehaviour, IMZMove
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