using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MZFormation : MZControlBase
{
	public enum Type
	{
		Unknow,
		Large,
		Mid,
		Small,
	}

	public enum SideType
	{
		Left,
		Right,
		Mid,
	}

	//

	int _stateExp = int.MinValue;
	Type _type = Type.Unknow;
	List<MZEnemy> _enemiesList = null;
	//

	public abstract float nextCreatedTime
	{ get; }

	public Type type
	{
		set { _type = value; }
		get { return _type; }
	}

	public int stateExp
	{
		set{ _stateExp = value; }
		get{ return ( _stateExp != int.MinValue )? _stateExp : GetDefaultStateExp( type ); }
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

	//
	protected MZEnemy Add(MZCharacterType type, string name)
	{
		GameObject enemyObject = MZCharacterObjectsFactory.instance.Get( type, name );
		MZEnemy enemy = enemyObject.GetComponent<MZEnemy>();
		enemy.belongFormation = this;

		if( _enemiesList == null )
			_enemiesList = new List<MZEnemy>();

		_enemiesList.Add( enemy );

		return enemy;
	}

	//

	public int GetDefaultStateExp(Type _type)
	{
		switch( _type )
		{
			case Type.Small:
				return 1;
			case Type.Mid:
				return 2;
			case Type.Large:
				return 11;
			default:
				return 0;
		}
	}
}
