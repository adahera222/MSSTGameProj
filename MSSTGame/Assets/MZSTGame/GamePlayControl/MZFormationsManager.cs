using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using PositionType = MZFormation.PositionType;
using SizeType = MZFormation.SizeType;

public class MZFormationsManager : MZControlBase
{
	void SetFormationStates()
	{
		MZFormationState s0 = AddFormationState( "s0" );
		s0.SetProbability( MZFormation.SizeType.Small, 1 );

		MZFormationState s1 = AddFormationState( "s1" );
		s1.SetProbability( MZFormation.SizeType.Small, 8 );
		s1.SetProbability( MZFormation.SizeType.Mid, 2 );

		MZFormationState s2 = AddFormationState( "s2" );
		s2.SetProbability( MZFormation.SizeType.Small, 5 );
		s2.SetProbability( MZFormation.SizeType.Mid, 5 );

		MZFormationState s3 = AddFormationState( "s3" );
		s3.SetProbability( MZFormation.SizeType.Small, 2 );
		s3.SetProbability( MZFormation.SizeType.Mid, 8 );

		MZFormationState s4 = AddFormationState( "s4" );
		s4.SetProbability( MZFormation.SizeType.Large, 10 );
	}

	void SetFormations()
	{
		TestAddFormation( PositionType.Mid, SizeType.Small, new Formation_S000() );
		TestAddFormation( PositionType.Left, SizeType.Small, new Formation_S000() );
		TestAddFormation( PositionType.Right, SizeType.Small, new Formation_S000() );

		TestAddFormation( PositionType.Mid, SizeType.Small, new Formation_S001() );
		TestAddFormation( PositionType.Left, SizeType.Small, new Formation_S001() );
		TestAddFormation( PositionType.Right, SizeType.Small, new Formation_S001() );

		TestAddFormation( PositionType.Left, SizeType.Mid, new Formation_M000() );
		TestAddFormation( PositionType.Mid, SizeType.Mid, new Formation_M000() );
		TestAddFormation( PositionType.Right, SizeType.Mid, new Formation_M000() );

		TestAddFormation( PositionType.Left, SizeType.Mid, new Formation_M001() );
		TestAddFormation( PositionType.Mid, SizeType.Mid, new Formation_M001() );
		TestAddFormation( PositionType.Right, SizeType.Mid, new Formation_M001() );

		TestAddFormation( PositionType.Any, SizeType.Large, new Formation_L000() );
	}

	//

	public bool enableUpdateState = true;

	public List<MZFormation> formations
	{
		get
		{
			if( _formationsDictionary == null )
				return null;

			List<MZFormation> formations = new List<MZFormation>();

			foreach( List<MZFormation> list in _formationsDictionary.Values )
			{
				foreach( MZFormation f in list )
					formations.Add( f );
			}

			return formations;
		}
	}

	//

	float _nextCreateTimeCount;
	List<MZFormation> _currentFormationsList;
	Dictionary<MZFormation.SizeType, List<MZFormation>> _formationsDictionary; // old, but on using ...
	Dictionary<MZFormation.PositionType, Dictionary<MZFormation.SizeType, List<MZFormation>>> _formationByPostionDictionary;
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

	public void ExecuteFormation(PositionType posType, SizeType sizeType, string name)
	{
		List<MZFormation> list = _formationsDictionary[ sizeType ];
		foreach( MZFormation f in list )
		{
			if( f.GetType().ToString() == name && posType == f.positionType )
			{
				ExecuteFormation( f );
				break;
			}
		}
	}

	//

	protected override void UpdateWhenActive()
	{
		if( enableUpdateState )
		{
			UpdateFormationState();
			UpdateFormationCreate();
		}

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

	void TestAddFormation(PositionType positionType, SizeType sizeType, MZFormation formation) // delete
	{
		if( _formationsDictionary == null )
			_formationsDictionary = new Dictionary<MZFormation.SizeType, List<MZFormation>>();

		if( _formationsDictionary.ContainsKey( sizeType ) == false )
		{
			_formationsDictionary.Add( sizeType, new List<MZFormation>() );
		}

		formation.sizeType = sizeType;
		formation.positionType = positionType;
		_formationsDictionary[ sizeType ].Add( formation );
	}

	void AddFormation(PositionType positionType, SizeType sizeType, MZFormation formation)
	{
		if( _formationByPostionDictionary == null )
			_formationByPostionDictionary = new Dictionary<PositionType, Dictionary<SizeType, List<MZFormation>>>();

		if( _formationByPostionDictionary.ContainsKey( positionType ) == false )
		{
			Dictionary<SizeType, List<MZFormation>> _formationBySizeDictionary = new Dictionary<SizeType, List<MZFormation>>();
			_formationByPostionDictionary.Add( positionType, _formationBySizeDictionary );
		}

		Dictionary<SizeType, List<MZFormation>> formationBySizeDic = _formationByPostionDictionary[ positionType ];

		if( formationBySizeDic.ContainsKey( sizeType ) == false )
		{
			List<MZFormation> _foramtions = new List<MZFormation>();
			formationBySizeDic.Add( sizeType, _foramtions );
		}

		formation.positionType = positionType;
		formation.sizeType = sizeType;

		formationBySizeDic[ sizeType ].Add( formation );
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
			MZFormation.SizeType createType = _currentFormationState.GetNewFormationType();

			MZDebug.Assert( _formationsDictionary.ContainsKey( createType ), "not contain this type: " + createType.ToString() );

			List<MZFormation> formationsList = _formationsDictionary[ createType ];
			int choiceIndex = MZMath.RandomFromRange( 0, formationsList.Count - 1 );

			MZFormation newFormation = formationsList[ choiceIndex ];

			if( _currentFormationsList.Count != 0 && _currentFormationsList.Contains( newFormation ) )
				return;

			if( newFormation.nextCreatedTime >= _nextCreateTimeCount )
				_nextCreateTimeCount = newFormation.nextCreatedTime;

			ExecuteFormation( newFormation );
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

	void ExecuteFormation(MZFormation formation)
	{
		MZDebug.Assert( formation != null, "formation is null" );

		formation.Reset();

		if( _currentFormationsList == null )
			_currentFormationsList = new List<MZFormation>();

		if( _currentFormationsList.Contains( formation ) == false )
			_currentFormationsList.Add( formation );
	}
}
