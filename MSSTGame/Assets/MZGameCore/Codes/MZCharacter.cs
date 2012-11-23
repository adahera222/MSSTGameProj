using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharacter : MonoBehaviour
{
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
//		gameObject.transform.position = gameObject.transform.position + new Vector3( 100*Time.deltaTime, 100*Time.deltaTime, 0 );

//		gameObject.transform.Translate( Time.deltaTime, 0, 0 );
	}
}