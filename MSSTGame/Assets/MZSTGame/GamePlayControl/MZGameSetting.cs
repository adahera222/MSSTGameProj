using UnityEngine;
using System.Collections;

using MZCharacterType = MZCharacter.MZCharacterType;

public class MZGameSetting
{
//	static bool DISABLE_ENEMY_ATTACK = false;

	static public bool SHOW_GIZMOS = false;
	static public bool SHOW_COLLISION_RANGE = true;
	static public bool SHOW_BULLET_ON_COLLISION_CHECK = false;
	static public bool SHOW_CHARACTERS_INFO = false;
	static public bool SHOW_FORMATION_LOG = false;
	static public bool DISABLE_BULLET_EFFECT = false;
	static public Vector2 INVALID_POSITIONV2 = new Vector2( -999, -999 );// new Vector2( 0, 330 ); //Vector2.zero;// new Vector2( -9999, -9999 );
	static public Vector2 PLAYER_MOVABLE_BOUND_CENTER = new Vector2( 0, 0 );
	static public Vector2 PLAYER_MOVABLE_BOUND_SIZE = new Vector2( 640, 850 );
	static public Vector3 PLAYER_MOVABLE_BOUND_V3CENTER = new Vector3( PLAYER_MOVABLE_BOUND_CENTER.x, PLAYER_MOVABLE_BOUND_CENTER.y, 0 );
	static public Vector3 PLAYER_MOVABLE_BOUND_V3SIZE = new Vector3( PLAYER_MOVABLE_BOUND_SIZE.x, PLAYER_MOVABLE_BOUND_SIZE.y, 100 );

	static public Vector2 PLAYER_MOVABLE_BOUND_DOWNLEFT = -PLAYER_MOVABLE_BOUND_SIZE/2;
	static public Vector2 PLAYER_MOVABLE_BOUND_TOPRIGHT = PLAYER_MOVABLE_BOUND_SIZE/2;

	static public float ENEMY_BOUNDLE_TOP = PLAYER_MOVABLE_BOUND_SIZE.y/2;
	static public float ENEMY_BOUNDLE_DOWN = -PLAYER_MOVABLE_BOUND_SIZE.y/2;
	static public float ENEMY_BOUNDLE_LEFT = -PLAYER_MOVABLE_BOUND_SIZE.x/2;
	static public float ENEMY_BOUNDLE_RIGHT = PLAYER_MOVABLE_BOUND_SIZE.x/2;
	static public float ENEMY_BOUNDLE_WIDTH = PLAYER_MOVABLE_BOUND_SIZE.x;
	static public float ENEMY_BOUNDLE_HEIGHT = PLAYER_MOVABLE_BOUND_SIZE.y;

	static public Rect GetPlayerMovableBoundRect()
	{
		return new Rect( PLAYER_MOVABLE_BOUND_CENTER.x - PLAYER_MOVABLE_BOUND_SIZE.x/2,
			PLAYER_MOVABLE_BOUND_CENTER.y + PLAYER_MOVABLE_BOUND_SIZE.y/2,
			PLAYER_MOVABLE_BOUND_SIZE.x,
			PLAYER_MOVABLE_BOUND_SIZE.y );
	}

	static public int GetDepthOfCharacter(MZCharacterType type)
	{
		switch( type )
		{
			case MZCharacterType.EnemyAir:
				return -200;

			case MZCharacterType.Player:
				return -400;

			case MZCharacterType.PlayerBullet:
				return -600;

			case MZCharacterType.EnemyBullet:
				return -800;
		}

		MZDebug.Assert( false, "undefine type: " + type.ToString() );
		return -1;
	}

	static public Color GetCollisionColor(MZCharacterType type)
	{
		switch( type )
		{
			case MZCharacterType.Player:
				return Color.red;

			case MZCharacterType.PlayerBullet:
				return Color.cyan;

			case MZCharacterType.EnemyAir:
			case MZCharacterType.EnemyGround:
				return Color.green;

			case MZCharacterType.EnemyBullet:
				return Color.blue;

			default:
				return Color.white;
		}
	}
}
