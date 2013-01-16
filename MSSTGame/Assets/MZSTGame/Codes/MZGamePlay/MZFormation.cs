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

	//

	int _stateExp = int.MinValue;
	int _createdMemberCount = 0;
	SizeType _sizeType = SizeType.Unknow;
	PositionType _positionType = PositionType.Unknow;
	List<MZEnemy> _enemiesList = null;

	//

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

	//

	public override bool ActiveCondition()
	{
		return ( _createdMemberCount < maxCreatedNumber || existedBelongEnemiesCount > 0 );
	}

	public override void Enable()
	{
		base.Enable();
		_createdMemberCount = 0;
		_enemiesList = new List<MZEnemy>();
	}
	//

	protected int currentCreatedMemberCount
	{get{ return _createdMemberCount; }}

	protected override void FirstUpdate()
	{
		base.FirstUpdate();
		MZDebug.Assert( maxCreatedNumber >= 0, "maxCreateNumber must br more than zero" );
	}

	protected MZEnemy AddNewEnemy(MZCharacterType type, string name, bool initDefaultMode)
	{
		if( _createdMemberCount >= maxCreatedNumber )
			return null;

		GameObject enemyObject = MZCharacterObjectsFactory.instance.Get( type, name );
		MZEnemy enemy = enemyObject.GetComponent<MZEnemy>();
		enemy.belongFormation = this;

		if( initDefaultMode )
			enemy.InitDefaultMode();

		NewEnemyBeforeEnable( enemy );

		if( _enemiesList == null )
			_enemiesList = new List<MZEnemy>();
		_enemiesList.Add( enemy );

		MZGameComponents.instance.charactersManager.Add( type, enemy.GetComponent<MZCharacter>() );

		_createdMemberCount++;
		MZDebug.Assert( _createdMemberCount <= maxCreatedNumber, "created/max=" + _createdMemberCount.ToString() + "/" + maxCreatedNumber + "???" );

		return enemy;
	}

	protected abstract int maxCreatedNumber
	{ get; }

	/// <summary>
	/// set new enemy before enable, such like position, override default mode ... etc
	/// </summary>
	/// <param name='enemy'>
	/// Enemy.
	/// </param>
	protected abstract void NewEnemyBeforeEnable(MZEnemy enemy);
}
