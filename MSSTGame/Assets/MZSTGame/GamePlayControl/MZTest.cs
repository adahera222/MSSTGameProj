using UnityEngine;
using System.Collections;

[System.Serializable]
public class MZTest
{
	public enum Type
	{
		None,
		Enemy,
		Formation,
		FormationState,
	}

	public bool create = false;
	public Type testType = Type.None;
	public string testEnemyName = null;
	public Vector2 testEnemyStartPosition = Vector2.zero;

	//

	bool _hasNotCreatedTest = false;

	//

	public void Update()
	{
		_hasNotCreatedTest = create;
		create = false;

		if( _hasNotCreatedTest == false )
			return;

		switch( testType )
		{
			case Type.Enemy:
				UpdateTestEnemy();
				break;
		}

		_hasNotCreatedTest = false;
	}

	//

	void UpdateTestEnemy()
	{
		MZDebug.Assert( testEnemyName != null, @"testEnemyName is null" );

		GameObject enemy = MZCharacterObjectsFactory.instance.Get( MZCharacterType.EnemyAir, testEnemyName );
		enemy.GetComponent<MZEnemy>().position = testEnemyStartPosition;

		MZDebug.Log( testEnemyName + " at " + testEnemyStartPosition.ToString() );
	}
}