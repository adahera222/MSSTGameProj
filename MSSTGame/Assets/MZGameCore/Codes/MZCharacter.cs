using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacter : MonoBehaviour, IMZMove, IMZRemove
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

	bool _isActive;
	float _lifeTimeCount = 0;
	Dictionary<string, MZCharacterPart> _partsByNameDictionary;
	MZCharacterType _characterType = MZCharacterType.Unknow;
	MZRemove_OutOfBound removeOutOfBound = null;

	#region IMZRemove implementation
	public void DoRemoveOutOfBound()
	{
		Disable();
	}

	public float lifeTimeCount
	{
		get
		{
			return _lifeTimeCount;
		}
	}

	public Vector2 frameSize
	{
		get
		{
			return new Vector2( 10, 10 );
		}
	}
	#endregion


	public MZCharacterPart AddPart(string name)
	{
		MZDebug.Assert( characterType != MZCharacterType.Unknow, "character type is unknow, must assgn it first" );

		GameObject partObject = new GameObject();
		partObject.transform.parent = gameObject.transform;
		partObject.AddComponent<MZCharacterPart>();

		MZCharacterPart characterPart = partObject.GetComponent<MZCharacterPart>();
		characterPart.name = name;
		characterPart.parentGameObject = gameObject;

		if( _partsByNameDictionary == null )
			_partsByNameDictionary = new Dictionary<string, MZCharacterPart>();

		_partsByNameDictionary.Add( name, characterPart );

		return partObject.GetComponent<MZCharacterPart>();
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

	void Start()
	{
		_isActive = true;
		_lifeTimeCount = 0;
		removeOutOfBound = new MZRemove_OutOfBound();
		removeOutOfBound.controlTarget = this;
	}

	void Update()
	{
		_lifeTimeCount += Time.deltaTime;
		removeOutOfBound.Update();
	}
}