using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	 
	int _stateExp = int.MinValue;
	SizeType _sizeType = SizeType.Unknow;
	PositionType _positionType = PositionType.Unknow;
	List<MZEnemy> _enemiesList = null;
	//

	public abstract float nextCreatedTime
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

	public int belongEnemiesCount
	{ get { return ( _enemiesList != null )? _enemiesList.Count : 0; } }

	//

	public void Remove(MZEnemy enemy)
	{
		MZDebug.Assert( enemy.belongFormation == this, "I am not your father" );
		_enemiesList.Remove( enemy );
	}

	public override void Reset()
	{
		base.Reset();
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
	protected MZEnemy AddNewEnemy(MZCharacterType type, string name, bool initDefaultMode)
	{
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

		return enemy;
	}

	/// <summary>
	/// set new enemy before enable, such like position, override default mode ... etc
	/// </summary>
	/// <param name='enemy'>
	/// Enemy.
	/// </param>
	protected abstract void NewEnemyBeforeEnable(MZEnemy enemy);
}
