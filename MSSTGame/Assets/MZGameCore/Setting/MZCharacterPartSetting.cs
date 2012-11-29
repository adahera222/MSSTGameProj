using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacterPartSetting
{
	public string name;
	public Vector2 position;
	public float scale;
	public float scaleX;
	public float scaleY;
	public float rotation;
	public string frameName;
	public string animationName;
	public float animationSpeed;
	public Color color;
	public List<MZCollision> collisions;
	public MZBaseObject.MZShaderType shaderType;

	public MZCharacterPartSetting()
	{
		name = "Part";
		position = Vector2.zero;
		scale = 1;
		scaleX = 1;
		scaleY = 1;
		rotation = 0;
		frameName = "";
		animationName = "";
		animationSpeed = 1.0f;
		color = new Color( 0.5f, 0.5f, 0.5f );
		collisions = new List<MZCollision>();
		shaderType = MZBaseObject.MZShaderType.AlphaBlended;
	}
}