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
	public int numberOfWatys = 0;
	public float colddown = 99;
	public float intervalDegrees = 0;
	public float initVelocity = 0;
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
	{
		get{ return _launchCount; }
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
