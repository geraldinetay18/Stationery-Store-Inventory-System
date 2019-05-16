package com.example.kaise.adproject_team10.Dept;

import android.content.SharedPreferences;
import android.graphics.Color;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONObject;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import com.example.kaise.adproject_team10.R;
import com.example.kaise.adproject_team10.Entities.*;
import com.example.kaise.adproject_team10.Store.DisbursementDetailsList;

/*created by Nguyen Ngoc Doan Trang*/

public class StationeryCollection03 extends AppCompatActivity {
    String disbursementId;
    String deptId;
    String locationId;
    String storeClerkId;
    String year;
    SharedPreferences pref;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_stationery_collection_03);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        deptId = pref.getString("DeptId", "DeptId");

        Intent intent = getIntent();
        disbursementId = intent.getStringExtra("DisbursementId");
        year = intent.getStringExtra("Year");


        new AsyncTask<String, Void, Disbursement>() {

            @SuppressLint("LongLogTag")
            @Override
            protected Disbursement doInBackground(String... params) {
                return Disbursement.getDisbursement(params[0]);
            }

            @Override
            protected void onPostExecute(Disbursement disbursement) {

                Date currentDate = Calendar.getInstance().getTime();
                SimpleDateFormat df = new SimpleDateFormat("dd MMM yyyy");
                String formattedDate = df.format(currentDate);

                TextView textViewDate = findViewById(R.id.textViewDate);
                textViewDate.setText(disbursement.getDateOfDisbursement());

                TextView textViewCollectionPoint = findViewById(R.id.textViewCollectionPoint);
                textViewCollectionPoint.setText(disbursement.getCollectionPoint());

                TextView textViewStatus = findViewById(R.id.textViewStatus);
                textViewStatus.setText(disbursement.getStatus());
            }
        }.execute(disbursementId);

        new AsyncTask<String, Void, List<Item>>() {

            @SuppressLint("LongLogTag")
            @Override
            protected List<Item> doInBackground(String... params) {
                return Item.getItems(params[0]);
            }

            @Override
            protected void onPostExecute(List<Item> items) {
                TableLayout table = findViewById(R.id.tableLayout2);
                addTableRow(table, items);
            }
        }.execute(disbursementId);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        Intent intent = getIntent();
        String status = intent.getStringExtra("Status");
        if (status.trim().equalsIgnoreCase("Waiting for Acknowledgement")) {
            MenuInflater inflater = getMenuInflater();
            inflater.inflate(R.menu.menu, menu);
            return super.onCreateOptionsMenu(menu);
        } else {
            MenuInflater inflater = getMenuInflater();
            inflater.inflate(R.menu.menu2, menu);
            return super.onCreateOptionsMenu(menu);
        }
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle item selection
        switch (item.getItemId()) {
            case R.id.acknowledge:
                acknowledge();
                return true;
            case R.id.call:
                new android.support.v7.app.AlertDialog.Builder(StationeryCollection03.this)
                        .setTitle("Calling")
                        .setMessage("Are you sure to call Store Clerk?")
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
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    private void call() {
        new AsyncTask<String, Void, DepartmentDetails>() {
            @Override
            protected DepartmentDetails doInBackground(String... param) {
                return DepartmentDetails.getDepartmentById(param[0]);
            }

            @Override
            protected void onPostExecute(DepartmentDetails departmentDetails) {
                locationId = departmentDetails.get("LocationId");

                new AsyncTask<String, Void, CollectionPointDetails>() {
                    @Override
                    protected CollectionPointDetails doInBackground(String... param) {
                        return CollectionPointDetails.getCollectionPointById(param[0]);
                    }

                    @Override
                    protected void onPostExecute(CollectionPointDetails collectionPointDetails) {
                        storeClerkId = collectionPointDetails.get("EmployeeId");

                        new AsyncTask<String, Void, BriefEmployee>() {
                            @Override
                            protected BriefEmployee doInBackground(String... param) {
                                return BriefEmployee.getBriefEmployee(param[0]);
                            }

                            @Override
                            protected void onPostExecute(BriefEmployee result) {
                                Intent intent = new Intent(Intent.ACTION_DIAL, Uri.parse("tel:" + result.get("Phone")));
                                startActivity(intent);
                            }
                        }.execute(storeClerkId);
                    }
                }.execute(locationId);
            }
        }.execute(deptId);
    }

    private void acknowledge() {
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this);
        builder1.setMessage("Once delivery is acknowledged, it shall deemed to be final.");
        builder1.setCancelable(true);
        builder1.setPositiveButton(
                "Yes",
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        new AsyncTask<String, Void, Integer>() {
                            @SuppressLint("LongLogTag")
                            @Override
                            protected Integer doInBackground(String... params) {
                                return Disbursement.acknowledge(params[0]);
                            }

                            @SuppressLint("LongLogTag")
                            @Override
                            protected void onPostExecute(Integer result) {
                                if (result == 1) {
                                    Intent intent2 = new Intent();
                                    intent2.putExtra("edited", 1);
                                    intent2.putExtra("year", year);
                                    setResult(RESULT_OK, intent2);
                                    finish();
                                } else {
                                    Toast.makeText(getApplicationContext(), "Failed. Please try again", Toast.LENGTH_SHORT).show();
                                }
                            }
                        }.execute(disbursementId);
                    }
                });
        builder1.setNegativeButton(
                "No",
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });
        AlertDialog alert11 = builder1.create();
        alert11.show();
    }

    private void addTableRow(TableLayout table, List<Item> items) {
        for (int i = 0; i < items.size(); i++) {

            TableRow row = new TableRow(this);
            TableRow.LayoutParams lp = new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT);
            row.setLayoutParams(lp);

            TextView textView1 = new TextView(this);
            textView1.setText(items.get(i).getItemName());
            textView1.setPadding(0, 8, 16, 8);
            textView1.setTextColor(Color.parseColor("#000000"));
            row.addView(textView1);

            TextView textView2 = new TextView(this);
            textView2.setText(items.get(i).getQtyNeed());
            textView2.setTextColor(Color.parseColor("#000000"));
            row.addView(textView2);

            TextView textView3 = new TextView(this);
            textView3.setText(items.get(i).getQtyDisbursed());
            textView3.setTextColor(Color.parseColor("#000000"));
            row.addView(textView3);

            TextView textView5 = new TextView(this);
            textView5.setText(items.get(i).getRequestDate());
            textView5.setTextColor(Color.parseColor("#000000"));
            row.addView(textView5);

            table.addView(row, i + 1);
        }
    }
}