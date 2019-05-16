package com.example.kaise.adproject_team10.Dept;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.kaise.adproject_team10.DisbursementAdapter;
import com.example.kaise.adproject_team10.Entities.Disbursement;
import com.example.kaise.adproject_team10.R;

import java.util.List;

/*created by Nguyen Ngoc Doan Trang*/

public class StationeryCollection02 extends AppCompatActivity {

    String deptId;
    String year;
    SharedPreferences pref;
    ListView list;
    TextView tvMessage;

    @SuppressLint("LongLogTag")
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_stationery_collection_02);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        deptId = pref.getString("DeptId", "DeptId");

        Intent intent = getIntent();
        year = intent.getStringExtra("year");

        list = findViewById(R.id.disbursementListView);
        tvMessage = findViewById(R.id.tvMessage);

        new AsyncTask<String, Void, List<Disbursement>>() {

            @Override
            protected List<Disbursement> doInBackground(String... params) {
                return Disbursement.getDisbursements(params[0], params[1]);
            }

            @Override
            protected void onPostExecute(List<Disbursement> disbursements) {
                if (disbursements.size() != 0) {
                    tvMessage.setVisibility(View.GONE);
                    DisbursementAdapter adapter = new DisbursementAdapter(getApplicationContext(), disbursements);
                    list.setAdapter(adapter);
                    list.setOnItemClickListener(new AdapterView.OnItemClickListener() {

                        @Override
                        public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                            Disbursement selected = (Disbursement) parent.getAdapter().getItem(position);
                            Intent intent = new Intent(getApplicationContext(), StationeryCollection03.class);
                            intent.putExtra("DisbursementId", selected.get("DisbursementId"));
                            intent.putExtra("Status", selected.get("Status"));
                            intent.putExtra("Year", year);
                            startActivityForResult(intent, 151);
                        }
                    });
                } else {
                    tvMessage.setVisibility(View.VISIBLE);
                    tvMessage.setText("*No collection record");
                }
            }
        }.execute(year, deptId);
    }

    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode == RESULT_OK && requestCode == 151) {
            if (data.hasExtra("edited")) {
                int result = data.getExtras().getInt("edited");
                if (result == 1) {
                    this.recreate();
                    Toast.makeText(getApplicationContext(), "Acknowledged", Toast.LENGTH_SHORT).show();
                }
            }
        }
    }
}