using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZEnemy : MonoBehaviour, IMZMode
{
	public int healthPoint = 1;
	MZControlUpdate<MZMode> _modeControlUpdate = new MZControlUpdate<MZMode>();

	#region IMZMode implementation

	public IMZMove moveControlTarget
	{ get { return gameObject.GetComponentInChildren<MZCharacter>(); } }

	public Dictionary<string, MZCharacterPart> partsByNameDictionary
	{ get { return gameObject.GetComponent<MZCharacter>().partsByNameDictionary; } }

	#endregion

	public List<MZMode> modesList
	{
		get
		{
			if( _modeControlUpdate == null )
				_modeControlUpdate = new MZControlUpdate<MZMode>();

			return _modeControlUpdate.controlsList;
		}
	}

	public Vector2 currentMovingVector
	{
		get{ return _modeControlUpdate.currentControl.currentMovingVector;}
	}

	public MZMode AddMode(string name)
	{
		MZMode mode = new MZMode();
		mode.name = name;
		mode.controlTarget = this;

		modesList.Add( mode );

		return mode;
	}

	void Start()
	{
		gameObject.GetComponent<MZCharacter>().enableRemoveTime = 10.0f;
	}

	void Update()
	{
		_modeControlUpdate.Update();
		UpdateCollision();
	}

	void UpdateCollision()
	{
		foreach( GameObject pb in GameObject.Find("MZCharactersManager").GetComponent<MZCharactersManager>().GetList( MZCharacterType.PlayerBullet ) )
		{
			if( pb.GetComponent<MZCharacter>().IsCollide( gameObject.GetComponent<MZCharacter>() ) )
			{
				healthPoint -= 1;
				pb.GetComponent<MZCharacter>().Disable();
			}
		}

		if( healthPoint <= 0 )
			gameObject.GetComponent<MZCharacter>().Disable();
	}
}