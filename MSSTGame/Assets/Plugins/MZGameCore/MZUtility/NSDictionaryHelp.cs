using System.Collections;
using System.Collections.Generic;
using PlistCS;

namespace MZUtility
{
	public class NSDictionaryHelp
	{
		static public Dictionary<string,object> Load(string filePath)
		{
			return (Dictionary<string,object>)Plist.readPlist( filePath );
		}

		static public Dictionary<string,object> GetDictionary(string key, Dictionary<string,object> nsDictionary)
		{
			return (Dictionary<string,object>)nsDictionary[ key ];
		}

	}
}