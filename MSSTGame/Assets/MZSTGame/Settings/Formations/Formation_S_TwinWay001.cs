using UnityEngine;
using System.Collections;

public class Formation_S_TwinWay : MZFormation
{
	public override float disableNextFormationTime
	{
		get
		{
			return 4.5f;
		}
	}

	protected override int maxEnemyCreatedNumber
	{
		get
		{
			if( rank < 4 )
				return 4;
			if( rank < 7 )
				return 8;
			if( rank < 10 )
				return 10;
			return 12;
		}
	}

	int _attackCode;
	int _dirCode;
	int _numberOfRow = 4;
	float _xInterval;
	Vector2 _leftStartPosition;
	Vector2 _rightStartPosition;

	protected override void InitValues()
	{
		enemyName = "EnemySRed";
		enemyCreateTimeInterval = 0.7f;

		_dirCode = ( MZMath.RandomFromRange( 0, 1 ) == 0 )? 1 : -1;
		_attackCode = MZMath.RandomFromRange( 0, 1 );
		_xInterval = ( MZGameSetting.ENEMY_BOUNDLE_RIGHT - MZGameSetting.ENEMY_BOUNDLE_LEFT )/_numberOfRow;
		_leftStartPosition = new Vector2( MZGameSetting.ENEMY_BOUNDLE_LEFT + _xInterval/2, MZGameSetting.ENEMY_BOUNDLE_TOP + 100 );
		_rightStartPosition = new Vector2( MZGameSetting.ENEMY_BOUNDLE_RIGHT - _xInterval/2, MZGameSetting.ENEMY_BOUNDLE_TOP + 100 );
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
	}

	protected override void UpdateWhenActive()
	{
		if( currentEnemyCreatedCount >= maxEnemyCreatedNumber )
			return;

		if( UpdateAndCheckTimeToCreateEnemy() )
		{
			AddNewEnemy( false );
		}
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.partsByNameDictionary[ "MainBody" ].faceTo = new MZFaceTo_MovingDirection();

		MZMode mode = enemy.AddMode( "m" );

		MZMove_LinearBy linear = mode.AddMove<MZMove_LinearBy>( "move" );
		linear.velocity = 160;
		linear.direction = 270;

		int ways = 3 + _attackCode;
		float initDegrees = ( ways == 4 )? 0 : 30;
		float intervalDegrees = 360/ways;

		for( int i = 0; i < ways; i++ )
		{
			MZPartControl pControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
			mode.AddPartControlUpdater().Add( pControl );
			SetTwinWayAttack( pControl, initDegrees + i*intervalDegrees );
		}
	}

	protected override Vector2 GetEnemyStartPosition()
	{
		_dirCode = ( currentEnemyCreatedCount != 0 && currentEnemyCreatedCount%_numberOfRow == 0 )? -_dirCode : _dirCode;
		Vector2 startPosition = ( _dirCode > 0 )? _leftStartPosition : _rightStartPosition;
		return startPosition + new Vector2( _xInterval*( currentEnemyCreatedCount%_numberOfRow )*_dirCode, 0 );
	}

	void SetTwinWayAttack(MZPartControl partControl, float degree)
	{
		float rankDurationMulitiply = 0.2f*rank;

		float colddown = 0.15f;
		float duration = ( 0.3f + ( ( _attackCode == 0 )? 0.6f : 0.3f ) )*rankDurationMulitiply;

		MZAttack_OddWay oddWay = partControl.AddAttack<MZAttack_OddWay>();
		oddWay.colddown = colddown;
		oddWay.duration = duration;
		oddWay.numberOfWays = 2;
		oddWay.offsetPositionsList.Add( MZMath.UnitVectorFromDegrees( degree + 90 )*10 );
		oddWay.offsetPositionsList.Add( MZMath.UnitVectorFromDegrees( degree + 90 )*-10 );
		oddWay.targetHelp = new MZTargetHelp_AssignDirection();
		oddWay.bulletName = "EBBee";
		oddWay.initVelocity = 350;
		( (MZTargetHelp_AssignDirection)oddWay.targetHelp ).direction = degree;

		MZAttack_Idle idle1 = partControl.AddAttack<MZAttack_Idle>();
		idle1.duration = 1.5f;

		MZAttack_OddWay oddWay2 = partControl.AddAttack<MZAttack_OddWay>();
		oddWay2.colddown = colddown;
		oddWay2.duration = duration;
		oddWay2.numberOfWays = 2;
		oddWay2.offsetPositionsList.Add( MZMath.UnitVectorFromDegrees( degree + 90 + 45 )*10 );
		oddWay2.offsetPositionsList.Add( MZMath.UnitVectorFromDegrees( degree + 90 + 45 )*-10 );
		oddWay2.targetHelp = new MZTargetHelp_AssignDirection();
		oddWay2.bulletName = "EBBee";
		oddWay2.initVelocity = 350;
		( (MZTargetHelp_AssignDirection)oddWay2.targetHelp ).direction = degree + 45;

		MZAttack_Idle idle2 = partControl.AddAttack<MZAttack_Idle>();
		idle2.duration = 1.5f;
	}
}
