package com.example.kaise.adproject_team10.Store;

import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import com.example.kaise.adproject_team10.Dept.ManageActHead;
import com.example.kaise.adproject_team10.Entities.BriefEmployee;
import com.example.kaise.adproject_team10.Entities.RetrievalItem;
import com.example.kaise.adproject_team10.LoginActivity;
import com.example.kaise.adproject_team10.MainActivity;
import com.example.kaise.adproject_team10.R;

import java.util.List;

public class NewRetrievalList extends AppCompatActivity {
    ListView list;
    TextView tvMessage;
    SharedPreferences pref;
    String empId;
    String RetrievalId="";

    Animation animation;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_retrieval_list);

        Intent intent = getIntent();
        if (intent.hasExtra("RetrievalId")) {
            RetrievalId = intent.getStringExtra("RetrievalId");
        }

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        empId = pref.getString("EmployeeId", "EmployeeId");

        list = findViewById(R.id.lvRetrieval);
        tvMessage = findViewById(R.id.tvMessage);

        new AsyncTask<String, Void, List<RetrievalItem>>() {

            @Override
            protected List<RetrievalItem> doInBackground(String... strings) {
                return RetrievalItem.getRetrievalItems(strings[0]);
            }

            @Override
            protected void onPostExecute(List<RetrievalItem> rList) {
                if (rList.size() != 0) {
                    tvMessage.setVisibility(View.GONE);
                    RetrievalId = rList.get(0).get("RetrievalId");
                    list.setAdapter(new SimpleAdapter(getApplicationContext(), rList, R.layout.rowretrieval, new String[]{"Description", "Bin", "QuantityInStock", "QuantityNeeded", "QuantityRetrieved"}, new int[]{R.id.tvDescription, R.id.tvBin, R.id.tvStock, R.id.tvRequestedQty, R.id.tvRetrievedQty}));
                    list.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                        @Override
                        public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                            animation = new AlphaAnimation(0.5f, 1.2f);
                            animation.setDuration(3000);
                            RetrievalItem rItem = (RetrievalItem) parent.getAdapter().getItem(position);
                            Intent intent = new Intent(getApplicationContext(), NewRetrievalDetail.class);
                            intent.putExtra("RetrievalDetailsId", rItem.get("RetrievalDetailsId"));
                            intent.putExtra("ItemCode", rItem.get("ItemCode"));
                            intent.putExtra("RetrievalId", rItem.get("RetrievalId"));
                            intent.putExtra("QuantityRetrieved", rItem.get("QuantityRetrieved"));
                            intent.putExtra("QuantityNeeded", rItem.get("QuantityNeeded"));
                            intent.putExtra("Description", rItem.get("Description"));
                            intent.putExtra("QuantityInStock", rItem.get("QuantityInStock"));
                            intent.putExtra("Bin", rItem.get("Bin"));
                            view.startAnimation(animation);
                            startActivityForResult(intent, 121);
                        }
                    });
                } else {
                    tvMessage.setVisibility(View.VISIBLE);
                    tvMessage.setText("*No pending retrieval");
                }
            }
        }.execute(empId);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        if (!RetrievalId.isEmpty()) {
            super.onCreateOptionsMenu(menu);
            getMenuInflater().inflate(R.menu.menusaveretrieval, menu);
            return true;
        } else {
            return false;
        }
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.miConfirmRetrieval:
                new AlertDialog.Builder(NewRetrievalList.this)
                        .setTitle("Confirm")
                        .setMessage("Are you sure to confirm retrieval?")
                        .setCancelable(false)
                        .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                new AsyncTask<String, Void, String>() {

                                    @Override
                                    protected String doInBackground(String... params) {
                                        String result = RetrievalItem.confirmRetrieval(params[0], params[1]);
                                        Log.i("background result", result);
                                        return result;
                                    }

                                    @Override
                                    protected void onPostExecute(String result) {
                                        String subResult = result.substring(0, 1);
                                        Log.i("foreground result", subResult);
                                        if (subResult.equalsIgnoreCase("1")) {
                                            Toast.makeText(getApplicationContext(), "Retrieval Confirmed", Toast.LENGTH_SHORT).show();
                                            recreate();
                                        } else {
                                            Toast.makeText(getApplicationContext(), "Failed. Please try again", Toast.LENGTH_SHORT).show();
                                        }
                                    }
                                }.execute(empId, RetrievalId);
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

    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode == RESULT_OK && requestCode == 121) {
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
