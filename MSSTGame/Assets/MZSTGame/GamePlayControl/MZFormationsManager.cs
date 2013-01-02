using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZFormationsManager : MZControlBase
{
	void SetFormationStates()
	{
		MZFormationState s000 = AddFormationState();
		s000.SetProbability( MZFormation.Type.Small, 1 );
	}

	void SetFormations()
	{
		AddFormation( MZFormation.Type.Small, new Formation_FromSide() );
		AddFormation( MZFormation.Type.Mid, new Formation_EnemyM001() );
	}

	//

	float _nextCreateTimeCount;
	List<MZFormation> _currentFormationsList;
	Dictionary<MZFormation.Type, List<MZFormation>> _formationsDictionary;
	int _currentFormationStatesIndex;
	List<MZFormationState> _formationStatesList;

	//

	MZFormationState _currentFormationState
	{
		get
		{
			MZDebug.Assert( _formationStatesList != null && _formationStatesList.Count > 0, "_formationStatesList not init" );
			MZDebug.Assert( 0 <= _currentFormationStatesIndex && _currentFormationStatesIndex < _formationStatesList.Count, "index error" );

			return _formationStatesList[ _currentFormationStatesIndex ];
		}
	}

	public MZFormationsManager()
	{
		SetFormationStates();
		SetFormations();

		_nextCreateTimeCount = 0;
		_currentFormationStatesIndex = 0;
		_currentFormationsList = new List<MZFormation>(); // temp
	}

	//

	protected override void UpdateWhenActive()
	{
		UpdateFormationState();
		UpdateFormationCreate();
		UpdateCurrentFormations();
	}

	//

	MZFormationState AddFormationState()
	{
		if( _formationStatesList == null )
			_formationStatesList = new List<MZFormationState>();

		MZFormationState state = new MZFormationState();
		_formationStatesList.Add( state );

		return state;
	}

	void AddFormation(MZFormation.Type type, MZFormation formation)
	{
		if( _formationsDictionary == null )
			_formationsDictionary = new Dictionary<MZFormation.Type, List<MZFormation>>();

		if( _formationsDictionary.ContainsKey( type ) == false )
		{
			_formationsDictionary.Add( type, new List<MZFormation>() );
		}

		_formationsDictionary[ type ].Add( formation );
	}

	void UpdateFormationState()
	{
		if( _currentFormationState.hasSwitchToNext == true )
		{
			_currentFormationStatesIndex++;
			if( _currentFormationStatesIndex >= _formationStatesList.Count )
				_currentFormationStatesIndex = 0;

			_currentFormationState.Reset();
		}
	}

	void UpdateFormationCreate()
	{
		_nextCreateTimeCount -= MZTime.deltaTime;

		if( _nextCreateTimeCount <= 0 )
		{
			MZFormation.Type createType = _currentFormationState.GetNewFormationType();

			MZDebug.Assert( _formationsDictionary.ContainsKey( createType ), "not contain this type: " + createType.ToString() );

			List<MZFormation> formationsList = _formationsDictionary[ createType ];
			int choiceIndex = MZMath.RandomFromRange( 0, formationsList.Count - 1 );

			// test exist ... not yet ... bcuz, less foramtion now.

			MZFormation f = formationsList[ choiceIndex ];
			f.Reset();

			if( f.nextCreatedTime >= _nextCreateTimeCount )
				_nextCreateTimeCount = f.nextCreatedTime;

			if( _currentFormationsList.Contains( f ) == false )
				_currentFormationsList.Add( f );
		}
	}

	void UpdateCurrentFormations()
	{
		if( _currentFormationsList == null )
			return;

		foreach( MZFormation f in _currentFormationsList )
		{
			f.Update();
		}
	}
}
