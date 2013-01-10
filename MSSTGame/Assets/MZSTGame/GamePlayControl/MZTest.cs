using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MZTest
{
	public enum Type
	{
		None,
		Enable,
//		Formation,
//		FormationState,
	}

	public Type testType = Type.None;
	public List<MZTestEnemySetting> testEnemySettings = new List<MZTestEnemySetting>();
	public List<MZTestFormationSetting> testFormationsSettings = new List<MZTestFormationSetting>();

	//

	MZFormationsManager _formationsManager;

	public void SetForamtions(List<MZFormation> formatonsList, MZFormationsManager formationsManager)
	{
		MZDebug.Assert( formationsManager != null, "_formationsManager is null" );

		_formationsManager = formationsManager;

		foreach( MZFormation f in formatonsList )
			testFormationsSettings.Add( new MZTestFormationSetting( f ) );
	}

	//

	public void Update()
	{
		if( testType != Type.Enable )
			return;

		UpdateTestEnemy();
		UpdateTestFormations();
	}

	//

	void UpdateTestEnemy()
	{
		if( testEnemySettings == null )
			return;

		foreach( MZTestEnemySetting setting in testEnemySettings )
			setting.Update();
	}

	void UpdateTestFormations()
	{
		if( testFormationsSettings == null )
			return;

		foreach( MZTestFormationSetting setting in testFormationsSettings )
		{
			setting.Update();
			if( setting.hasExecting )
			{
				_formationsManager.ExecuteFormation( setting.positionType, setting.sizeType, setting.name );
			}
		}
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
		enemy.GetComponent<MZEnemy>().InitDefaultMode();
		MZGameComponents.instance.charactersManager.Add( MZCharacterType.EnemyAir, enemy.GetComponent<MZCharacter>() );
		
		MZDebug.Log( testEnemyName + " at " + testEnemyStartPosition.ToString() );
		
		_hasNotCreatedTest = false;
	}
}

[System.Serializable]
public class MZTestFormationSetting
{
	public bool create = false;
	public string name;
	public MZFormation.SizeType sizeType;
	public MZFormation.PositionType positionType;

	public bool hasExecting
	{
		get
		{
			bool preValue = _hasExecting;
			_hasExecting = false;
			return preValue;
		}
	}

	bool _hasExecting = false;

	public MZTestFormationSetting(MZFormation formation)
	{
		MZDebug.Assert( formation != null, "formation is null" );

		name = formation.GetType().ToString();
		sizeType = formation.sizeType;
		positionType = formation.positionType;
	}

	public void Update()
	{
		if( create )
		{
			_hasExecting = true;
			create = false;
		}
	}
}
