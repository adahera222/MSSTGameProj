using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class Formation_S_Rail : MZFormation
{
	public override float disableNextFormationTime
	{
		get
		{
			return 1.5f;
		}
	}

	protected override int maxCreatedNumber
	{
		get
		{
			return 8;
		}
	}

	//

	int _constructCode;
	float _createInterval;
	float _createTimeCount;
	Vector2 _initPosition;
	MZMove.RotationType rotationType;
	string enemyName = "EnemySYellow";

	//

	protected override void FirstUpdate()
	{
		base.FirstUpdate();

		_createInterval = 0.25f;
		_createTimeCount = 0;
		_constructCode = MZMath.RandomFromRange( 0, 1 );

		SetInitByType();
	}

	protected override void UpdateWhenActive()
	{
		_createTimeCount -= MZTime.deltaTime;

		if( _createTimeCount < 0 )
		{
			_createTimeCount += _createInterval;

			AddNewEnemy( MZCharacterType.EnemyAir, enemyName, false );
		}
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.healthPoint = 1;
		enemy.position = _initPosition;
		MZMove.RotationType rotType = rotationType;

		enemy.partsByNameDictionary[ "MainBody" ].faceTo = new MZFaceTo_MovingDirection();

		if( _constructCode == 1 )
		{
			if( positionType == MZFormation.PositionType.Mid )
			{
				enemy.position += new Vector2( 50*( ( currentCreatedMemberCount%2 == 0 )? 1 : -1 ), 0 );
			}
			else
			{
				enemy.position += ( ( currentCreatedMemberCount%2 == 0 )? new Vector2( 0, 100 ) : Vector2.zero );
			}
		}

		if( positionType == MZFormation.PositionType.Mid )
		{
			rotType = ( currentCreatedMemberCount%2 == 0 )? MZMove.RotationType.CCW : MZMove.RotationType.CW;
		}

		MZMode mode = enemy.AddMode( "mode" );

		// move
		float velocity = 400;

		MZMove_LinearBy moveLinear1 = mode.AddMove<MZMove_LinearBy>( "l1" );
		moveLinear1.direction = ( positionType == MZFormation.PositionType.Mid )? 270 : GetStartDirection( rotType );
		moveLinear1.velocity = velocity;
		moveLinear1.duration = 0.8f;

		MZMove_DegreesTo moveTurn = mode.AddMove<MZMove_DegreesTo>( "t" );
		moveTurn.rotationType = rotType;
		moveTurn.direction = moveLinear1.direction;
		moveTurn.destinationDegrees = GetDestinationDirection( rotType );
		moveTurn.totalTime = 0.5f;
		moveTurn.duration = 100;
		moveTurn.velocity = velocity;

		// main attack
		MZPartControl mainPartControl = new MZPartControl( enemy.partsByNameDictionary[ "MainBody" ] );
		mode.AddPartControlUpdater().Add( mainPartControl );

		MZAttack_OddWay attack = mainPartControl.AddAttack<MZAttack_OddWay>();
		attack.numberOfWays = 1;
		attack.initVelocity = 300;
		attack.additionalVelocity = 150;
		attack.bulletName = "EBDonuts";
		attack.colddown = 0.1f;
		attack.duration = 0.3f;
		attack.targetHelp = new MZTargetHelp_Target();

		MZAttack_Idle attackIdle = mainPartControl.AddAttack<MZAttack_Idle>();
		attackIdle.duration = 3;
	}

	void SetInitByType()
	{
		float sideX = 320;
		float offsetX = 30;
		float sideY = 600;

		switch( positionType )
		{
			case PositionType.Left:
				_initPosition = new Vector2( -sideX - offsetX, sideY );
				rotationType = MZMove.RotationType.CW;
				break;

			case PositionType.Right:
				_initPosition = new Vector2( sideX + offsetX, sideY );
				rotationType = MZMove.RotationType.CCW;
				break;

			case PositionType.Mid:
				_initPosition = new Vector2( 0, sideY );
				break;

			default:
				MZDebug.AssertFalse( "not support" );
				break;
		}
	}

	float GetStartDirection(MZMove.RotationType rotType)
	{
		return ( rotType == MZMove.RotationType.CW )? -45 : 360 - 135;
	}

	float GetDestinationDirection(MZMove.RotationType rotType)
	{
		return ( rotType == MZMove.RotationType.CW )? 360 - 135 : -45;
	}
}