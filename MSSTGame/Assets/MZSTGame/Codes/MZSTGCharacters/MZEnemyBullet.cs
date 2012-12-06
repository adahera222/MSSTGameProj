using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZEnemyBullet : MonoBehaviour, IMZMove
{
	public Vector2 position
	{
		set{ gameObject.GetComponent<MZCharacter>().position = value; }
		get{ return gameObject.GetComponent<MZCharacter>().position; }
	}

	public Vector2 movingVector;

	public List<MZMove_Base> movesList
	{
		get
		{
			if( _moveControlUpdate == null )
				_moveControlUpdate = new MZControlUpdate<MZMove_Base>();

			return _moveControlUpdate.controlsList;
		}
	}

	public MZMove_Base AddMove(string name, string typeString)
	{
		MZMove_Base move = (MZMove_Base)MZObjectHelp.CreateClass( "MZMove_" + typeString );
		move.name = name;
		move.controlTarget = gameObject.GetComponent<MZCharacter>();

		movesList.Add( move );

		return move;
	}

	MZControlUpdate<MZMove_Base> _moveControlUpdate = null;

	void Start()
	{
		gameObject.GetComponent<MZCharacter>().enableRemoveTime = 0.5f;
	}

	void Update()
	{
		_moveControlUpdate.Update();
	}
}
