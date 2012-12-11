using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacterPart : MZBaseObject, IMZPart, IMZFaceTo, IMZCollision
{
	List<MZCollision> _collisionsList = null;
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
		get
		{
			if( parentGameObject.GetComponent<MZCharacter>().characterType == MZCharacterType.EnemyAir ||
				parentGameObject.GetComponent<MZCharacter>().characterType == MZCharacterType.EnemyGround )
				return parentGameObject.GetComponent<MZEnemy>().currentMovingVector;

			if( parentGameObject.GetComponent<MZCharacter>().characterType == MZCharacterType.EnemyBullet ||
				parentGameObject.GetComponent<MZCharacter>().characterType == MZCharacterType.PlayerBullet )
				return parentGameObject.GetComponent<MZBullet>().currentMovingVector;
//				return parentGameObject.GetComponent<MZEnemyBullet>().currentMovingVector;

				return Vector2.zero;

//			return parentGameObject.GetComponent<MZCharacter>().currentMovingVector; <-- want this... but ???
		}
	}

	public Vector2 selfMovingVector
	{
		get
		{
			throw new System.NotImplementedException();
		}
	}

	#endregion

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

	public List<MZCollision> collisionsList
	{
		set{ _collisionsList = value; }
		get{ return _collisionsList; }
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

	public MZCollision AddCollision()
	{
		if( _collisionsList == null )
			_collisionsList = new List<MZCollision>();

		MZCollision collision = new MZCollision();
		collision.collisionDelegate = this;
		collisionsList.Add( collision );

		return collision;
	}

	public bool IsCollide(MZCharacterPart other)
	{
		foreach( MZCollision selfCollision in _collisionsList )
		{
			foreach( MZCollision otherCollision in other._collisionsList )
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

	}

	void OnDrawGizmos()
	{
		if( _parentGameObject == null )
			return;
		Gizmos.color = MZGameSetting.GetCollisionColor( _parentCharacter.characterType );

		foreach( MZCollision c in _collisionsList )
		{
			Vector2 realCenter = c.collisionDelegate.realPosition + c.center;
			Gizmos.DrawWireSphere( realCenter, c.radius );
		}
	}
}