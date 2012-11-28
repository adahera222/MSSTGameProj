using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacterSettingsCollection
{
	static public MZCharacterSetting GetPlayerType1()
	{
		MZCharacterPartSetting mainBoby = new MZCharacterPartSetting();
		mainBoby.name = "MainBody";
//		mainBoby.frameName = "Donut_normal0001";
//		mainBoby.scale = 0.5f;
		mainBoby.rotation = 90;
//		mainBoby.animationName = "[Celestial]_Army_med2_normal";
		mainBoby.animationName = "Ika_normal";
		mainBoby.animationSpeed = 0.3f;
		mainBoby.collisions.Add( new MZCollision( new Vector2( 0, 0 ), 50 ) );

		MZCharacterSetting characterSetting = new MZCharacterSetting();
		characterSetting.partSettings.Add( mainBoby );

		return characterSetting;
	}

	static public MZCharacterSetting GetPlayerBullet001()
	{
		MZCharacterPartSetting mainBoby = new MZCharacterPartSetting();
		mainBoby.name = "MainBody";
		mainBoby.frameName = "Donut_normal0001";
		mainBoby.scale = 0.3f;
//		partSetting.scaleX = 2.0f;
//		partSetting.scaleY = 0.5f;
		mainBoby.rotation = 90;
		mainBoby.collisions.Add( new MZCollision( new Vector2( 0, 0 ), 25 ) );

		MZCharacterSetting characterSetting = new MZCharacterSetting();
		characterSetting.partSettings.Add( mainBoby );

		return characterSetting;
	}

	static public MZCharacterSetting GetEnemy001()
	{
		MZCharacterPartSetting mainBoby = new MZCharacterPartSetting();
		mainBoby.name = "MainBody";
		mainBoby.scale = 1.0f;
		mainBoby.rotation = 270;
		mainBoby.animationName = "[Celestial]_Army_med3_normal";
		mainBoby.collisions.Add( new MZCollision( new Vector2( 0, 0 ), 90 ) );

		MZCharacterSetting characterSetting = new MZCharacterSetting();
		characterSetting.partSettings.Add( mainBoby );

		return characterSetting;
	}

	static public MZCharacterSetting GetEnemyBullet001()
	{
		MZCharacterPartSetting mainBoby = new MZCharacterPartSetting();
		mainBoby.name = "MainBody";
		mainBoby.scale = 0.5f;
		mainBoby.rotation = 270;
		mainBoby.frameName = "Donut_normal0001";
		mainBoby.collisions.Add( new MZCollision( new Vector2( 0, 0 ), 30 ) );

		MZCharacterSetting characterSetting = new MZCharacterSetting();
		characterSetting.partSettings.Add( mainBoby );

		return characterSetting;
	}
}
