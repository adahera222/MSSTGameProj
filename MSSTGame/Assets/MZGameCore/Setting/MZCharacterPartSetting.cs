using UnityEngine;
using System.Collections;

public class MZCharacterPartSetting
{
	public string name;
	public Vector2 position;
	public string frameName;
	public string animationName;
	public float animationSpeed;

	public MZCharacterPartSetting()
	{
		name = "Part";
		position = Vector2.zero;
		frameName = "";
		animationName = "";
		animationSpeed = 1.0f;
	}
}