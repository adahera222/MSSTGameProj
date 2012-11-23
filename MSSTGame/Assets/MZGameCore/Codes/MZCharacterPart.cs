using UnityEngine;
using System.Collections;

public class MZCharacterPart : MZBaseObject
{
	public void Init(MZCharacterPartSetting setting)
	{
		name = setting.name;
		animationSpeed = setting.animationSpeed;
		position = setting.position;

		if( setting.animationName != null && setting.animationName.Length > 0 )
			PlayAnimation( setting.animationName );
		else
			SetFrame( setting.frameName );
	}

	void Start()
	{

	}

	void Update()
	{

	}
}
