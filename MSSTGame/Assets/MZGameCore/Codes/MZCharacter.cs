using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacter : MonoBehaviour
{
	public int poolIndex = -1;

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

	public float enableRemoveTime
	{
		set{ _enableRemoveTime = value; }
		get{ return _enableRemoveTime; }
	}

	public float lifeTimeCount
	{
		get{ return _lifeTimeCount; }
	}

	bool _isActive;
	float _lifeTimeCount = 0;
	float _enableRemoveTime = 9999.99f;
	Dictionary<string, MZCharacterPart> _partsByNameDictionary;
	MZCharacterType _characterType = MZCharacterType.Unknow;

	public virtual void Enable()
	{
		_isActive = true;
		_lifeTimeCount = 0;
	}

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

		MZDebug.Assert( _partsByNameDictionary.ContainsKey( name ) == false, "Duplicate key=" + name );
		_partsByNameDictionary.Add( name, characterPart );

		return partObject.GetComponent<MZCharacterPart>();
	}

	public void Disable()
	{
		_isActive = false;
	}

	public virtual void Clear()
	{
		foreach( MZCharacterPart characterPart in partsByNameDictionary.Values )
		{
			characterPart.enabled = false;
			MZOTSpritesPoolManager.GetInstance().ReturnSpriteObject( characterPart.gameObject, characterType );
		}

		partsByNameDictionary.Clear();
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

	// set private to start function ... and use custom ReStart() (ref: rest ... ) call by my CharacterObjectManager ...
//	protected virtual void Start()
//	{
//		_isActive = true;
//		_lifeTimeCount = 0;
//		Reset(); // ya .. i am re-start() ...
//	}


	protected virtual void Update()
	{
		_lifeTimeCount += Time.deltaTime;
		RemoveWhenOutOfBound();
	}

	private void Start()
	{
		
	}

	void RemoveWhenOutOfBound()
	{
		if( lifeTimeCount <= enableRemoveTime )
			return;

		foreach( MZCharacterPart part in _partsByNameDictionary.Values )
		{
			if( part.IsInScreen() == true )
				return;
		}

		Disable();
	}
}