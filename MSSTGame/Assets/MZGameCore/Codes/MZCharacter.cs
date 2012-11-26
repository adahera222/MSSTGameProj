using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacter : MonoBehaviour
{
	public Vector2 position
	{
		set{ gameObject.transform.position = new Vector3( value.x, value.y, gameObject.transform.position.z ); }
		get{ return new Vector2( gameObject.transform.position.x, gameObject.transform.position.y ); }
	}

	public int AddPart(MZCharacterPartSetting setting)
	{
		GameObject part = MZResources.InstantiateMZGameCoreObject( "MZCharacterPart" );
		part.transform.parent = gameObject.transform;

		MZCharacterPart partBehaviour = part.GetComponent<MZCharacterPart>();
		partBehaviour.Init( setting );

		if( partsList == null )
			partsList = new List<MZCharacterPart>();

		partsList.Add( partBehaviour );

		return partsList.Count;
	}

	List<MZCharacterPart> partsList;

	void Start()
	{

	}

	void Update()
	{

	}
}