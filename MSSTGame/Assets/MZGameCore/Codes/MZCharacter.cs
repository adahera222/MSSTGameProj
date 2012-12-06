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
	float _enableRemoveTime = 9999.99f;
	Dictionary<string, MZCharacterPart> _partsByNameDictionary;
	MZCharacterType _characterType = MZCharacterType.Unknow;
	MZRemove_OutOfBound removeOutOfBound = null;

	#region IMZRemove implementation
	public void DoRemoveOutOfBound()
	{
		Disable();
	}

	public float enableRemoveTime
	{
		set{ _enableRemoveTime = value; }
		get { return _enableRemoveTime; }
	}

	public float lifeTimeCount
	{
		get
		{
			return _lifeTimeCount;
		}
	}

	// framesize for remove, not good now ... maybe need all part to his remove rect ... and remove test class ...
	public Vector2 frameSize { get { return new Vector2( 10, 10 ); } }
	#endregion


	public MZCharacterPart AddPart(string name)
	{
		MZDebug.Assert( characterType != MZCharacterType.Unknow, "character type is unknow, must assgn it first" );

		GameObject partObject = MZOTSpritesPoolManager.GetInstance().GetSpriteObject( characterType );

		MZCharacterPart characterPart = null;
		if( partObject.GetComponent<MZCharacterPart>() == null )
			characterPart = partObject.AddComponent<MZCharacterPart>();
		else
			characterPart = partObject.GetComponent<MZCharacterPart>();

		characterPart.enabled = true;
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

	public void BeforeDestory()
	{
		foreach( MZCharacterPart characterPart in partsByNameDictionary.Values )
		{
			characterPart.enabled = false;
			MZOTSpritesPoolManager.GetInstance().ReturnSpriteObject( characterPart.gameObject, characterType );
		}
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