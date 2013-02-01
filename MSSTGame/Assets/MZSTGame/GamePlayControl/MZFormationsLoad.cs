using UnityEngine;
using System.Collections;

using PositionType = MZFormation.PositionType;
using SizeType = MZFormation.SizeType;

public class MZFormationsLoad
{
	static public void SetFormationStates(MZFormationsManager formationsManager)
	{
		MZFormationState s0 = formationsManager.AddFormationState( "s0" );
		s0.SetProbability( MZFormation.SizeType.Small, 1 );
		s0.expLimited = 20;

		MZFormationState s1 = formationsManager.AddFormationState( "s1" );
		s1.SetProbability( MZFormation.SizeType.Small, 8 );
		s1.SetProbability( MZFormation.SizeType.Mid, 2 );
		s1.expLimited = 30;

		MZFormationState s2 = formationsManager.AddFormationState( "s2" );
		s2.SetProbability( MZFormation.SizeType.Small, 5 );
		s2.SetProbability( MZFormation.SizeType.Mid, 5 );
		s2.expLimited = 30;

		MZFormationState s3 = formationsManager.AddFormationState( "s3" );
		s3.SetProbability( MZFormation.SizeType.Small, 2 );
		s3.SetProbability( MZFormation.SizeType.Mid, 8 );
		s3.expLimited = 30;

		MZFormationState s4 = formationsManager.AddFormationState( "s4" );
		s4.SetProbability( MZFormation.SizeType.Large, 10 );
		s4.expLimited = 5;
	}

	static public void SetFormations(MZFormationsManager formationsManager)
	{
		formationsManager.AddFormation( SizeType.Mid, PositionType.Any, new Formation_M_TwinWay() );

		formationsManager.AddFormation( SizeType.Mid, PositionType.Any, new Formation_M_SwastikaMaster() );

		formationsManager.AddFormation( SizeType.Small, PositionType.Left, new Formation_S_Rocket() );
		formationsManager.AddFormation( SizeType.Small, PositionType.Mid, new Formation_S_Rocket() );
		formationsManager.AddFormation( SizeType.Small, PositionType.Right, new Formation_S_Rocket() );

		formationsManager.AddFormation( SizeType.Mid, PositionType.Any, new Formation_M_Split() );

		formationsManager.AddFormation( SizeType.Small, PositionType.Any, new Formation_S_TwinWay() );
		formationsManager.AddFormation( SizeType.Small, PositionType.Any, new Formation_S_Any_Round001() );

		formationsManager.AddFormation( SizeType.Small, PositionType.Left, new Formation_S_Rail() );
		formationsManager.AddFormation( SizeType.Small, PositionType.Mid, new Formation_S_Rail() );
		formationsManager.AddFormation( SizeType.Small, PositionType.Right, new Formation_S_Rail() );

		formationsManager.AddFormation( SizeType.Small, PositionType.Mid, new Formation_S_Squadron() );
		formationsManager.AddFormation( SizeType.Small, PositionType.Left, new Formation_S_Squadron() );
		formationsManager.AddFormation( SizeType.Small, PositionType.Right, new Formation_S_Squadron() );

		formationsManager.AddFormation( SizeType.Small, PositionType.Mid, new TestFormation_S000() );
		formationsManager.AddFormation( SizeType.Small, PositionType.Left, new TestFormation_S000() );
		formationsManager.AddFormation( SizeType.Small, PositionType.Right, new TestFormation_S000() );

		// tracer
//		formationsManager.AddFormation( SizeType.Small, PositionType.Mid, new TestFormation_S001() );
//		formationsManager.AddFormation( SizeType.Small, PositionType.Left, new TestFormation_S001() );
//		formationsManager.AddFormation( SizeType.Small, PositionType.Right, new TestFormation_S001() );

		formationsManager.AddFormation( SizeType.Small, PositionType.Right, new TestFormation_S_XY() );
		formationsManager.AddFormation( SizeType.Small, PositionType.Left, new TestFormation_S_XY() );

		formationsManager.AddFormation( SizeType.Mid, PositionType.Left, new TestFormation_M000() );
		formationsManager.AddFormation( SizeType.Mid, PositionType.Mid, new TestFormation_M000() );
		formationsManager.AddFormation( SizeType.Mid, PositionType.Right, new TestFormation_M000() );

		formationsManager.AddFormation( SizeType.Mid, PositionType.Left, new Formation_M_Waterfall() );
		formationsManager.AddFormation( SizeType.Mid, PositionType.Right, new Formation_M_Waterfall() );

		formationsManager.AddFormation( SizeType.Large, PositionType.Any, new TestFormation_L000() );
	}
}
