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
			return 8;
		}
	}

	int _numberOfRow = 4;
	float _createTimeIntercval = 0.7f;
	float _createTimeCount;
	float _xInterval;
	Vector2 leftStartPosition;
	Vector2 rightStartPosition;

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

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

		for( int i = 0; i < 4; i++ )
		{
			MZPartControl pControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
			mode.AddPartControlUpdater().Add( pControl );
			SetTwinWayAttack( pControl, 0 + i*90 );
		}
	}

	void SetTwinWayAttack(MZPartControl partControl, float degree)
	{
		MZAttack_OddWay oddWay = partControl.AddAttack<MZAttack_OddWay>();
		oddWay.colddown = 0.1f;
		oddWay.duration = 0.4f;
		oddWay.numberOfWays = 2;
		oddWay.offsetPosition.Add( MZMath.UnitVectorFromDegrees( degree + 90 )*10 );
		oddWay.offsetPosition.Add( MZMath.UnitVectorFromDegrees( degree + 90 )*-10 );
		oddWay.targetHelp = new MZTargetHelp_AssignDirection();
		oddWay.bulletName = "EBBee";
		oddWay.initVelocity = 400;
		( (MZTargetHelp_AssignDirection)oddWay.targetHelp ).direction = degree;

		MZAttack_Idle idle = partControl.AddAttack<MZAttack_Idle>();
		idle.duration = 1;

		MZAttack_OddWay oddWay2 = partControl.AddAttack<MZAttack_OddWay>();
		oddWay2.colddown = 0.1f;
		oddWay2.duration = 0.4f;
		oddWay2.numberOfWays = 2;
		oddWay2.offsetPosition.Add( MZMath.UnitVectorFromDegrees( degree + 90 + 45 )*10 );
		oddWay2.offsetPosition.Add( MZMath.UnitVectorFromDegrees( degree + 90 + 45 )*-10 );
		oddWay2.targetHelp = new MZTargetHelp_AssignDirection();
		oddWay2.bulletName = "EBBee";
		oddWay2.initVelocity = 400;
		( (MZTargetHelp_AssignDirection)oddWay2.targetHelp ).direction = degree + 45;
	}

	Vector2 GetStartPosition()
	{
		return( ( currentCreatedMemberCount/_numberOfRow )%2 == 0 )?
			 leftStartPosition + new Vector2( _xInterval*( currentCreatedMemberCount%_numberOfRow ), 0 ) :
				rightStartPosition - new Vector2( _xInterval*( currentCreatedMemberCount%_numberOfRow ), 0 );
	}
}
