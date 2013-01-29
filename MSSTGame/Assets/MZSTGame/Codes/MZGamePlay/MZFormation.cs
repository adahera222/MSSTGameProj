using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MZCharacterType = MZCharacter.MZCharacterType;

public abstract class MZFormation : MZControlBase
{
	public enum SizeType
	{
		Unknow,
		Large,
		Mid,
		Small,
	}

	public enum PositionType
	{
		Unknow,
		Any,
		Left,
		Right,
		Mid,
	}

	//

	public int rank = 0;

	public bool isAllMembersTakenDownByPlayer
	{
		get
		{
			return ( maxEnemyCreatedNumber == _enemyDefeatedCount );
		}
	}

	public new float duration
	{
		get{ return -1; }
	}

	public abstract float disableNextFormationTime
	{ get; }

	public SizeType sizeType
	{
		set { _sizeType = value; }
		get { return _sizeType; }
	}

	public PositionType positionType
	{
		set{ _positionType = value; }
		get{ return _positionType; }
	}

	public int stateExp
	{
		set{ _stateExp = value; }
		get{ return ( _stateExp != int.MinValue )? _stateExp : GetDefaultStateExp( sizeType ); }
	}

	public int existedBelongEnemiesCount
	{
		get{ return ( _enemiesList != null )? _enemiesList.Count : 0; }
	}

	public int constructCode
	{ get { return _constructCode; } }

	//

	protected float enemyCreateTimeInterval;
	protected string enemyName;
	protected float enemyCreateTimeCount;

	//

	int _stateExp = int.MinValue;
	int _enemyCreatedCount = 0;
	int _enemyDefeatedCount = 0;
	int _constructCode = 0;
	SizeType _sizeType = SizeType.Unknow;
	PositionType _positionType = PositionType.Unknow;
	List<MZEnemy> _enemiesList = null;

	//

	public MZFormation() : base()
	{

	}

	public MZFormation(int constructCode) : base()
	{
		_constructCode = constructCode;
	}

	public void Remove(MZEnemy enemy)
	{
		MZDebug.Assert( enemy.belongFormation == this && _enemiesList.Contains( enemy ), "I am not your father" );
		_enemiesList.Remove( enemy );
	}

	public int GetDefaultStateExp(SizeType _type)
	{
		switch( _type )
		{
			case SizeType.Small:
				return 1;
			case SizeType.Mid:
				return 2;
			case SizeType.Large:
				return 11;
			default:
				return 0;
		}
	}

	public void NotifyDefeated(MZEnemy enemy)
	{
		MZDebug.Assert( enemy.currentHealthPoint <= 0, "you hp={0}, are you sure you would be defeated", enemy.healthPoint );
		MZDebug.Assert( _enemiesList.Contains( enemy ) == true, "you are not my member, go out " + enemy.name );

		_enemyDefeatedCount++;
	}

	//

	public override bool ActiveCondition()
	{
		return ( _enemyCreatedCount < maxEnemyCreatedNumber || existedBelongEnemiesCount > 0 );
	}

	public override void Enable()
	{
		MZDebug.Assert( rank > 0, "You must set rank first" );

		base.Enable();

		enemyCreateTimeCount = 0;
		_enemyCreatedCount = 0;
		_enemyDefeatedCount = 0;
		_enemiesList = new List<MZEnemy>();

		InitValues();
	}

	//

	protected bool UpdateAndCheckTimeToCreateEnemy()
	{
		if( currentEnemyCreatedCount >= maxEnemyCreatedNumber )
			return false;

		enemyCreateTimeCount -= MZTime.deltaTime;

		if( enemyCreateTimeCount < 0 )
		{
			enemyCreateTimeCount += enemyCreateTimeInterval;
			return true;
		}

		return false;
	}

	protected int currentEnemyCreatedCount
	{ get { return _enemyCreatedCount; } }

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		MZDebug.Assert( maxEnemyCreatedNumber >= 0, "maxCreateNumber must br more than zero" );
	}

	protected MZEnemy AddNewEnemy(bool initDefaultMode)
	{
		if( _enemyCreatedCount >= maxEnemyCreatedNumber )
			return null;

		MZCharacterType type = MZCharacterType.EnemyAir;
		string name = enemyName;

		GameObject enemyObject = MZCharacterObjectsFactory.instance.Get( type, name );
		MZEnemy enemy = enemyObject.GetComponent<MZEnemy>();
		enemy.belongFormation = this;

		if( initDefaultMode )
			enemy.InitDefaultMode();

		enemy.position = GetEnemyStartPosition();
		NewEnemyBeforeEnable( enemy );

		if( _enemiesList == null )
			_enemiesList = new List<MZEnemy>();
		_enemiesList.Add( enemy );

		MZGameComponents.instance.charactersManager.Add( type, enemy.GetComponent<MZCharacter>() );

		_enemyCreatedCount++;
		MZDebug.Assert( _enemyCreatedCount <= maxEnemyCreatedNumber, "created/max=" + _enemyCreatedCount.ToString() + "/" + maxEnemyCreatedNumber + "???" );

		return enemy;
	}

	protected abstract int maxEnemyCreatedNumber
	{ get; }

	/// <summary>
	/// Inits the values.
	/// </summary>
	protected abstract void InitValues();

	/// <summary>
	/// Gets the enemy start position.
	/// </summary>
	/// <returns>
	/// The enemy start position.
	/// </returns>
	protected abstract Vector2 GetEnemyStartPosition();

	/// <summary>
	/// set new enemy before enable, such like position, override default mode ... etc
	/// </summary>
	/// <param name='enemy'>
	/// Enemy.
	/// </param>
	protected abstract void NewEnemyBeforeEnable(MZEnemy enemy);
}
