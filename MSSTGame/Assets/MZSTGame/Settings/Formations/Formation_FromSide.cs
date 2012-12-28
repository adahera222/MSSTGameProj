using UnityEngine;
using System.Collections;

public class Formation_FromSide : MZFormation
{
	int _choice;
	float _createTimeCount;
	float _createInterval;

	public override void Reset()
	{
		base.Reset();

		_choice = 0;
		_createTimeCount = 0;
		_createInterval = 0.4f;
	}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		_choice = MZMath.RandomFromRange( 0, 1 );
	}

	protected override void UpdateWhenActive()
	{
		_createTimeCount -= MZTime.deltaTime;

		if( _createTimeCount < 0 )
		{
			GameObject enemy = MZCharacterObjectsFactory.instance.Get( MZCharacterType.EnemyAir, "EnemyS000" );
			float xValue = 330;
			enemy.GetComponent<MZEnemy>().position = ( _choice == 0 )? new Vector3( -xValue, 400 ) : new Vector3( xValue, 400 );

			_createTimeCount += _createInterval;
		}
	}
}
