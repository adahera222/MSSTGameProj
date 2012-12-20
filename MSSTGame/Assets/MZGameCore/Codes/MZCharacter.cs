using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacter : MonoBehaviour, IMZCollision
{
	public MZCollision outCollision = new MZCollision();
	public new string name = "";
	//
	bool _isActive;
	bool _renderEnable = true;
	bool _hasInitValues = false;
	float _lifeTimeCount = 0;
	float _enableRemoveTime = 9999.99f;
	Dictionary<string, MZCharacterPart> _partsByNameDictionary;
	MZCharacterType _characterType = MZCharacterType.Unknow;
	MZFaceTo.Type _faceToType;

	#region IMZCollision implementation
	public Vector2 realPosition
	{ get { return position; } }
	#endregion

	public bool renderEnable
	{
		set
		{
			_renderEnable = value;

			if( partsByNameDictionary == null )
				return;

			foreach( MZCharacterPart p in partsByNameDictionary.Values )
			{
				p.gameObject.renderer.enabled = value;
			}
		}
		get
		{
			return _renderEnable;
		}
	}

	public bool isActive
	{
		get { return _isActive; }
	}

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

	public MZFaceTo.Type faceToType
	{
		set
		{
			_faceToType = value;
			foreach( MZCharacterPart p in partsByNameDictionary.Values )
				p.SetFaceTo( faceToType );
		}
		get
		{
			return _faceToType;
		}
	}

	public float lifeTimeCount
	{
		get{ return _lifeTimeCount; }
	}

	public virtual Vector2 currentMovingVector
	{
		get{ return Vector2.zero; }
	}

	public virtual void InitValues()
	{
		MZDebug.Assert( _hasInitValues == false, "Don't call me twice, you suck!!!" );

		outCollision.collisionDelegate = this;
		outCollision.Set( new Vector2( 0, 0 ), 50 );

		InitPartsInfoFromChild();
		InitMode();

		_isActive = false;
	}

	public virtual void Enable()
	{
		_isActive = true;
//		renderEnable = true;
		_lifeTimeCount = 0;

		if( partsByNameDictionary != null )
		{
			foreach( MZCharacterPart part in partsByNameDictionary.Values )
				part.Enable();
		}
	}

	public virtual void Disable()
	{
		_isActive = false;

		if( partsByNameDictionary != null )
		{
			foreach( MZCharacterPart part in partsByNameDictionary.Values )
				part.Disable();
		}
	}

	public MZCharacterPart AddPart(string name) // delete
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

	public virtual void OnRemoving()
	{
		if( partsByNameDictionary == null )
			return;

		foreach( MZCharacterPart characterPart in partsByNameDictionary.Values )
		{
			characterPart.Disable(); // wrong this
		}

		renderEnable = false;
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

		if( _isActive )
		{
			UpdateWhenActive();
		}
	}

	protected virtual void InitMode()
	{

	}

	// not conside how to design this function
	protected virtual void CheckActive()
	{
		MZDebug.AssertFalse( "not conside how to design this function" );
	}

	protected virtual void UpdateWhenActive()
	{

	}

	void OnDrawGizmos()
	{
		if( MZGameSetting.SHOW_COLLISION_RANGE == false || isActive == false )
			return;

		Gizmos.color = new Color( 0.67f, 0.917f, 0.921f );
		Gizmos.DrawWireSphere( position, outCollision.radius );
	}

	void InitPartsInfoFromChild()
	{
		if( _partsByNameDictionary != null )
			return;

		_partsByNameDictionary = new Dictionary<string, MZCharacterPart>();

		foreach( OTSprite s in gameObject.transform.GetComponentsInChildren<OTSprite>() )
		{
			MZDebug.Log( "+" + s.name + "+" );
		}

		for( int i = 0; i < gameObject.transform.GetChildCount(); i++ )
		{
			GameObject partObject = gameObject.transform.GetChild( i ).gameObject;
			string partName = GetClearPartName( partObject.name );

			MZDebug.Log( "Now add: " + partName + " at " + i.ToString() );

			MZCharacterPart part = partObject.GetComponent<MZCharacterPart>();
			part.name = partName;
			part.parentGameObject = gameObject;

			MZDebug.Assert( _partsByNameDictionary.ContainsKey( partName ) == false, "Duplicate key=" + partName );
			MZDebug.Assert( _partsByNameDictionary.ContainsValue( part ) == false, "Duplicate part=" + partName );

			_partsByNameDictionary.Add( partName, part );
		}

		renderEnable = _renderEnable;
	}

	string GetClearPartName(string origin)
	{
		return origin.Split( '-' )[ 0 ];
	}

	void RemoveWhenOutOfBound()
	{
		if( lifeTimeCount <= enableRemoveTime )
		{
			return;
		}

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