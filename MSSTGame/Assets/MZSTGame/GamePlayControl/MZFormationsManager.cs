using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZFormationsManager : MZControlBase
{
	void SetFormationStates()
	{
		MZFormationState s0 = AddFormationState( "s0" );
		s0.SetProbability( MZFormation.Type.Small, 1 );

		MZFormationState s1 = AddFormationState( "s1" );
		s1.SetProbability( MZFormation.Type.Small, 1 );
		s1.SetProbability( MZFormation.Type.Mid, 2 );

		MZFormationState s3 = AddFormationState( "s3" );
		s3.SetProbability( MZFormation.Type.Large, 3 );
	}

	void SetFormations()
	{
		Formation_S000 sf1 = new Formation_S000();
		sf1.sideType = MZFormation.SideType.Mid;
		AddFormation( MZFormation.Type.Small, sf1 );

		Formation_S000 sf2 = new Formation_S000();
		sf2.sideType = MZFormation.SideType.Left;
		AddFormation( MZFormation.Type.Small, sf2 );

		Formation_S000 sf3 = new Formation_S000();
		sf3.sideType = MZFormation.SideType.Right;
		AddFormation( MZFormation.Type.Small, sf3 );

		AddFormation( MZFormation.Type.Mid, new Formation_M000() );

		AddFormation( MZFormation.Type.Large, new Formation_L000() );
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
		_currentFormationsList = new List<MZFormation>();
	}

	//

	protected override void UpdateWhenActive()
	{
		UpdateFormationState();
		UpdateFormationCreate();
		UpdateCurrentFormations();
	}

	//

	MZFormationState AddFormationState(string name)
	{
		if( _formationStatesList == null )
			_formationStatesList = new List<MZFormationState>();

		MZFormationState state = new MZFormationState( name );
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

		formation.type = type;
		_formationsDictionary[ type ].Add( formation );
	}

	void UpdateFormationState()
	{
		if( _currentFormationState.hasExpToLimited == true )
		{
			_currentFormationStatesIndex++;
			if( _currentFormationStatesIndex >= _formationStatesList.Count )
				_currentFormationStatesIndex = 0;

			_currentFormationState.Reset();

			MZDebug.Log( "Formation State Level Up = " + _currentFormationState.name );
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

			MZFormation newFormation = formationsList[ choiceIndex ];

			if( _currentFormationsList.Count != 0 && _currentFormationsList.Contains( newFormation ) )
				return;

			newFormation.Reset();

			if( newFormation.nextCreatedTime >= _nextCreateTimeCount )
				_nextCreateTimeCount = newFormation.nextCreatedTime;

			if( _currentFormationsList.Contains( newFormation ) == false )
				_currentFormationsList.Add( newFormation );
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

		for( int i = 0; i < _currentFormationsList.Count; i++ )
		{
			if( _currentFormationsList[ i ].isActive == false )
			{
				_currentFormationState.exp += _currentFormationsList[ i ].stateExp;
				_currentFormationsList.RemoveAt( i );
			}
		}

	}
}
