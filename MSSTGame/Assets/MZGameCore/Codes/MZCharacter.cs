using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacter : MonoBehaviour, IMZCollision, IMZBaseBehavoir
{
	public enum MZCharacterType
	{
		Unknow,
		Player,
		PlayerBullet,
		EnemyAir,
		EnemyGround,
		EnemyBullet,
	}

	public new string name = "";
	//
	bool _isActive = false;
	bool _renderEnable = true;
	float _lifeTimeCount = 0;
	float _enableRemoveTime = 9999.99f;
	Dictionary<string, MZCharacterPart> _partsByNameDictionary;
	MZCharacterType _characterType = MZCharacterType.Unknow;

	#region IMZCollision implementation
	public Vector2 realPosition
	{ get { return position; } }
	#endregion

	public bool renderEnable
	{
		set
		{
			_renderEnable = value;

			foreach( Transform t in gameObject.transform )
			{
				if( MZGameSetting.DISABLE_BULLET_EFFECT && t.gameObject.name == "Particle" )
				{
					t.renderer.enabled = false;
					continue;
				}

				t.renderer.enabled = _renderEnable;
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

	public float lifeTimeCount
	{
		get{ return _lifeTimeCount; }
	}

	public virtual float currentMovingDirection
	{
		get{ return 0; }
	}

	//

	public virtual void Clear()
	{
		_isActive = false;
		_renderEnable = false;
		_lifeTimeCount = 0;
		_enableRemoveTime = 9999.99f;
	}

	public virtual void InitCharacterPartsData()
	{
		MZDebug.Assert( _partsByNameDictionary == null, "Don't call me twice, you suck!!!" );

		_partsByNameDictionary = new Dictionary<string, MZCharacterPart>();

		foreach( Transform childTransform in gameObject.transform )
		{
			GameObject partObject = childTransform.gameObject;
			string partName = GetClearPartName( partObject.name );

			MZCharacterPart part = partObject.GetComponent<MZCharacterPart>();

			if( part == null )
				continue;

			part.name = partName;
			part.parentGameObject = gameObject;

			MZDebug.Assert( _partsByNameDictionary.ContainsKey( partName ) == false, "Duplicate key=" + partName );
			MZDebug.Assert( _partsByNameDictionary.ContainsValue( part ) == false, "Duplicate part=" + partName );

			_partsByNameDictionary.Add( partName, part );
		}

		renderEnable = _renderEnable;
	}

	public virtual void Enable()
	{
		_isActive = true;
		_lifeTimeCount = 0;

		if( partsByNameDictionary != null )
		{
			foreach( MZCharacterPart part in partsByNameDictionary.Values )
				part.Enable();
		}

		InitValues();
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

	public virtual void OnRemoving()
	{
		if( partsByNameDictionary == null )
			return;

		foreach( MZCharacterPart characterPart in partsByNameDictionary.Values )
		{
			characterPart.Disable(); // wrong this
		}
	}

	public virtual bool IsCollide(MZCharacter other)
	{
		if( isActive == false || other.isActive == false )
			return false;

		foreach( MZCharacterPart selfPart in _partsByNameDictionary.Values )
		{
			foreach( MZCharacterPart otherPart in other._partsByNameDictionary.Values )
			{
				if( selfPart.IsCollide( otherPart ) )
				{
					return true;
				}
			}
		}

		return false;
	}

	public virtual void InitDefaultMode()
	{
		MZDebug.Assert( partsByNameDictionary != null, "must set partsByNameDictionary first" );
	}

	//

	protected virtual void InitValues()
	{

	}

	protected virtual void Update()
	{
		if( _lifeTimeCount == 0 )
			FirstUpdate();

		_lifeTimeCount += MZTime.deltaTime;

		RemoveWhenOutOfBound();

		if( _isActive )
		{
			UpdateWhenActive();
		}
	}

	// not conside how to design this function
	protected virtual void CheckActive()
	{
		MZDebug.AssertFalse( "not conside how to design this function" );
	}

	protected virtual void FirstUpdate()
	{

	}

	protected virtual void UpdateWhenActive()
	{

	}

	void OnDrawGizmos()
	{
		if( MZGameSetting.SHOW_GIZMOS == false || MZGameSetting.SHOW_COLLISION_RANGE == false || isActive == false )
			return;

		Gizmos.color = new Color( 0.67f, 0.917f, 0.921f );
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