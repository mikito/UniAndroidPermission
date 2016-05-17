package jp.ne.donuts.uniandroidpermission;

import android.app.Activity;
import android.content.Context;
import android.content.pm.PackageManager;
import android.os.Build;
import com.unity3d.player.UnityPlayer;

public class PermissionManager {
    public static void requestPermission(String permissionName) {
        Activity activity = UnityPlayer.currentActivity;
        if (!hasPermission(permissionName)) {
            if(Build.VERSION.SDK_INT >= 23) {
                activity.requestPermissions(new String[]{permissionName}, 0);
            }
        }
    }

    public static boolean hasPermission(String permissionName){
        Activity activity = UnityPlayer.currentActivity;
        if(Build.VERSION.SDK_INT < 23){
            return true;
        }
        Context context = activity.getApplicationContext();
        return  context.checkCallingOrSelfPermission(permissionName) == PackageManager.PERMISSION_GRANTED;
    }
}