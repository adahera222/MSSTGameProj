using UnityEngine;
using System.Collections;

public class DrawResolutionRange : MonoBehaviour
{
	public bool drawSolid = false;
	bool preDrawSolid = false;
	float depth = 0;
	Vector3 padSize = new Vector3( 768, 1024, 10 ); // 3:4
	Vector3 phone3dot5InchSize = new Vector3( 640, 960, 20 ); // 2:3
	Vector3 phone4InchSize = new Vector3( 640, 1136, 30 ); // 9:16

	void Start()
	{
		
	}

	void Update()
	{
		if( drawSolid != preDrawSolid )
		{
			SetDrawSolid( drawSolid );
			preDrawSolid = drawSolid;
		}
	}

	void OnDrawGizmos()
	{
		DrawRangeGizmos( Color.blue, padSize );
		DrawRangeGizmos( Color.green, phone3dot5InchSize );
		DrawRangeGizmos( Color.red, phone4InchSize );
	}

	void DrawRangeGizmos(Color color, Vector3 size)
	{
		Gizmos.color = color;
		Gizmos.DrawWireCube( new Vector3( 0, 0, depth ), size );
	}

	void SetDrawSolid(bool draw)
	{
		foreach( Transform child in gameObject.transform )
		{
			foreach( Transform childOfChild in child.transform )
			{
				childOfChild.GetComponent<MeshRenderer>().enabled = draw;
			}
		}
	}
}
