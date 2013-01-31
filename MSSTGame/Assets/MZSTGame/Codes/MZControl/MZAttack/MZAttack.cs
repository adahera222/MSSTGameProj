using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MZCharacterType = MZCharacter.MZCharacterType;

public interface IMZAttack : IMZControl
{
	MZCharacterType characterType
	{
		get;
	}

	Vector2 realPosition
	{
		get;
	}

	float rotation
	{
		get;
	}
}

public abstract class MZAttack : MZControlBase, IMZTargetHelp
{
	static public A Create<A>(string name, IMZAttack controlDelegate) where A : MZAttack, new()
	{
		MZDebug.Assert( controlDelegate != null, "controlTarget is null" );

		A attack = new A();
		MZDebug.Assert( attack != null, "create fail" );

		attack.name = name;
		attack.controlDelegate = controlDelegate;

		return attack;
	}

	public enum Type
	{
		Idle,
		OddWay,
		EvenWay,
		Vortex,
	}

	//
	public bool disableRankEffect = false;
	public bool enableLaunchOffsetWithRotation = false;
	public List<Vector2> offsetPositionsList = new List<Vector2>();
	public new IMZAttack controlDelegate = null;
	public int numberOfWays = 0;
	public int additionalWaysPerLaunch = 0;
	public int strength = 1;
	public float colddown = 99;
	public float intervalDegrees = 0;
	public float initVelocity = 0;
	public float additionalVelocity = 0;
	public float maxVelocity = float.NaN;
	public string bulletName = null;

	//

	bool _enable = true;
	int _launchCount = 0;
	float _colddownCount = 0;
	float _currentAdditionalVelocity = 0;
	MZTargetHelp _targetHelp = null;

	public bool enable
	{
		set
		{
			_enable = value;

			if( _enable == false )
				_colddownCount = 0;
		}
		get{ return _enable; }
	}

	#region IMZTargetHelp implementation
	public Vector2 selfPosition
	{
		get { return controlDelegate.realPosition; }
	}

	public MZCharacterType characterType
	{
		get { return controlDelegate.characterType; }
	}
	#endregion

	public MZTargetHelp targetHelp
	{
		set
		{
			_targetHelp = value;
			_targetHelp.controlDelegate = this;
		}
		get
		{
			if( _targetHelp == null )
			{
				_targetHelp = MZTargetHelp.Create<MZTargetHelp_AssignDirection>( this );
				( _targetHelp as MZTargetHelp_AssignDirection ).direction = 90;
			}

			return _targetHelp;
		}
	}

	public override void Enable()
	{
		base.Enable();

		_launchCount = 0;
		_colddownCount = 0;
		_currentAdditionalVelocity = 0;

		if( targetHelp != null )
			targetHelp.Reset();
	}

	//

	protected override void UpdateWhenActive()
	{
		if( !enable )
			return;

		_colddownCount -= MZTime.deltaTime;

		if( _colddownCount <= 0 )
		{
			targetHelp.BeginOneTime();

			LaunchBullet();
			_colddownCount += colddown;

			targetHelp.EndOneTime();
		}
	}

	protected int launchCount
	{ get { return _launchCount; } }

	protected int currentWays
	{
		get
		{
			int _currentWay = numberOfWays + ( ( launchCount - 1 )*additionalWaysPerLaunch );
			return ( _currentWay < 0 )? 0 : _currentWay;
		}
	}

	protected float currentVelocity
	{
		get
		{
			float _currentVelocity = initVelocity + currentAdditionalVelocity;

			if( float.IsNaN( maxVelocity ) )
				return _currentVelocity;

			if( additionalVelocity < 0 )
				return ( _currentVelocity < maxVelocity )? maxVelocity : _currentVelocity;

			if( additionalVelocity > 0 )
				return ( _currentVelocity > maxVelocity )? maxVelocity : _currentVelocity;

			return _currentVelocity;
		}
	}

	protected float currentAdditionalVelocity
	{
		set{ _currentAdditionalVelocity = value; }
		get{ return _currentAdditionalVelocity; }
	}

	#region methods that subClass need to assign call timing

	protected void UpdateAdditionalVelocity()
	{
		if( additionalVelocity == 0 )
			return;

		_currentAdditionalVelocity += additionalVelocity;
	}

	#endregion

	protected MZBullet GetNewBullet(int index)
	{
		GameObject bullet = GetNewBulletObject();
		MZBullet bulletScript = bullet.GetComponent<MZBullet>();
		bulletScript.position = GetBulletLaunchPosition( index );

		return bulletScript;
	}

	protected virtual void LaunchBullet()
	{
		_launchCount++;
	}

	protected void AddLinearMoveToBullet(GameObject bulletObject)
	{
		AddLinearMoveToBullet( bulletObject.GetComponent<MZBullet>() );
	}

	protected void AddLinearMoveToBullet(MZBullet bullet)
	{
		MZMove_LinearBy bulletMove = bullet.AddMove<MZMove_LinearBy>( "LinearBy" );
		bulletMove.velocity = currentVelocity*GetBulletVelocityMultiply();
	}

	protected void EnableBullet(MZBullet bullet)
	{
		MZGameComponents.instance.charactersManager.Add( GetBulletType(), bullet );
	}

	//

	GameObject GetNewBulletObject()
	{
		MZDebug.Assert( bulletName != null, "bulletName is null" );

		GameObject bullet = MZCharacterObjectsFactory.instance.Get( GetBulletType(), bulletName );
		MZBullet bulletScript = bullet.GetComponent<MZBullet>();

		bulletScript.strength = strength;
		bulletScript.partsByNameDictionary[ "MainBody" ].faceTo = new MZFaceTo_MovingDirection();

		return bullet;
	}

	MZCharacterType GetBulletType()
	{
		switch( controlDelegate.characterType )
		{
			case MZCharacterType.EnemyAir:
			case MZCharacterType.EnemyGround:
				return MZCharacterType.EnemyBullet;

			case MZCharacterType.Player:
				return MZCharacterType.PlayerBullet;
		}

		MZDebug.Assert( false, "Bullet not support this type: " + controlDelegate.characterType.ToString() );
		return MZCharacterType.Unknow;
	}

	float GetBulletVelocityMultiply()
	{
		float min = 0.6f;
		float interval = ( 1.0f - min )/4.0f;
		return ( disableRankEffect == false )? min + interval*( MZGameComponents.instance.enemyRank - 1 ) : 1;
	}

	Vector2 GetBulletLaunchPosition(int index)
	{
		if( offsetPositionsList == null || offsetPositionsList.Count == 0 )
			return controlDelegate.realPosition;

		int _index = index%offsetPositionsList.Count;
		Vector2 offset = offsetPositionsList[ _index ];

		if( enableLaunchOffsetWithRotation )
		{
			float radians = MZMath.DegreesToRadians( controlDelegate.rotation );
			float cosValue = Mathf.Cos( radians );
			float sinValue = Mathf.Sin( radians );

			float x = offset.x*cosValue - offset.y*sinValue;
			float y = offset.x*sinValue + offset.y*cosValue;

			offset = new Vector2( x, y );
		}

		return controlDelegate.realPosition + offset;
	}
}
