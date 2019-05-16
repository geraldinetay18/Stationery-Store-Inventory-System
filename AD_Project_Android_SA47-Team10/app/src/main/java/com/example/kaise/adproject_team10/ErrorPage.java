package com.example.kaise.adproject_team10;

import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import com.example.kaise.adproject_team10.Entities.BriefEmployee;

/*created by Lee Kai Seng*/

public class ErrorPage extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_error_page);
    }

    @Override
    public void onBackPressed() {
        Logout();
        finish();
    }

    void Logout() {
        BriefEmployee.Logout(PreferenceManager.getDefaultSharedPreferences(getApplicationContext()));
    }

}
