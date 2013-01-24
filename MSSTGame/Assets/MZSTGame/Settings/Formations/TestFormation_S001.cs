using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class TestFormation_S001 : MZFormation
{
	//

	public override float disableNextFormationTime
	{ get { return 1; } }

	protected override int maxEnemyCreatedNumber
	{ get { return 5; } }

	//

	protected override void InitValues()
	{
		enemyName = "EnemyS001";
		enemyCreateTimeInterval = 0.6f;
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
	}

	protected override void UpdateWhenActive()
	{
		if( UpdateAndCheckTimeToCreateEnemy() )
		{
			AddNewEnemy( false );
		}
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.healthPoint = 4;

		MZMode mode = enemy.AddMode( "mode" );

		MZMove_ToTarget move = mode.AddMove<MZMove_ToTarget>( "GoDie" );
		move.velocity = 200;
		move.target.calcuteEveryTime = true;

		MZPartControl partControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( partControl );

		MZAttack_OddWay attack = partControl.AddAttack<MZAttack_OddWay>();
		attack.numberOfWays = 3;
		attack.colddown = 10;
		attack.duration = -1;
		attack.intervalDegrees = 15;
		attack.initVelocity = 500;
		attack.bulletName = "EBDonuts";
		attack.targetHelp = new MZTargetHelp_Target();
		attack.isRunOnce = true;
	}

	protected override Vector2 GetEnemyStartPosition()
	{
		float xValue = 400;
		float yValue = 500;

		switch( positionType )
		{
			case PositionType.Left:
				return new Vector3( -xValue, yValue );
			case PositionType.Right:
				return new Vector3( xValue, yValue );
			case PositionType.Mid:
			default:
				return new Vector3( 0, yValue );
		}
	}
}