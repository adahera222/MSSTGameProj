using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class TestFormation_S000 : MZFormation
{
	//

	float _createTimeCount;
	float _createInterval;
	Vector2 _initPosition;

	//

	public override float disableNextFormationTime
	{ get { return 1; } }

	//

	public TestFormation_S000() : base()
	{
		duration = 1.3f;
	}

	public override void Enable()
	{
		base.Enable();
		_createTimeCount = 0;
		_createInterval = 0.8f;
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
			AddNewEnemy( MZCharacterType.EnemyAir, "EnemyS000", false );
			_createTimeCount += _createInterval;
		}
	}

	protected override void NewEnemyBeforeEnable(MZEnemy enemy)
	{
		enemy.position = _initPosition;
		enemy.InitDefaultMode();
	}

	//

	Vector2 GetInitPosition(PositionType sideType)
	{
		float xValue = 280;
		float yValue = 400;

		switch( sideType )
		{
			case PositionType.Left:
				return new Vector3( -xValue, yValue );
			case PositionType.Right:
				return new Vector3( xValue, yValue );
			case PositionType.Mid:
			default:
				return new Vector3( 0, yValue + 90 );
		}
	}
}
