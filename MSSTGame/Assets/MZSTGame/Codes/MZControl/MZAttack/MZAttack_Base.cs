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

public abstract class MZAttack_Base : MZControlBase
{
	public new IMZAttack controlTarget = null;
	public int numberOfWays = 0;
	public int additionalWaysPerLaunch = 0;
	public float colddown = 99;
	public float intervalDegrees = 0;
	public float initVelocity = 0;
	public float additionalVelocityPerLaunch = 0;
	public float maxVelocity = float.NaN;
	int _launchCount = 0;
	float _colddownCount = 0;

	public override void Reset()
	{
		base.Reset();

		_launchCount = 0;
		_colddownCount = 0;
	}

	protected override void UpdateWhenActive()
	{
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
		GameObject bullet = MZCharacterFactory.GetInstance().CreateCharacter( GetBulletTypeByControlTargetType(), "Bullet", "EnemyBullet001Setting" );
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
