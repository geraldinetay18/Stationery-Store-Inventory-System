package com.example.kaise.adproject_team10.Dept;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import com.example.kaise.adproject_team10.Entities.BriefEmployee;
import com.example.kaise.adproject_team10.R;

import java.util.List;

/*created by Lee Kai Seng*/

public class EmployeeList extends AppCompatActivity {
    String Email;
    ListView list;
    SharedPreferences pref;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_employee_list);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        Email = pref.getString("Email", "Email");

        new AsyncTask<String, Void, List<BriefEmployee>>() {

            @Override
            protected List<BriefEmployee> doInBackground(String... strings) {
                return BriefEmployee.getBriefEmployeesUnderSameDeptExclActHead(strings[0]);
            }

            @Override
            protected void onPostExecute(List<BriefEmployee> eList) {
                list = findViewById(R.id.lvEmployee);
                list.setAdapter(new SimpleAdapter(getApplicationContext(), eList, R.layout.rowemployee, new String[]{"EmployeeName"}, new int[]{R.id.tvEmployeeName}));
                list.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                        BriefEmployee emp = (BriefEmployee) parent.getAdapter().getItem(position);
                        Intent intent = new Intent();
                        intent.putExtra("edited", 1);
                        intent.putExtra("EmployeeId", emp.get("EmployeeId"));
                        intent.putExtra("EmployeeName", emp.get("EmployeeName"));
                        setResult(RESULT_OK, intent);
                        finish();
                    }
                });
            }
        }.execute(Email);
    }
}