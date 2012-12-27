using UnityEngine;
using System.Collections;

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
}

public abstract class MZAttack : MZControlBase, IMZTargetHelp
{
	static public MZAttack Create(string name, Type type, IMZAttack controlTarget)
	{
		MZDebug.Assert( controlTarget != null, "controlTarget is null" );

		MZAttack attack = (MZAttack)MZObjectHelp.CreateClass( "MZAttack_" + type.ToString() );
		MZDebug.Assert( attack != null, "create fail, type=" + type.ToString() );

		attack.name = name;
		attack.controlDelegate = controlTarget;

		return attack;
	}

	public enum Type
	{
		Idle,
		OddWay,
	}

	public new IMZAttack controlDelegate = null;
	public int numberOfWays = 0;
	public int additionalWaysPerLaunch = 0;
	public int strength = 1;
	public float colddown = 99;
	public float intervalDegrees = 0;
	public float initVelocity = 0;
	public float additionalVelocityPerLaunch = 0;
	public float maxVelocity = float.NaN;
	public string bulletName = null;
	public MZFaceTo.Type bulletFaceToType = MZFaceTo.Type.MovingVector;
	//
	bool _enable = true;
	int _launchCount = 0;
	float _colddownCount = 0;
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
			_targetHelp.controlObject = this;
		}
		get
		{
			if( _targetHelp == null )
			{
				_targetHelp = MZTargetHelp.Create( MZTargetHelp.Type.AssignMovingVector, this );
				( _targetHelp as MZTargetHelp_AssignMovingVector ).movingVector = new Vector2( 0, 1 );
			}

			return _targetHelp;
		}
	}

	public override void Reset()
	{
		base.Reset();

		_launchCount = 0;
		_colddownCount = 0;
	}

	protected override void UpdateWhenActive()
	{
		if( !enable )
			return;

		_colddownCount -= MZTime.deltaTime;

		if( _colddownCount <= 0 )
		{
			LaunchBullet();
			_colddownCount += colddown;
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
			float _currentVelocity = initVelocity + ( ( launchCount - 1 )*additionalVelocityPerLaunch );

			if( float.IsNaN( maxVelocity ) )
				return _currentVelocity;

			if( additionalVelocityPerLaunch < 0 )
				return ( _currentVelocity < maxVelocity )? maxVelocity : _currentVelocity;

			if( additionalVelocityPerLaunch > 0 )
				return ( _currentVelocity > maxVelocity )? maxVelocity : _currentVelocity;

			return _currentVelocity;
		}
	}

	protected GameObject GetNewBulletObject()
	{
		MZDebug.Assert( bulletName != null, "bulletName is null" );

		GameObject bullet = MZCharacterObjectsFactory.instance.Get( GetBulletType(), bulletName );
		MZBullet bulletScript = bullet.GetComponent<MZBullet>();

		bulletScript.strength = strength;
		bulletScript.position = controlDelegate.realPosition;
		bulletScript.faceToType = bulletFaceToType;

		return bullet;
	}

	protected virtual void LaunchBullet()
	{
		_launchCount++;
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
}
