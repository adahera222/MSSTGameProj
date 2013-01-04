using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public Type testType = Type.None;
	public List<MZTestEnemySetting> testEnemySettings = new List<MZTestEnemySetting>();

	public void Update()
	{
		switch( testType )
		{
			case Type.Enemy:
				UpdateTestEnemy();
				break;
		}
	}

	//

	void UpdateTestEnemy()
	{
		if( testEnemySettings == null )
			return;

		foreach( MZTestEnemySetting setting in testEnemySettings )
			setting.Update();
	}
}

[System.Serializable]
public class MZTestEnemySetting
{
	public bool create = false;
	public string testEnemyName = null;
	public Vector2 testEnemyStartPosition = Vector2.zero;

	//

	bool _hasNotCreatedTest;

	//

	public void Update()
	{
		_hasNotCreatedTest = create;
		create = false;

		if( _hasNotCreatedTest == false || testEnemyName == null )
			return;

		GameObject enemy = MZCharacterObjectsFactory.instance.Get( MZCharacterType.EnemyAir, testEnemyName );
		enemy.GetComponent<MZEnemy>().position = testEnemyStartPosition;

		MZDebug.Log( testEnemyName + " at " + testEnemyStartPosition.ToString() );

		_hasNotCreatedTest = false;
	}
}