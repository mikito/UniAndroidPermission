using UnityEngine;
using System;
using System.Collections;

#if UNITY_ANDROID
public class UniAndroidPermission : MonoBehaviour {
    const string PackageClassName = "jp.ne.donuts.uniandroidpermission.PermissionManager";
    static UniAndroidPermission instance;
    Action permitCallBack;
    Action notPermitCallBack;
    AndroidJavaClass permissionManager;

    void Awake(){
        DontDestroyOnLoad (gameObject);
        permissionManager = new AndroidJavaClass (PackageClassName);
    }

    static UniAndroidPermission Instance {
        get
        {
            if(instance == null)
            {
                var go = new GameObject("UniAndroidPermission");
                instance = go.AddComponent<UniAndroidPermission>();
            }

            return instance;
        }
    }

    string GetPermittionStr(AndroidPermission permission){
        return "android.permission." + permission.ToString ();
    }

    bool IsPermittedInternal(AndroidPermission permission){
        return permissionManager.CallStatic<bool> ("hasPermission", GetPermittionStr(permission));
    }

    void RequestPremissionInternal(AndroidPermission permission, Action onPermit, Action notPermit){
        permissionManager.CallStatic("requestPermission", GetPermittionStr(permission));
        permitCallBack = onPermit;
        notPermitCallBack = notPermit;
    }

    void OnPermit(){
        if (permitCallBack != null) {
            permitCallBack ();
        }
        ResetCallBacks ();
    }

    void NotPermit(){
        if (notPermitCallBack != null) {
            notPermitCallBack ();
        }
        ResetCallBacks ();
    }

    void ResetCallBacks(){
        notPermitCallBack = null;
        permitCallBack = null;
    }

    public static bool IsPermitted(AndroidPermission permission){
#if !UNITY_EDITOR
        return Instance.IsPermittedInternal(permission);
#else
        return true;
#endif
    }

    public static void RequestPremission(AndroidPermission permission, Action onPermit = null, Action notPermit = null){
#if !UNITY_EDITOR
        Instance.RequestPremissionInternal(permission, onPermit, notPermit);
#else
        Debug.LogWarning("UniAndroidPermission works only Android Devices.");
#endif
    }
}

// Protection level: dangerous permissions 2015/11/25
// http://developer.android.com/intl/ja/reference/android/Manifest.permission.html
public enum AndroidPermission{
    ACCESS_COARSE_LOCATION,
    ACCESS_FINE_LOCATION,
    ADD_VOICEMAIL,
    BODY_SENSORS,
    CALL_PHONE,
    CAMERA,
    GET_ACCOUNTS,
    PROCESS_OUTGOING_CALLS,
    READ_CALENDAR,
    READ_CALL_LOG,
    READ_CONTACTS,
    READ_EXTERNAL_STORAGE,
    READ_PHONE_STATE,
    READ_SMS,
    RECEIVE_MMS,
    RECEIVE_SMS,
    RECEIVE_WAP_PUSH,
    RECORD_AUDIO,
    SEND_SMS,
    USE_SIP,
    WRITE_CALENDAR,
    WRITE_CALL_LOG,
    WRITE_CONTACTS,
    WRITE_EXTERNAL_STORAGE
}
#endif
