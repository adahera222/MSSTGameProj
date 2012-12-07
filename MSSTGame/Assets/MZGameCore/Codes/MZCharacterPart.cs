using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacterPart : MZBaseObject, IMZPart, IMZFaceTo
{
	List<MZCollision> _collisionsList = new List<MZCollision>();
	GameObject _parentGameObject = null;
	MZFaceTo_Base _faceTo = null;

	#region IMZAttack implementation

	public MZCharacterType characterType
	{
		get{ return _parentGameObject.GetComponent<MZCharacter>().characterType; }
	}

	public Vector2 realPosition
	{ get { return _parentGameObject.GetComponent<MZCharacter>().position + this.position; } }

	#endregion

	#region IMZFaceTo implementation

	public Vector2 targetRealPosition
	{
		get{ return GameObject.Find( "MZCharactersManager" ).GetComponent<MZCharactersManager>().GetPlayerPosition(); }
	}

	public Vector2 parentMovingVector
	{
		get
		{
			if( parentGameObject.GetComponent<MZCharacter>().characterType == MZCharacterType.EnemyAir ||
				parentGameObject.GetComponent<MZCharacter>().characterType == MZCharacterType.EnemyGround )
				return parentGameObject.GetComponent<MZEnemy>().currentMovingVector;

			if( parentGameObject.GetComponent<MZCharacter>().characterType == MZCharacterType.EnemyBullet )
				return parentGameObject.GetComponent<MZEnemyBullet>().currentMovingVector;

			return Vector2.zero;
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

	public MZCollision GetRealCollision(MZCollision origin)
	{
		Vector2 realCenter = _parentGameObject.GetComponent<MZCharacter>().position + this.position + origin.center;
		return new MZCollision( realCenter, origin.radius );
	}

	public bool IsCollide(MZCharacterPart other)
	{
		foreach( MZCollision selfCollision in _collisionsList )
		{
			MZCollision realSelfCollision = GetRealCollision( selfCollision );

			foreach( MZCollision otherCollision in other._collisionsList )
			{
				MZCollision realOtherCollision = other.GetRealCollision( otherCollision );

				if( realSelfCollision.IsCollision( realOtherCollision ) )
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
		Gizmos.color = MZGameSetting.GetCollisionColor( _parentGameObject.GetComponent<MZCharacter>().characterType );

		foreach( MZCollision c in _collisionsList )
		{
			Vector2 realCenter = _parentGameObject.GetComponent<MZCharacter>().position + position + c.center;
			Gizmos.DrawWireSphere( realCenter, c.radius );
		}
	}
}