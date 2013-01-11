using UnityEngine;
using System.Collections;

public class TestFormation_S_XY : MZFormation
{
	//

	float _createTimeCount;
	float _createInterval;
	Vector2 _initPosition;

	//

	public override float nextCreatedTime
	{ get { return 1; } }

	//

	public TestFormation_S_XY() : base()
	{
		duration = 4.0f;
	}

	public override void Reset()
	{
		base.Reset();

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

		MZMove_DegreesTo move = mode.AddMove( "GoTurn", MZMove.Type.DegreesTo ) as MZMove_DegreesTo;
		move.initMovingVector = ( positionType == PositionType.Left )? new Vector2( 1, 1 ) : new Vector2( -1, 1 );
		move.initVelocity = 200;
		move.totalTime = 4;

		move.rotationType = MZMove.RotationType.CCW;
		move.destinationDegrees = ( positionType == PositionType.Left )? 135 : 45;

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
		float xValue = 30;
		float yValue = 100;

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
