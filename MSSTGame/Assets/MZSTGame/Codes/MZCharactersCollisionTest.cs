using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharactersCollisionTest<S, F>
	where S : MZCharacter
	where F : MZCharacter
{
	public delegate void OnCollide(S split ,F full);
	public delegate bool PreTest(S split ,F full);

	//

	public int maxTestPerTime = 100;
	public List<MZCharacter> splitUpdateList;
	public List<MZCharacter> fullUpdateList;
	public OnCollide onCollide;
	public PreTest preTest;

	//

	int start = 0;
	int end = 0;

	public void CollisionTest()
	{
		if( fullUpdateList == null || splitUpdateList == null )
			return;

		int testCount = 0;
		bool doNotUpdateNextRange = false;
		for( int i = start; i < end && testCount < maxTestPerTime; i++, testCount++ )
		{
			if( splitUpdateList.Count == 0 )
				return;

			if( i >= splitUpdateList.Count )
			{
				i = 0;
				start = 0;
				end = maxTestPerTime - testCount;
				doNotUpdateNextRange = true;
			}

			MZCharacter s = splitUpdateList[ i ];
			S _s = (S)s;

			foreach( MZCharacter f in fullUpdateList )
			{
				F _f = (F)f;

				if( preTest( _s, _f ) == false )
					continue;

				if( s.IsCollide( f ) )
				{
					onCollide( _s, _f );
				}
			}
		}

		if( doNotUpdateNextRange == false )
		{
			start = end;
			end = start + maxTestPerTime;
		}
	}
}
