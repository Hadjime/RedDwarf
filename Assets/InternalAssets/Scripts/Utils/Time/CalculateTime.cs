using UnityEngine;


namespace InternalAssets.Scripts.Utils.Time
{
	public class CalculateTime
	{
		//TODO из нативного класса SystemClock можно получить время не привяззанное к времени наустройстве, которое игрок может самостоятельно поменять
		public static long ElapsedTime()
        		{
        			if (Application.platform != RuntimePlatform.Android) return 0;
        			AndroidJavaClass systemClock = new AndroidJavaClass("android.os.SystemClock");
        			return systemClock.CallStatic<long>("elapsedRealtime");
        		}
	}
}
