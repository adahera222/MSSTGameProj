using UnityEngine;
using System.Collections;

public class MZEnemyBullet : MonoBehaviour
{
	public Vector2 movingVector;

	void Start()
	{

	}

	void Update()
	{
		gameObject.GetComponent<MZCharacter>().position += movingVector*300*Time.deltaTime;
	}
}
