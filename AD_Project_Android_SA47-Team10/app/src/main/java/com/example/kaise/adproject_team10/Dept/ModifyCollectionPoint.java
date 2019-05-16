package com.example.kaise.adproject_team10.Dept;

import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.TextView;
import android.widget.Toast;

import com.example.kaise.adproject_team10.Entities.BriefEmployee;
import com.example.kaise.adproject_team10.Entities.CollectionPointDetails;
import com.example.kaise.adproject_team10.Entities.DepartmentDetails;
import com.example.kaise.adproject_team10.R;

/*created by Shalin Christina Stephen Selvaraja*/

public class ModifyCollectionPoint extends AppCompatActivity {

    String deptId = "";
    String locationId;
    String storeClerkId;
    String newLocationId = "";
    TextView txtStoreClerk;
    TextView txtTime;
    TextView txtContactNumber;
    TextView txtCurrentLocation;
    TextView txtNewCollection;
    ImageButton list;
    Button update;
    SharedPreferences pref;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_modify_collection_point);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        deptId = pref.getString("DeptId", "DeptId");

        findViewsById();
        show();


        list.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), CollectionPointList.class);
                startActivityForResult(intent, 141);
            }
        });

        update.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                new AlertDialog.Builder(ModifyCollectionPoint.this)
                        .setTitle("Update")
                        .setMessage("Are you sure to update collection point?")
                        .setCancelable(false)
                        .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                update();
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
                        txtTime.setText(collectionPointDetails.get("Time"));
                        txtCurrentLocation.setText(collectionPointDetails.get("LocationName"));
                        storeClerkId = collectionPointDetails.get("EmployeeId");

                        new AsyncTask<String, Void, BriefEmployee>() {
                            @Override
                            protected BriefEmployee doInBackground(String... param) {
                                return BriefEmployee.getBriefEmployee(param[0]);
                            }

                            @Override
                            protected void onPostExecute(BriefEmployee result) {
                                txtStoreClerk.setText(result.get("EmployeeName"));
                                txtContactNumber.setText(result.get("Phone"));
                            }
                        }.execute(storeClerkId);
                    }
                }.execute(locationId);
            }
        }.execute(deptId);
    }

    void update() {
        if (txtNewCollection.getText().toString().isEmpty()) {
            Toast.makeText(getApplicationContext(), "Please select a new location", Toast.LENGTH_SHORT).show();
        } else if (locationId.equalsIgnoreCase(newLocationId)) {
            Toast.makeText(getApplicationContext(), "Selection is same as the existing location", Toast.LENGTH_SHORT).show();
        } else {

            new AsyncTask<String, Void, String>() {
                @Override
                protected String doInBackground(String... params) {
                    String result = DepartmentDetails.updateCollectionPoint(params[0], params[1], params[2]);
                    Log.i("background result", result);
                    return result;
                }

                @Override
                protected void onPostExecute(String result) {
                    String value = result;
                    Log.i("foreground result", value);
                    if (!result.equalsIgnoreCase("1")) {
                        Toast.makeText(getApplicationContext(), "Updated successfully and email is sent to the clerk", Toast.LENGTH_SHORT).show();
                        recreate();
                    } else if (!result.equalsIgnoreCase("2")) {
                        Toast.makeText(getApplicationContext(), "Updated successfully, but email is not sent to the store clerk.. Please check the email", Toast.LENGTH_SHORT).show();
                        recreate();
                    } else {
                        Toast.makeText(getApplicationContext(), "Failed to update", Toast.LENGTH_SHORT).show();
                    }
                }
            }.execute(newLocationId, deptId, locationId);
        }
    }

    void findViewsById() {
        txtStoreClerk = findViewById(R.id.txtStoreClerk);
        txtTime = findViewById(R.id.txtTime);
        txtContactNumber = findViewById(R.id.txtContactNumber);
        txtCurrentLocation = findViewById(R.id.txtCurrentLocation);
        txtNewCollection = findViewById(R.id.txtNewCollection);
        list = findViewById(R.id.btnlist);
        update = findViewById(R.id.btnUpdate);
    }

    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode == RESULT_OK && requestCode == 141) {
            if (data.hasExtra("edited")) {
                int result = data.getExtras().getInt("edited");
                if (result == 1) {
                    newLocationId = data.getStringExtra("LocationId");
                    txtNewCollection.setText(data.getStringExtra("LocationName"));
                    Toast.makeText(getApplicationContext(), "Selected new location", Toast.LENGTH_SHORT).show();
                }
            }
        }
    }
}