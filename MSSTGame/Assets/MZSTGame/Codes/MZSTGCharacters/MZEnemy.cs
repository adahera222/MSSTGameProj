using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZEnemy : MonoBehaviour, IMZMode, IMZAttack // test
{
	public int healthPoint = 1;

	#region IMZMode implementation

	public IMZMove moveControlTarget
	{ get { return gameObject.GetComponentInChildren<MZCharacter>(); } }

	#endregion

	// test
	#region IMZAttack implementation
	public Vector2 position
	{
		get
		{
			return gameObject.GetComponent<MZCharacter>().position;
		}
	}
	#endregion



	public List<MZMode> modesList
	{
		get
		{
			if( _modeControlUpdate == null )
				_modeControlUpdate = new MZControlUpdate<MZMode>();

			return _modeControlUpdate.controlsList;
		}
	}

	public MZMode AddMode(string name)
	{
		MZMode mode = new MZMode();
		mode.name = name;
		mode.controlTarget = this;

		modesList.Add( mode );

		return mode;
	}

	MZControlUpdate<MZMode> _modeControlUpdate = new MZControlUpdate<MZMode>();
	MZAttack_Base attactTest;

	void Start()
	{
		attactTest = new MZAttack_OddWay();
		attactTest.controlTarget = this;
		attactTest.numberOfWatys = 12;
		attactTest.colddown = 5.0f;
		attactTest.intervalDegrees = 30;
	}

	void Update()
	{
		_modeControlUpdate.Update();
		attactTest.Update();
		UpdateCollision();
	}

	void UpdateCollision()
	{
		foreach( GameObject pb in GameObject.Find("MZCharactersManager").GetComponent<MZCharactersManager>().GetList( MZCharacterType.PlayerBullet ) )
		{
			if( pb.GetComponent<MZCharacter>().IsCollide( gameObject.GetComponent<MZCharacter>() ) )
			{
				healthPoint -= 1;
				pb.GetComponent<MZCharacter>().Disable();
			}
		}

		if( healthPoint <= 0 )
			gameObject.GetComponent<MZCharacter>().Disable();

	}

//	float cd = 0;
//	float interval = 3.0f;
//
//	void UpdateAttack()
//	{
//		cd -= Time.deltaTime;
//		if( cd <= 0 )
//		{
//			GameObject eb = MZCharacterFactory.GetInstance().CreateCharacter( MZCharacterType.EnemyBullet, "EnemyBullet" );
//			eb.GetComponent<MZCharacter>().position = gameObject.GetComponent<MZCharacter>().position;
//			eb.GetComponent<MZEnemyBullet>().movingVector = new Vector2( 0, -1 );
//
//			cd += interval;
//		}
//	}
}