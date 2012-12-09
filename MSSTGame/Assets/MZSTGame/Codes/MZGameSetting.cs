using UnityEngine;
using System.Collections;

public class MZGameSetting
{
//	static bool DISABLE_ENEMY_ATTACK = false;

	public static Vector2 PLAYER_MOVABLE_BOUND_CENTER = new Vector2( 0, 0 );
	public static Vector2 PLAYER_MOVABLE_BOUND_SIZE = new Vector2( 640, 850 );
	public static Vector3 PLAYER_MOVABLE_BOUND_V3CENTER = new Vector3( PLAYER_MOVABLE_BOUND_CENTER.x, PLAYER_MOVABLE_BOUND_CENTER.y, 0 );
	public static Vector3 PLAYER_MOVABLE_BOUND_V3SIZE = new Vector3( PLAYER_MOVABLE_BOUND_SIZE.x, PLAYER_MOVABLE_BOUND_SIZE.y, 100 );

	static public Rect GetPlayerMovableBoundRect()
	{
		return new Rect( PLAYER_MOVABLE_BOUND_CENTER.x - PLAYER_MOVABLE_BOUND_SIZE.x/2,
			PLAYER_MOVABLE_BOUND_CENTER.y + PLAYER_MOVABLE_BOUND_SIZE.y/2,
			PLAYER_MOVABLE_BOUND_SIZE.x,
			PLAYER_MOVABLE_BOUND_SIZE.y );
	}

	static public int GetCharacterDepth(MZCharacterType type)
	{
		switch( type )
		{
			case MZCharacterType.Player:
				return -30;

			case MZCharacterType.PlayerBullet:
				return -20;

			case MZCharacterType.EnemyAir:
				return -50;

			case MZCharacterType.EnemyBullet:
				return -100;
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
