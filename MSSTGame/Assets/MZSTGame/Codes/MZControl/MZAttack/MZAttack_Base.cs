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

public abstract class MZAttack_Base : MZControlBase, IMZTargetHelp
{
	public new IMZAttack controlTarget = null;
	public int numberOfWays = 0;
	public int additionalWaysPerLaunch = 0;
	public float colddown = 99;
	public float intervalDegrees = 0;
	public float initVelocity = 0;
	public float additionalVelocityPerLaunch = 0;
	public float maxVelocity = float.NaN;
	public string bulletSettingName = null;
	bool _enable = true;
	int _launchCount = 0;
	float _colddownCount = 0;
	MZTargetHelp_Base _targetHelp = null;

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
		get { return controlTarget.realPosition; }
	}

	public MZCharacterType characterType
	{
		get { return controlTarget.characterType; }
	}
	#endregion

	protected MZTargetHelp_Base targetHelp
	{
		get
		{
			if( _targetHelp == null )
				SetTargetHelp( new MZTargetHelp_AssignMovingVector( new Vector2( 0, 1 ) ) );

			return _targetHelp;
		}
	}

	public MZTargetHelp_Base SetTargetHelp(MZTargetHelp_Base targetHelp)
	{
		_targetHelp = targetHelp;
		_targetHelp.controlObject = this;

		return _targetHelp;
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

		_colddownCount -= Time.deltaTime;

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
		MZDebug.Assert( bulletSettingName != null, "bulletSettingName is null" );

		GameObject bullet = MZCharacterFactory.GetInstance().CreateCharacter( GetBulletTypeByControlTargetType(), "Bullet", bulletSettingName );
		bullet.GetComponent<MZCharacter>().position = controlTarget.realPosition;

		return bullet;
	}

	protected virtual void LaunchBullet()
	{
		_launchCount++;
	}

	MZCharacterType GetBulletTypeByControlTargetType()
	{
		switch( controlTarget.characterType )
		{
			case MZCharacterType.EnemyAir:
			case MZCharacterType.EnemyGround:
				return MZCharacterType.EnemyBullet;

			case MZCharacterType.Player:
				return MZCharacterType.PlayerBullet;
		}

		MZDebug.Assert( false, "Bullet not support this type: " + controlTarget.characterType.ToString() );
		return MZCharacterType.Unknow;
	}
}
