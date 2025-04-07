using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모바일 가상 키보드에 따른 화면 조정 코드
public class Keyboard : MonoBehaviour
{
    void Start()
    {
        NotScreenResizeAndroidSetting();
    }

    /// <Summary>
    //	You do not change the screen size with a soft keyboard displayed at the time of the notification bar display set in the /// Android
    /// </ Summary>
    void NotScreenResizeAndroidSetting()
    {
#if UNITY_ANDROID
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		AndroidJNI.AttachCurrentThread ();
		AndroidJNI.PushLocalFrame (0);
		try
		{
//			Get // Activity
			using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"))
			using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic <AndroidJavaObject> ("currentActivity"))
			{
				// Run on the UI thread
				joActivity.Call ("runOnUiThread", new AndroidJavaRunnable (RunOnUiThread));
			}
		}
		catch (System.Exception ex)
		{
			Debug.LogError (ex.Message);
		}
		finally
		{
			AndroidJNI.PopLocalFrame (System.IntPtr.Zero);
		}
#endif
    }

    /// <Summary>
    /// Run on the UI thread
    /// </ Summary>
    void RunOnUiThread()
    {
#if UNITY_ANDROID
//		Call the following code: // on the Activity of Android
			// GetWindow () setSoftInputMode (WindowManager.LayoutParams.SOFT_INPUT_ADJUST_NOTHING).;
		using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"))
		using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic <AndroidJavaObject> ("currentActivity"))
		using (AndroidJavaObject joWindow = joActivity.Call <AndroidJavaObject> ("getWindow"))
		{
			joWindow.Call ("setSoftInputMode", 48);
		}
#endif
    }
}
