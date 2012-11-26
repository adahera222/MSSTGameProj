using UnityEngine;
using System.Collections;

public class SetCameraByResolution : MonoBehaviour
{
	public float cameraFovOfDefault = 59.62f;
	public float cameraFovOfIPhone5 = 59.62f;
	public float cameraFovOfIPad = 54.2f;

	void Start()
	{
		Camera.mainCamera.transform.position = new Vector3( 0, 0, -100 );
		Camera.mainCamera.fieldOfView = GetCameraFovFromIPhoneGeneration( iPhone.generation );
		Camera.mainCamera.orthographicSize = GetOrthographicSizeFromIPhoneModelName( SystemInfo.deviceModel );
	}

	float GetCameraFovFromIPhoneGeneration(iPhoneGeneration generation)
	{
		switch( generation )
		{
			case iPhoneGeneration.iPhone5:
			case iPhoneGeneration.iPodTouch5Gen:
				return cameraFovOfIPhone5;

			case iPhoneGeneration.iPad1Gen:
			case iPhoneGeneration.iPad2Gen:
			case iPhoneGeneration.iPad3Gen:
				return cameraFovOfIPad;

			default:
				return Camera.mainCamera.fieldOfView;
		}
	}

	float GetOrthographicSizeFromIPhoneModelName(string deviceModelName)
	{
		switch( deviceModelName )
		{
			case "iPad":
				MZDebug.Log( "iPad: set size=512.1289f" );
				return 512.1289f;

			case "iPhone":
			case "iPod":
				MZDebug.Log( "iPhone: set size=568.0076f" );
				return 568.0076f;
			
			default:
				MZDebug.Log( "Editor: size=" + Camera.mainCamera.orthographicSize.ToString() );
				return Camera.mainCamera.orthographicSize;
				
		}
	}
}
