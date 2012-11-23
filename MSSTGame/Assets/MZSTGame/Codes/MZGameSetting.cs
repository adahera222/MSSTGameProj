using UnityEngine;
using System.Collections;

public class MZGameSetting
{
	static public int GetCharacterDepth(MZCharacterFactory.MZCharacterType type)
	{
		switch( type )
		{
			case MZCharacterFactory.MZCharacterType.Player:
				return -50;
		}

		MZDebug.Assert( false, "undefine type: " + type.ToString() );
		return -1;
	}

}
