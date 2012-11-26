using UnityEngine;
using System.Collections;

public class MZCharacterPart : MZBaseObject
{
	public void Init(MZCharacterPartSetting setting)
	{
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
	}

//	void Start()
//	{
//
//	}
//
//	void Update()
//	{
//
//	}
}
