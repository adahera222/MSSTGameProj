using UnityEngine;
using System.Collections;

public class MZPlayerBullet : MZCharacter
{
	protected override void Start()
	{
		base.Start();
		enableRemoveTime = 1.0f;
	}

	protected override void Update()
	{
		base.Update();

		gameObject.GetComponent<MZCharacter>().position += new Vector2( 0, 800*Time.deltaTime );
	}
}
