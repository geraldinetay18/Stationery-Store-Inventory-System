package com.example.kaise.adproject_team10.Store;

import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import com.example.kaise.adproject_team10.Dept.ManageActHead;
import com.example.kaise.adproject_team10.Entities.BriefDisbursement;
import com.example.kaise.adproject_team10.Entities.BriefDisbursementDetails;
import com.example.kaise.adproject_team10.R;

import java.util.List;

public class DisbursementDetailsList extends AppCompatActivity {
    String empId;
    String DisbursementId;
    String DeptId;
    String DeptName;
    String DateDisbursed;
    String CollectionPointId;
    String CollectionPointName;
    String CollectionPointTime;
    String Status;
    String RepresentativeId;
    String RepresentativeName;
    String RepresentativePhone;
    TextView tvDateDisbursed;
    TextView tvStatus;
    TextView tvDeptName;
    TextView tvCollectionPointName;
    TextView tvDeptRep;
    TextView tvTime;
    TextView tvMessage;
    ListView lvDisbursementDetails;
    ImageButton btnCall;
    SharedPreferences pref;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_disbursement_details_list);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        empId = pref.getString("EmployeeId", "EmployeeId");

        Intent intent = getIntent();
        if (intent.hasExtra("DisbursementId")) {
            DisbursementId = intent.getStringExtra("DisbursementId");
            DeptId = intent.getStringExtra("DeptId");
            DeptName = intent.getStringExtra("DeptName");
            DateDisbursed = intent.getStringExtra("DateDisbursed");
            CollectionPointId = intent.getStringExtra("CollectionPointId");
            CollectionPointName = intent.getStringExtra("CollectionPointName");
            CollectionPointTime = intent.getStringExtra("CollectionPointTime");
            Status = intent.getStringExtra("Status");
            RepresentativeId = intent.getStringExtra("RepresentativeId");
            RepresentativeName = intent.getStringExtra("RepresentativeName");
            RepresentativePhone = intent.getStringExtra("RepresentativePhone");
        }

        findViewsById();
        show();
    }

    void findViewsById() {
        tvDateDisbursed = findViewById(R.id.tvDateDisbursed);
        tvStatus = findViewById(R.id.tvStatus);
        tvDeptName = findViewById(R.id.tvDeptName);
        tvCollectionPointName = findViewById(R.id.tvCollectionPointName);
        tvDeptRep = findViewById(R.id.tvDeptRep);
        tvTime = findViewById(R.id.tvTime);
        tvMessage = findViewById(R.id.tvMessage);
        lvDisbursementDetails = findViewById(R.id.lvDisbursementDetails);
        btnCall = findViewById(R.id.btnCall);
        btnCall.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                new AlertDialog.Builder(DisbursementDetailsList.this)
                        .setTitle("Calling")
                        .setMessage("Are you sure to call Department Representative?")
                        .setCancelable(false)
                        .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                call();
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

    void show() {
        tvDateDisbursed.setText(DateDisbursed);
        tvStatus.setText(Status);
        tvDeptName.setText(DeptName);
        tvCollectionPointName.setText(CollectionPointName);
        tvDeptRep.setText(RepresentativeName);
        tvTime.setText(CollectionPointTime);

        new AsyncTask<String, Void, List<BriefDisbursementDetails>>() {

            @Override
            protected List<BriefDisbursementDetails> doInBackground(String... strings) {
                return BriefDisbursementDetails.getBriefDisbursementDetails(strings[0]);
            }

            @Override
            protected void onPostExecute(List<BriefDisbursementDetails> bDDList) {
                if (bDDList.size() != 0) {
                    tvMessage.setVisibility(View.GONE);
                    lvDisbursementDetails.setAdapter(new SimpleAdapter(getApplicationContext(), bDDList, R.layout.rowdisbursementdetails, new String[]{"ItemDescription", "QuantityRequested", "QuantityReceived"}, new int[]{R.id.tvItemDescription, R.id.tvQtyRequested, R.id.tvQtyReceived}));
                    lvDisbursementDetails.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                        @Override
                        public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                            if (Status.equalsIgnoreCase("Allocated")) {
                                BriefDisbursementDetails briefDisbursementDetails = (BriefDisbursementDetails) parent.getAdapter().getItem(position);
                                Intent intent = new Intent(getApplicationContext(), DisbursementDetails.class);
                                intent.putExtra("DisbursementDetailsId", briefDisbursementDetails.get("DisbursementDetailsId"));
                                intent.putExtra("ItemCode", briefDisbursementDetails.get("ItemCode"));
                                intent.putExtra("ItemDescription", briefDisbursementDetails.get("ItemDescription"));
                                intent.putExtra("QuantityRequested", briefDisbursementDetails.get("QuantityRequested"));
                                intent.putExtra("QuantityReceived", briefDisbursementDetails.get("QuantityReceived"));
                                startActivityForResult(intent, 111);
                            } else {
                                Toast.makeText(getApplicationContext(), "Item disbursed", Toast.LENGTH_SHORT).show();
                            }
                        }
                    });
                } else {
                    tvMessage.setVisibility(View.VISIBLE);
                    tvMessage.setText("No record of disbursement detail");
                }
            }
        }.execute(DisbursementId);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        if (Status.equalsIgnoreCase("Allocated")) {
            super.onCreateOptionsMenu(menu);
            getMenuInflater().inflate(R.menu.menusavedisbursement, menu);
            return true;
        } else {
            return false;
        }
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.miMarkAsNotCollected:
                new AlertDialog.Builder(DisbursementDetailsList.this)
                        .setTitle("Not Collected")
                        .setMessage("Are you sure to mark this disbursement as not collected?")
                        .setCancelable(false)
                        .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                new AsyncTask<String, Void, String>() {

                                    @Override
                                    protected String doInBackground(String... params) {
                                        String result = BriefDisbursement.MarkAsNotCollected(params[0], params[1]);
                                        Log.i("background result", result);
                                        return result;
                                    }

                                    @Override
                                    protected void onPostExecute(String result) {
                                        String subResult = result.substring(0, 1);
                                        Log.i("foreground result", subResult);
                                        if (subResult.equalsIgnoreCase("1")) {
                                            Intent intent = new Intent();
                                            intent.putExtra("edited", 1);
                                            setResult(RESULT_OK, intent);
                                            finish();
                                        } else {
                                            Toast.makeText(getApplicationContext(), "Failed. Please try again", Toast.LENGTH_SHORT).show();
                                        }
                                    }
                                }.execute(empId, DisbursementId);
                            }
                        })
                        .setNegativeButton(android.R.string.no, new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                            }
                        })
                        .show();
                return true;
            case R.id.miRequestForAcknowledgement:
                new AlertDialog.Builder(DisbursementDetailsList.this)
                        .setTitle("Acknowledgement")
                        .setMessage("Are you sure to request for acknowledgement?")
                        .setCancelable(false)
                        .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                new AsyncTask<String, Void, String>() {

                                    @Override
                                    protected String doInBackground(String... params) {
                                        String result = BriefDisbursement.RequestForAcknowledgement(params[0], params[1]);
                                        Log.i("background result", result);
                                        return result;
                                    }

                                    @Override
                                    protected void onPostExecute(String result) {
                                        String subResult = result.substring(0, 1);
                                        Log.i("foreground result", subResult);
                                        if (subResult.equalsIgnoreCase("1")) {
                                            Intent intent = new Intent();
                                            intent.putExtra("edited", 2);
                                            setResult(RESULT_OK, intent);
                                            finish();
                                        } else {
                                            Toast.makeText(getApplicationContext(), "Failed. Please try again", Toast.LENGTH_SHORT).show();
                                        }
                                    }
                                }.execute(empId, DisbursementId);
                            }
                        })
                        .setNegativeButton(android.R.string.no, new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                            }
                        })
                        .show();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    void call() {
        Uri uri = Uri.parse(String.format("tel:%s", RepresentativePhone));
        Intent intent = new Intent(Intent.ACTION_DIAL, uri);
        startActivity(intent);
    }

    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode == RESULT_OK && requestCode == 111) {
            if (data.hasExtra("edited")) {
                int result = data.getExtras().getInt("edited");
                if (result == 1) {
                    this.recreate();
                    Toast.makeText(getApplicationContext(), "saved", Toast.LENGTH_SHORT).show();
                }
            }
        }
    }
}
