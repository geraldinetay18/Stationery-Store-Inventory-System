package com.example.kaise.adproject_team10.Store;

import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.SimpleAdapter;

import com.example.kaise.adproject_team10.Dept.DeptRepHome;
import com.example.kaise.adproject_team10.Dept.StationeryCollection;
import com.example.kaise.adproject_team10.Entities.BriefEmployee;
import com.example.kaise.adproject_team10.Entities.RetrievalItem;
import com.example.kaise.adproject_team10.LoginActivity;
import com.example.kaise.adproject_team10.R;

import java.util.List;

public class StoreClerkHome extends AppCompatActivity {
    SharedPreferences pref;
    String empId;
    String RetrievalId = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_store_clerk_home);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        empId = pref.getString("EmployeeId", "EmployeeId");

        Button btnRetrieval = findViewById(R.id.btnRetrieval);
        Button btnDisbursement = findViewById(R.id.btnDisbursement);
        Button btnLogout = findViewById(R.id.btnLogout);

        btnRetrieval.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                new AsyncTask<String, Void, List<RetrievalItem>>() {

                    @Override
                    protected List<RetrievalItem> doInBackground(String... strings) {
                        return RetrievalItem.getRetrievalItems(strings[0]);
                    }

                    @Override
                    protected void onPostExecute(List<RetrievalItem> rList) {
                        Intent intent = new Intent(getApplicationContext(), NewRetrievalList.class);
                        if (rList.size() != 0) {
                            RetrievalId = rList.get(0).get("RetrievalId");
                            intent.putExtra("RetrievalId", RetrievalId);
                        }
                        startActivityForResult(intent, 411);
                    }
                }.execute(empId);
            }
        });

        btnDisbursement.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), DisbursementList.class);
                startActivityForResult(intent, 412);
            }
        });

        btnLogout.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                new AlertDialog.Builder(StoreClerkHome.this)
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
}