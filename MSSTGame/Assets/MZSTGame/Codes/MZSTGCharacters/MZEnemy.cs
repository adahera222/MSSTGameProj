using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZEnemy : MZCharacter, IMZMode, IMZMove
{
	public int healthPoint = 1;
	MZControlUpdate<MZMode> _modeControlUpdate = new MZControlUpdate<MZMode>();

	#region IMZMode implementation

	public IMZMove moveControlTarget
	{ get { return this; } }

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

	protected override void Start()
	{
		base.Start();

		enableRemoveTime = 10.0f;
	}

	protected override void Update()
	{
		base.Update();

		_modeControlUpdate.Update();
//		UpdateCollision();

		if( healthPoint <= 0 )
			Disable();
	}

	void UpdateCollision()
	{
//		foreach( GameObject pb in GameObject.Find("MZCharactersManager").GetComponent<MZCharactersManager>().GetList( MZCharacterType.PlayerBullet ) )
//		{
//			if( pb.GetComponent<MZCharacter>().IsCollide( gameObject.GetComponent<MZCharacter>() ) )
//			{
//				healthPoint -= 1;
//				pb.GetComponent<MZCharacter>().Disable();
//			}
//		}

//		foreach( GameObject playerBullet in MZGameComponents.GetInstance().charactersManager.GetList( MZCharacterType.PlayerBullet ) )
//		{
//			if( playerBullet.GetComponent<MZCharacter>().IsCollide( gameObject.GetComponent<MZCharacter>() ) )
//			{
////				healthPoint -= 1;
//				playerBullet.GetComponent<MZCharacter>().Disable();
//			}
//		}
	}
}