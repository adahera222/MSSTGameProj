using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MZCharacterType = MZCharacterFactory.MZCharacterType;

public class MZCharacter : MonoBehaviour
{
	public bool isActive
	{ get{ return _isActive; } }

	public MZCharacterType characterType
	{
		set{ _characterType = value; }
		get{ return _characterType; }
	}

	public MZCharacterPart[] Parts
	{ get { return _partsList.ToArray(); } }

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

		if( _partsList == null )
			_partsList = new List<MZCharacterPart>();

		_partsList.Add( partBehaviour );

		return _partsList.Count;
	}

	public void Disable()
	{
		_isActive = false;
	}

	public bool IsCollide(MZCharacter other)
	{
		foreach( MZCharacterPart selfPart in _partsList )
		{
			foreach( MZCharacterPart otherPart in other._partsList )
			{
				if( selfPart.IsCollide( otherPart ) )
					return true;
			}
		}

		return false;
	}

	bool _isActive;
	List<MZCharacterPart> _partsList;
	MZCharacterType _characterType = MZCharacterType.Unknow;

	void Start()
	{
		_isActive = true;
	}

	void Update()
	{

	}
}