using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MZFormation : MZControlBase
{
	List<MZEnemy> _enemiesList = null;
	//
	public int belongEnemiesCount
	{
		get { return ( _enemiesList != null )? _enemiesList.Count : 0; }
	}
	//
	public void Remove(MZEnemy enemy)
	{
		MZDebug.Assert( enemy.belongFormation == this, "I am not your father" );
		_enemiesList.Remove( enemy );
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

}
