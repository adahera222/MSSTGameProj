using UnityEngine;
using System.Collections;

public interface IMZAttack : IMZControl
{
	Vector2 position
	{
//		set;
		get;
	}
}

public abstract class MZAttack_Base : MZControlBase
{
	public new IMZAttack controlTarget = null;
	public int numberOfWatys = 0;
	public float colddown = 99;
	public float intervalDegrees = 0;
	int _launchCount = 0;
	float _colddownCount = 0;

	public override void Reset()
	{
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
		GameObject bullet = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.EnemyBullet, "Bullet", "EnemyBullet001Setting" );
		bullet.GetComponent<MZCharacter>().position = controlTarget.position;

		return bullet;
	}

	protected virtual void LaunchBullet()
	{
		_launchCount++;
	}
}
