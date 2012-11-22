using UnityEngine;
using System.Collections;

public class MZCharacterPart : MZBaseObject
{
	public void Init(MZCharacterPartSetting setting)
	{
		name = setting.name;
		animationSpeed = setting.animationSpeed;
		position = setting.position;
		PlayAnimation( setting.animationName );
	}

	void Start()
	{

	}

	void Update()
	{

	}
}
