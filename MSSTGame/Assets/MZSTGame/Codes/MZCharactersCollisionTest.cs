using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MZCharactersCollisionTest
{
	public delegate void OnCollideHandler(MZCharacter c1 ,MZCharacter c2);
	//
	public int maxTestPerTime = 100;
	public List<MZCharacter> splitUpdateList;
	public List<MZCharacter> fullUpdateList;
	public OnCollideHandler onCollideHandler;
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

			foreach( MZCharacter f in fullUpdateList )
			{
				if( s.IsCollide( f ) )
				{
					onCollideHandler( s, f );
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
