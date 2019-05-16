package com.example.kaise.adproject_team10.Dept;

import android.content.DialogInterface;
import android.content.Intent;
import android.preference.PreferenceManager;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;

import com.example.kaise.adproject_team10.Entities.BriefEmployee;
import com.example.kaise.adproject_team10.LoginActivity;
import com.example.kaise.adproject_team10.MainActivity;
import com.example.kaise.adproject_team10.R;

/*created by Lee Kai Seng*/

public class DeptHeadHome extends AppCompatActivity {
    Button btnManageActHead;
    Button btnManageCollectionPoint;
    Button btnLogout;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_dept_head_home);

        findViewsById();

        btnManageActHead.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), ManageActHead.class);
                startActivityForResult(intent, 211);
            }
        });

        btnManageCollectionPoint.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), ModifyCollectionPoint.class);
                startActivityForResult(intent, 212);
            }
        });

        btnLogout.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                new AlertDialog.Builder(DeptHeadHome.this)
                        .setTitle("Logout")
                        .setMessage("Are you sure to logout?")
                        .setCancelable(false)
                        .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                Logout();
                                startActivity(new Intent(getApplicationContext(), LoginActivity.class));
                                finish();
                            }
                        })
                        .setNegativeButton(android.R.string.no, new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                            }
                        })
                        .show();
            }
        });
    }

    @Override
    public void onBackPressed() {
        Logout();
        finish();
    }

    void Logout() {
        BriefEmployee.Logout(PreferenceManager.getDefaultSharedPreferences(getApplicationContext()));
    }

    void findViewsById() {
        btnManageActHead = findViewById(R.id.btnDelegateHead);
        btnManageCollectionPoint = findViewById(R.id.btnManageCollectionPoint);
        btnLogout = findViewById(R.id.btnLogout);
    }

}
