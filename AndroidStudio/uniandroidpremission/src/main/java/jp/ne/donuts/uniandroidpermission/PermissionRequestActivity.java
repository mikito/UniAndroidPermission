package jp.ne.donuts.uniandroidpermission;

import android.app.Activity;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Build;
import android.os.Bundle;

import com.unity3d.player.UnityPlayer;

public class PermissionRequestActivity extends Activity {
    @Override
    public void onCreate(Bundle savedInstanceState) {
        if(Build.VERSION.SDK_INT >= 23) {
            super.onCreate(savedInstanceState);
            Intent intent = getIntent();
            String permissionName = intent.getStringExtra("permissionName");
            requestPermissions(new String[]{permissionName}, 0);
        }else{
            finish();
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) {
        switch (requestCode) {
            case 0: {
                if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    UnityPlayer.UnitySendMessage("UniAndroidPermission", "OnPermit", "");
                } else {
                    UnityPlayer.UnitySendMessage("UniAndroidPermission", "NotPermit", "");
                }
                break;
            }
        }
        finish();
    }
}