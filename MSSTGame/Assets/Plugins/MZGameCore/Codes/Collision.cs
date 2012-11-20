using UnityEngine;
using System.Collections;

namespace MZGameCore
{
	[System.Serializable]
	public class Collision
	{
		public Vector2 center = new Vector2( 0, 0 );
		public float radius = 0;

		public Collision()
		{

		}

		public Collision(Vector2 center, float radius)
		{
			this.center = center;
			this.radius = radius;
		}
	}
}