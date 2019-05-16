package com.example.kaise.adproject_team10.Store;

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
import android.widget.TextView;
import android.widget.Toast;

import com.example.kaise.adproject_team10.Entities.BriefDisbursement;
import com.example.kaise.adproject_team10.Entities.RetrievalItem;
import com.example.kaise.adproject_team10.R;

import java.util.List;

public class DisbursementList extends AppCompatActivity {
    ListView list;
    TextView tvMessage;
    SharedPreferences pref;
    String empId;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_disbursement_list);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        empId = pref.getString("EmployeeId", "EmployeeId");

        list = findViewById(R.id.lvDisbursement);
        tvMessage = findViewById(R.id.tvMessage);

        new AsyncTask<String, Void, List<BriefDisbursement>>() {

            @Override
            protected List<BriefDisbursement> doInBackground(String... strings) {
                return BriefDisbursement.GetBriefDisbursements(strings[0]);
            }

            @Override
            protected void onPostExecute(List<BriefDisbursement> bDList) {
                if (bDList.size() != 0) {
                    tvMessage.setVisibility(View.GONE);
                    list.setAdapter(new SimpleAdapter(getApplicationContext(), bDList, R.layout.rowdisbursement, new String[]{"DateDisbursed", "Status", "DeptName", "CollectionPointName"}, new int[]{R.id.tvDateDisbursed, R.id.tvStatus, R.id.tvDeptName, R.id.tvCollectionPointName}));
                    list.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                        @Override
                        public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                            BriefDisbursement briefDisbursement = (BriefDisbursement) parent.getAdapter().getItem(position);
                            Intent intent = new Intent(getApplicationContext(), DisbursementDetailsList.class);
                            intent.putExtra("DisbursementId", briefDisbursement.get("DisbursementId"));
                            intent.putExtra("DeptId", briefDisbursement.get("DeptId"));
                            intent.putExtra("DeptName", briefDisbursement.get("DeptName"));
                            intent.putExtra("DateDisbursed", briefDisbursement.get("DateDisbursed"));
                            intent.putExtra("CollectionPointId", briefDisbursement.get("CollectionPointId"));
                            intent.putExtra("CollectionPointName", briefDisbursement.get("CollectionPointName"));
                            intent.putExtra("CollectionPointTime", briefDisbursement.get("CollectionPointTime"));
                            intent.putExtra("Status", briefDisbursement.get("Status"));
                            intent.putExtra("RepresentativeId", briefDisbursement.get("RepresentativeId"));
                            intent.putExtra("RepresentativeName", briefDisbursement.get("RepresentativeName"));
                            intent.putExtra("RepresentativePhone", briefDisbursement.get("RepresentativePhone"));
                            startActivityForResult(intent, 100);
                        }
                    });
                } else {
                    tvMessage.setVisibility(View.VISIBLE);
                    tvMessage.setText("*No disbursement record");
                }
            }
        }.execute(empId);
    }

    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode == RESULT_OK && requestCode == 100) {
            if (data.hasExtra("edited")) {
                int result = data.getExtras().getInt("edited");
                if (result == 1) {
                    this.recreate();
                    Toast.makeText(getApplicationContext(), "Disbursement marked as not collected", Toast.LENGTH_SHORT).show();
                } else if (result == 2) {
                    this.recreate();
                    Toast.makeText(getApplicationContext(), "Disbursement is pending for acknowledgement", Toast.LENGTH_SHORT).show();
                }
            }
        }
    }
}
