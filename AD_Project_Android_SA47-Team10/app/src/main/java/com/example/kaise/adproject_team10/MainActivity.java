package com.example.kaise.adproject_team10;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import com.example.kaise.adproject_team10.Dept.DeptHeadHome;
import com.example.kaise.adproject_team10.Dept.DeptRepHome;
import com.example.kaise.adproject_team10.Entities.BriefEmployee;
import com.example.kaise.adproject_team10.Store.StoreClerkHome;

/*created by Lee Kai Seng*/

public class MainActivity extends AppCompatActivity {
    SharedPreferences pref;
    String role;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());

        new AsyncTask<String, Void, BriefEmployee>() {
            @Override
            protected BriefEmployee doInBackground(String... strings) {
                return BriefEmployee.getBriefEmployeeByEmail(strings[0]);
            }

            @Override
            protected void onPostExecute(BriefEmployee bemp) {
                if (bemp != null) {
                    SharedPreferences.Editor editor = pref.edit();
                    editor.putString("EmployeeId", bemp.get("EmployeeId"));
                    editor.putString("DeptId", bemp.get("DeptId"));
                    editor.putString("Role", bemp.get("Role"));
                    editor.commit();

                    role = bemp.get("Role");
                    if (role.equalsIgnoreCase("Department Head")) {
                        startActivity(new Intent(getApplicationContext(), DeptHeadHome.class));
                        finish();
                    } else if (role.equalsIgnoreCase("DeptRep")) {
                        startActivity(new Intent(getApplicationContext(), DeptRepHome.class));
                        finish();
                    } else if (role.equalsIgnoreCase("Store Clerk")) {
                        startActivity(new Intent(getApplicationContext(), StoreClerkHome.class));
                        finish();
                    } else {
                        startActivity(new Intent(getApplicationContext(), ErrorPage.class));
                        finish();
                    }
                } else {
                    BriefEmployee.Logout(pref);
                    startActivity(new Intent(getApplicationContext(), LoginActivity.class));
                    finish();
                }
            }
        }.execute(pref.getString("Email", "Email"));
    }
}