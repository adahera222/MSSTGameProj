using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacterPart : MZBaseObject, IMZPart, IMZFaceTo, IMZCollision
{
	public List<MZCollision> collisionsList = new List<MZCollision>();
	//
	GameObject _parentGameObject = null;
	MZCharacter _parentCharacter = null;
	MZFaceTo_Base _faceTo = null;

	#region IMZAttack, IMZCollision implementation

	public MZCharacterType characterType
	{
		get{ return _parentCharacter.characterType; }
	}

	public Vector2 realPosition
	{ get { return _parentCharacter.position + this.position; } }

	#endregion

	#region IMZFaceTo implementation

	public Vector2 targetRealPosition
	{
		get{ return MZGameComponents.GetInstance().charactersManager.GetPlayerPosition(); }
	}

	public Vector2 parentMovingVector
	{
		get{ return _parentCharacter.currentMovingVector; }
	}

	public Vector2 selfMovingVector
	{
		get
		{
			throw new System.NotImplementedException();
		}
	}

	#endregion

	public override void Enable()
	{
		base.Enable();
	}

	public override void Disable()
	{
		base.Disable();
	}

	public GameObject parentGameObject
	{
		set
		{
			_parentGameObject = value;
			gameObject.transform.parent = value.transform;
			_parentCharacter = _parentGameObject.GetComponent<MZCharacter>();
		}
		get{ return _parentGameObject; }
	}

	public MZFaceTo_Base faceTo
	{
		set
		{
			_faceTo = value;
			if( _faceTo != null )
				_faceTo.controlTarget = this;
		}
		get { return _faceTo; }
	}

	public MZCollision AddCollision() //delete
	{
//		if( collisionsList == null )
//			collisionsList = new List<MZCollision>();
//
//		MZCollision collision = new MZCollision();
//		collision.collisionDelegate = this;
//		collisionsList.Add( collision );
//
//		return collision;
		return null;
	}

	public bool IsCollide(MZCharacterPart other)
	{
		foreach( MZCollision selfCollision in collisionsList )
		{
			foreach( MZCollision otherCollision in other.collisionsList )
			{
				if( selfCollision.IsCollision( otherCollision ) )
					return true;
			}
		}

		return false;
	}

	public bool IsInScreen()
	{
		float halfMaxEdge = GetMaxEdge()/2;

		Rect boundRect = MZGameSetting.GetPlayerMovableBoundRect();
		Vector2 realPos = realPosition;

		if( realPos.x + halfMaxEdge < boundRect.x ||
			realPos.x - halfMaxEdge > boundRect.x + boundRect.width ||
			realPos.y + halfMaxEdge < boundRect.y - boundRect.height ||
			realPos.y - halfMaxEdge > boundRect.y )
		{
			return false;
		}

		return true;
	}

	override protected void Update()
	{
		base.Update();

		if( _faceTo != null )
		{
			if( parentGameObject.GetComponent<MZCharacter>().characterType == MZCharacterType.EnemyAir ||
				parentGameObject.GetComponent<MZCharacter>().characterType == MZCharacterType.EnemyBullet ) // need fix
			{
				_faceTo.Update();
			}
		}
	}

	void Start()
	{
		if( collisionsList == null )
			return;

		foreach( MZCollision c in collisionsList )
			c.collisionDelegate = this;
	}

	void OnDrawGizmos()
	{
		if( _parentGameObject == null || MZGameSetting.SHOW_COLLISION_RANGE == false || enabled == false )
			return;

		Gizmos.color = MZGameSetting.GetCollisionColor( _parentCharacter.characterType );

		foreach( MZCollision c in collisionsList )
		{
			Vector2 realCenter = c.collisionDelegate.realPosition + c.center;
			Gizmos.DrawWireSphere( realCenter, c.radius );
		}
	}
}