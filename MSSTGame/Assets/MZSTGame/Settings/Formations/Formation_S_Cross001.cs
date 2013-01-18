using UnityEngine;
using System.Collections;

public class Formation_S_Cross : MZFormation
{
	public override float disableNextFormationTime
	{
		get
		{
			return 6;
		}
	}

	protected override int maxCreatedNumber
	{
		get
		{
			return _maxCreatedNumber;
		}
	}

	int _attackCode;
	int _dirCode;
	int _numberOfRow = 4;
	int _maxCreatedNumber = 8;
	float _createTimeIntercval = 0.7f;
	float _createTimeCount;
	float _xInterval;
	Vector2 leftStartPosition;
	Vector2 rightStartPosition;

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_dirCode = ( MZMath.RandomFromRange( 0, 1 ) == 0 )? 1 : -1;
		_attackCode = MZMath.RandomFromRange( 0, 1 );
		_createTimeCount = 0;

		_xInterval = ( MZGameSetting.ENEMY_BOUNDLE_RIGHT - MZGameSetting.ENEMY_BOUNDLE_LEFT )/_numberOfRow;
		leftStartPosition = new Vector2( MZGameSetting.ENEMY_BOUNDLE_LEFT + _xInterval/2, MZGameSetting.ENEMY_BOUNDLE_TOP + 100 );
		rightStartPosition = new Vector2( MZGameSetting.ENEMY_BOUNDLE_RIGHT - _xInterval/2, MZGameSetting.ENEMY_BOUNDLE_TOP + 100 );
	}

	protected override void UpdateWhenActive()
	{
		_createTimeCount -= MZTime.deltaTime;
		if( _createTimeCount >= 0 || currentCreatedMemberCount >= maxCreatedNumber )
			return;

		_createTimeCount += _createTimeIntercval;

		AddNewEnemy( MZCharacter.MZCharacterType.EnemyAir, "EnemySRed", false );
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.position = GetStartPosition();

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

	void SetTwinWayAttack(MZPartControl partControl, float degree)
	{
		float colddown = 0.1f;
		float duration = 0.3f + ( ( _attackCode == 0 )? 0.6f : 0.0f );

		MZAttack_OddWay oddWay = partControl.AddAttack<MZAttack_OddWay>();
		oddWay.colddown = colddown;
		oddWay.duration = duration;
		oddWay.numberOfWays = 2;
		oddWay.offsetPosition.Add( MZMath.UnitVectorFromDegrees( degree + 90 )*10 );
		oddWay.offsetPosition.Add( MZMath.UnitVectorFromDegrees( degree + 90 )*-10 );
		oddWay.targetHelp = new MZTargetHelp_AssignDirection();
		oddWay.bulletName = "EBBee";
		oddWay.initVelocity = 400;
		( (MZTargetHelp_AssignDirection)oddWay.targetHelp ).direction = degree;

		MZAttack_Idle idle1 = partControl.AddAttack<MZAttack_Idle>();
		idle1.duration = 0.5f;

		MZAttack_OddWay oddWay2 = partControl.AddAttack<MZAttack_OddWay>();
		oddWay2.colddown = colddown;
		oddWay2.duration = duration;
		oddWay2.numberOfWays = 2;
		oddWay2.offsetPosition.Add( MZMath.UnitVectorFromDegrees( degree + 90 + 45 )*10 );
		oddWay2.offsetPosition.Add( MZMath.UnitVectorFromDegrees( degree + 90 + 45 )*-10 );
		oddWay2.targetHelp = new MZTargetHelp_AssignDirection();
		oddWay2.bulletName = "EBBee";
		oddWay2.initVelocity = 400;
		( (MZTargetHelp_AssignDirection)oddWay2.targetHelp ).direction = degree + 45;

		MZAttack_Idle idle2 = partControl.AddAttack<MZAttack_Idle>();
		idle2.duration = 0.5f;
	}

	Vector2 GetStartPosition()
	{
		_dirCode = ( currentCreatedMemberCount != 0 && currentCreatedMemberCount%_numberOfRow == 0 )? -_dirCode : _dirCode;
		Vector2 startPosition = ( _dirCode > 0 )? leftStartPosition : rightStartPosition;
		return startPosition + new Vector2( _xInterval*( currentCreatedMemberCount%_numberOfRow )*_dirCode, 0 );
	}
}
