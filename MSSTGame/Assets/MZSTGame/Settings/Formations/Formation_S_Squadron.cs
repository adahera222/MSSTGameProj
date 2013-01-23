using UnityEngine;
using System.Collections;

public class Formation_S_Squadron : MZFormation
{
	public override float disableNextFormationTime
	{
		get
		{
			return 3;
		}
	}

	protected override int maxCreatedNumber
	{
		get
		{
			return 6;
		}
	}

	int row = 3;
	int col = 3;
	Vector2 _enemyInterval = new Vector2( 90, 90 );
	Vector2 _leaderTargetPosition;
	Vector2 _currentNewEnemyDestPos;

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_leaderTargetPosition = GetLeaderPosition();

		if( positionType == MZFormation.PositionType.Mid )
		{
			CreateMidTypeEnemies();
		}
		else
		{
			CreateSideTypeEnemies();
		}
	}

	protected override void UpdateWhenActive()
	{

	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.position = GetStartPosition();

		MZMode mode = enemy.AddMode( "m" );

		MZMove_LinearTo move = mode.AddMove<MZMove_LinearTo>( "m" );
		move.destinationPosition = _currentNewEnemyDestPos;
		move.totalTime = 0.3f;
		move.duration = 1.0f;

		mode.AddMove<MZMove_Idle>( "idle" ).duration = 3;

		MZMove_LinearBy leave = mode.AddMove<MZMove_LinearBy>( "leave" );
		leave.direction = 270;
		leave.velocity = 200;

		MZPartControl mainPartControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( mainPartControl );

		MZAttack_Idle attackShow = mainPartControl.AddAttack<MZAttack_Idle>();
		attackShow.duration = 0.5f;
		attackShow.isRunOnce = true;

		MZAttack_EvenWay attackEven = mainPartControl.AddAttack<MZAttack_EvenWay>();
		attackEven.numberOfWays = 4;
		attackEven.colddown = 0.1f;
		attackEven.duration = 0.5f;
		attackEven.intervalDegrees = 45;
		attackEven.initVelocity = 300;// + 25*currentCreatedMemberCount;
//		attackEven.additionalVelocity = 50;
		attackEven.bulletName = "EBBee";
		attackEven.targetHelp = new MZTargetHelp_Target();

		MZAttack_Idle attackIdleInterval = mainPartControl.AddAttack<MZAttack_Idle>();
		attackIdleInterval.duration = 0.3f;

		MZAttack_OddWay attackOdd = mainPartControl.AddAttack<MZAttack_OddWay>();
		attackOdd.numberOfWays = 1;
		attackOdd.colddown = 0.1f;
		attackOdd.duration = 0.5f;
		attackOdd.intervalDegrees = 5;
		attackOdd.initVelocity = 600;// + 25*currentCreatedMemberCount;
		attackOdd.bulletName = "EBBee2";
		attackOdd.targetHelp = new MZTargetHelp_Target();
//		attackOdd.additionalVelocity = 50;

		MZAttack_Idle attackIdle = mainPartControl.AddAttack<MZAttack_Idle>();
		attackIdle.duration = 3.0f;
	}

	void CreateMidTypeEnemies()
	{
		for( int i = 0; i < row; i++ )
		{
			Vector2 leftPositionOfCol = new Vector2( _enemyInterval.x/2*i, _enemyInterval.y*i );

			for( int j = 0; j < col - i; j++ )
			{
				_currentNewEnemyDestPos = _leaderTargetPosition + leftPositionOfCol + new Vector2( _enemyInterval.x*j, 0 );
				AddNewEnemy( MZCharacter.MZCharacterType.EnemyAir, "EnemySGreen", false );
			}
		}
	}

	void CreateSideTypeEnemies()
	{
		Vector2 __enemyInterval = _enemyInterval;
		__enemyInterval.x = ( positionType == MZFormation.PositionType.Left )? -_enemyInterval.x : _enemyInterval.x;

		for( int i = 0; i < row; i++ )
		{
			float offsetY = _enemyInterval.y/2*i;

			for( int j = 0; j < col - i; j++ )
			{
				_currentNewEnemyDestPos = new Vector2( _leaderTargetPosition.x + __enemyInterval.x*i, offsetY + _leaderTargetPosition.y + __enemyInterval.y*j );
				AddNewEnemy( MZCharacter.MZCharacterType.EnemyAir, "EnemySGreen", false );
			}
		}
	}

	Vector2 GetLeaderPosition()
	{
		Vector2 offset = new Vector2( 225, 150 );

		switch( positionType )
		{
			case PositionType.Left:
				return new Vector2( MZGameSetting.ENEMY_BOUNDLE_LEFT + offset.x, MZGameSetting.ENEMY_BOUNDLE_TOP - offset.y );

			case PositionType.Right:
				return new Vector2( MZGameSetting.ENEMY_BOUNDLE_RIGHT - offset.x, MZGameSetting.ENEMY_BOUNDLE_TOP - offset.y );

			case PositionType.Mid:
				return new Vector2( -_enemyInterval.x, MZGameSetting.ENEMY_BOUNDLE_TOP - _enemyInterval.y*2.3f );

			default:
				MZDebug.Assert( positionType != MZFormation.PositionType.Mid, "not support" );
				return Vector2.zero;
		}
	}

	Vector2 GetStartPosition()
	{
		if( positionType == MZFormation.PositionType.Mid )
		{
			int grounpSign = ( currentCreatedMemberCount == 0 || currentCreatedMemberCount == 3 || currentCreatedMemberCount == 5 )? -1 : 1;
			float x = 400*grounpSign;
			float y = 400*grounpSign;
			Vector2 offset = new Vector2( x, y );

			return _currentNewEnemyDestPos + offset;
		}
		else
		{
			float y = ( currentCreatedMemberCount == 0 || currentCreatedMemberCount == 3 || currentCreatedMemberCount == 5 )? -500 : ( currentCreatedMemberCount == 1 )? 0 : 500;
			Vector2 offsetToStart = new Vector2( 300*( ( positionType == MZFormation.PositionType.Left )? -1 : 1 ), y );
			return _currentNewEnemyDestPos + offsetToStart;
		}
	}
}
