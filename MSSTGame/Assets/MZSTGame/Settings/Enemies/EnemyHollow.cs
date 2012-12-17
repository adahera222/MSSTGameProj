using UnityEngine;
using System.Collections;

public class EnemyHollow : MZEnemy
{
	protected override void Update()
	{
		base.Update();

		position += new Vector2( 0, -20*Time.deltaTime );
	}
}
