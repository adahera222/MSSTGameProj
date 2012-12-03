using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacterPart : MZBaseObject
{
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

	override protected void Update()
	{
		base.Update();
	}

	List<MZCollision> _collisionsList = new List<MZCollision>();
	GameObject _parentGameObject = null;

	void OnDrawGizmos()
	{
		Gizmos.color = MZGameSetting.GetCollisionColor( _parentGameObject.GetComponent<MZCharacter>().characterType );

		foreach( MZCollision c in _collisionsList )
		{
			Vector2 realCenter = _parentGameObject.GetComponent<MZCharacter>().position + position + c.center;
			Gizmos.DrawWireSphere( realCenter, c.radius );
		}
	}
}