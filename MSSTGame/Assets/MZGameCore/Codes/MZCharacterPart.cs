using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MZCharacterType = MZCharacterFactory.MZCharacterType;

public class MZCharacterPart : MZBaseObject
{
	public void Init(MZCharacterPartSetting setting, GameObject parentGameObject)
	{
		_parentGameObject = parentGameObject;

		if( setting.animationName != null && setting.animationName.Length > 0 )
			PlayAnimation( setting.animationName );
		else
			SetFrame( setting.frameName );
		
		name = setting.name;
		animationSpeed = setting.animationSpeed;
		position = setting.position;
		rotation = setting.rotation;

		if( setting.scaleX != 1 || setting.scaleY != 1 )
		{
			scaleX = setting.scaleX;
			scaleY = setting.scaleY;
		}
		else
		{
			scale = setting.scale;
		}

		_collisionsList = new List<MZCollision>();
		_collisionsList.Add( new MZCollision( new Vector2( 0, 0 ), 100 ) );
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

	List<MZCollision> _collisionsList;
	GameObject _parentGameObject;

	void OnDrawGizmos()
	{
		Gizmos.color = MZCharacterFactory.GetCollisionColor( _parentGameObject.GetComponent<MZCharacter>().characterType );

		foreach( MZCollision c in _collisionsList )
		{
			Vector2 realCenter = _parentGameObject.GetComponent<MZCharacter>().position + position + c.center;
			Gizmos.DrawWireSphere( realCenter, c.radius );
		}
	}
}