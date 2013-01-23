using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MZCharacterType = MZCharacter.MZCharacterType;

public class MZCharacterPart : MZBaseObject, IMZPart, IMZFaceTo, IMZCollision
{
	public List<MZCollision> collisionsList = new List<MZCollision>();

	public MZFaceTo faceTo
	{
		set
		{
			MZDebug.Assert( value != null, "face to is null" );
			_faceTo = value;
			_faceTo.controlDelegate = this;
		}
		get
		{
			return _faceTo;
		}
	}

	//
	GameObject _parentGameObject = null;
	MZCharacter _parentCharacter = null;
	MZFaceTo _faceTo = null;

	#region IMZAttack, IMZCollision implementation

	public MZCharacterType characterType
	{
		get{ return _parentCharacter.characterType; }
	}

	public Vector2 realPosition
	{
		get	{ return _parentCharacter.position + this.position;	}
	}

	#endregion

	#region IMZFaceTo implementation

	public Vector2 targetRealPosition
	{
		get{ return MZGameComponents.GetInstance().charactersManager.GetPlayerPosition(); }
	}

	public float movingDirection
	{
		get{ return _parentCharacter.currentMovingDirection; }
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
			_parentCharacter = _parentGameObject.GetComponent<MZCharacter>();
		}
		get{ return _parentGameObject; }
	}

	public bool IsCollide(MZCharacterPart other)
	{
		foreach( MZCollision selfCollision in collisionsList )
		{
			foreach( MZCollision otherCollision in other.collisionsList )
			{
				if( selfCollision.IsCollision( otherCollision ) )
				{
					return true;
				}
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
			_faceTo.Update();
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
		if( _parentGameObject == null || MZGameSetting.SHOW_COLLISION_RANGE == false || MZGameSetting.SHOW_GIZMOS == false || enabled == false )
			return;

		Gizmos.color = MZGameSetting.GetCollisionColor( _parentCharacter.characterType );

		foreach( MZCollision c in collisionsList )
		{
			Vector2 realCenter = c.collisionDelegate.realPosition + c.center;
			Gizmos.DrawWireSphere( realCenter, c.radius );
		}
	}
}