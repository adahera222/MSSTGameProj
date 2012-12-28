using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZFormationsManager : MZControlBase
{
	public enum MZFormationType
	{
		Huge,
		Mid,
		Small,
	}
	//
	float nextTimeCount;
//	Dictionary<MZFormationType, List<MZFormation>> _formationsByType = null;
	List<MZFormation> currentFormations = null;
	MZFormation currentFormation = null;

	public MZFormationsManager()
	{
//		_formationsByType = new Dictionary<MZFormationType, List<MZFormation>>();
//
//		foreach( MZFormationType type in System.Enum.GetValues( typeof(MZFormationType) ) )
//		{
//			List<MZFormationType> list = new List<MZFormationType>();
//			_formationsByType.Add( type, list );
//		}
//
//		_formationsByType[ MZFormationType.Mid ].Add( new Formation_Test1() );

		nextTimeCount = 0;

		currentFormations = new List<MZFormation>();
//		currentFormations.Add( new Formation_Test1() );
		currentFormations.Add( new Formation_FromSide() );
		currentFormations.Add( new Formation_EnemyM001() );
	}

	protected override void UpdateWhenActive()
	{
		nextTimeCount += MZTime.deltaTime;

		if( currentFormation == null || nextTimeCount >= 10 )
		{
			int index = MZMath.RandomFromRange( 0, currentFormations.Count - 1 );
			currentFormation = currentFormations[ index ];
			currentFormation.Reset();
			nextTimeCount = 0;
		}

		currentFormation.Update();
	}
}
