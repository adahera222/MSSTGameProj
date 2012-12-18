using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacter : MonoBehaviour, IMZCollision
{
	public MZCollision outCollision = new MZCollision();
	public new string name = "";

	#region IMZCollision implementation
	public Vector2 realPosition
	{ get { return position; } }
	#endregion

	public bool isActive
	{ get { return _isActive; } }

	public MZCharacterType characterType
	{
		set{ _characterType = value; }
		get{ return _characterType; }
	}

	public Dictionary<string, MZCharacterPart> partsByNameDictionary
	{
		get{ return _partsByNameDictionary; }
	}

	public Vector2 position
	{
		set{ gameObject.transform.position = new Vector3( value.x, value.y, gameObject.transform.position.z );	}
		get{ return new Vector2( gameObject.transform.position.x, gameObject.transform.position.y ); }
	}

	public float depth
	{
		set{ gameObject.transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, value ); }
		get{ return gameObject.transform.position.z; }
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

	public virtual Vector2 currentMovingVector
	{
		get{ return Vector2.zero; }
	}

	bool _isActive;
	float _lifeTimeCount = 0;
	float _enableRemoveTime = 9999.99f;
	Dictionary<string, MZCharacterPart> _partsByNameDictionary;
	MZCharacterType _characterType = MZCharacterType.Unknow;

	public virtual void Enable()
	{
		enabled = true;
		_isActive = true;
		_lifeTimeCount = 0;
		outCollision.collisionDelegate = this;
		outCollision.Set( new Vector2( 0, 0 ), 50 );
	}

	public virtual void Disable()
	{
		enabled = false;
		_isActive = false;
	}

	public MZCharacterPart AddPart(string name)
	{
//		MZDebug.Assert( characterType != MZCharacterType.Unknow, "character type is unknow, must assgn it first" );
//
//		GameObject partObject = MZOTSpritesPoolManager.GetInstance().GetSpriteObject( characterType );
//
//		MZCharacterPart characterPart = partObject.GetComponent<MZCharacterPart>();
//
//		characterPart.Enable();
//		characterPart.name = name;
//		characterPart.parentGameObject = gameObject;
//
//		if( _partsByNameDictionary == null )
//			_partsByNameDictionary = new Dictionary<string, MZCharacterPart>();
//
//		MZDebug.Assert( _partsByNameDictionary.ContainsKey( name ) == false, "Duplicate key=" + name );
//		_partsByNameDictionary.Add( name, characterPart );
//
//		return partObject.GetComponent<MZCharacterPart>();

		return null;
	}

	public virtual void Clear()
	{
		if( partsByNameDictionary == null )
			return;

		foreach( MZCharacterPart characterPart in partsByNameDictionary.Values )
		{
			characterPart.Disable();
//			MZOTSpritesPoolManager.GetInstance().ReturnSpriteObject( characterPart.gameObject, characterType );
		}

		partsByNameDictionary.Clear();
	}

	public virtual bool IsCollide(MZCharacter other)
	{
		if( outCollision.IsCollision( other.outCollision ) == false )
			return false;

		if( isActive == false )
			return false;

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

	protected virtual void Update()
	{
		_lifeTimeCount += Time.deltaTime;
		RemoveWhenOutOfBound();
	}

	private void Start()
	{
		// get self part info ... and setting????
	}

	void OnDrawGizmos()
	{
		if( MZGameSetting.SHOW_COLLISION_RANGE == false || isActive == false )
			return;

		Gizmos.color = new Color( 0.67f, 0.917f, 0.921f );
		Gizmos.DrawWireSphere( position, outCollision.radius );
	}

	void RemoveWhenOutOfBound()
	{
		if( lifeTimeCount <= enableRemoveTime )
			return;

		if( _partsByNameDictionary != null )
		{
			foreach( MZCharacterPart part in _partsByNameDictionary.Values )
			{
				if( part.IsInScreen() == true )
					return;
			}
		}

		Disable();
	}
}