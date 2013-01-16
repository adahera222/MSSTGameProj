using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class TestFormation_S001 : MZFormation
{
	//

	float _createTimeCount;
	float _createInterval;
	Vector2 _initPosition;

	//

	public override float disableNextFormationTime
	{ get { return 1; } }

	protected override int maxCreatedNumber
	{ get { return 5; } }

	//

	public override void Enable()
	{
		base.Enable();

		_createTimeCount = 0;
		_createInterval = 0.6f;
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		_initPosition = GetInitPosition( positionType );
	}

	protected override void UpdateWhenActive()
	{
		_createTimeCount -= MZTime.deltaTime;

		if( _createTimeCount < 0 )
		{
			AddNewEnemy( MZCharacterType.EnemyAir, "EnemyS001", false );
			_createTimeCount += _createInterval;
		}
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.healthPoint = 4;
		enemy.position = _initPosition;

		MZMode mode = enemy.AddMode( "mode" );

		MZMove move = mode.AddMove( "GoDie", MZMove.Type.ToTarget );
		move.velocity = 200;

		MZPartControl partControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( partControl );

		MZAttack attack = partControl.AddAttack( MZAttack.Type.OddWay );
		attack.numberOfWays = 3;
		attack.colddown = 10;
		attack.duration = -1;
		attack.intervalDegrees = 15;
		attack.initVelocity = 500;
		attack.bulletName = "EBDonuts";
		attack.targetHelp = new MZTargetHelp_Target();
		attack.isRunOnce = true;
	}

	//

	Vector2 GetInitPosition(PositionType sideType)
	{
		float xValue = 400;
		float yValue = 500;

		switch( sideType )
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