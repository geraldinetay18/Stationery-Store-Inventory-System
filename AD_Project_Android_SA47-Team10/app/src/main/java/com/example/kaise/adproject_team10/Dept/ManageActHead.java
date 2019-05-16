package com.example.kaise.adproject_team10.Dept;

import android.app.DatePickerDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.InputType;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.TextView;
import android.widget.Toast;

import com.example.kaise.adproject_team10.Entities.Assignment;
import com.example.kaise.adproject_team10.Entities.BriefEmployee;
import com.example.kaise.adproject_team10.LoginActivity;
import com.example.kaise.adproject_team10.MainActivity;
import com.example.kaise.adproject_team10.R;
import com.example.kaise.adproject_team10.Store.DisbursementDetails;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

/*created by Lee Kai Seng*/

public class ManageActHead extends AppCompatActivity implements View.OnClickListener {
    String DeptId = "";
    String EmployeeId = "";
    String EmployeeName = "";
    SimpleDateFormat dateFormat;
    TextView tvHeadName;
    TextView tvMessage;
    EditText etStartDate;
    EditText etEndDate;
    DatePickerDialog startDatePickerDialog;
    DatePickerDialog endDatePickerDialog;
    Button btnUpdate;
    Button btnRemove;
    ImageButton btnEmpList;
    SharedPreferences pref;
    boolean isNew;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_manage_act_head);
        findAllViewsById();
        restoreInstance(savedInstanceState);
        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        DeptId = pref.getString("DeptId", "DeptId");
        dateFormat = new SimpleDateFormat("dd-MM-yyyy", Locale.ENGLISH);

        new AsyncTask<String, Void, Assignment>() {

            @Override
            protected Assignment doInBackground(String... params) {
                return Assignment.getAssignment(params[0]);
            }

            @Override
            protected void onPostExecute(Assignment a) {
                if (a != null) {
                    show(a);
                } else {
                    showEmpty(new Intent());
                }
            }
        }.execute(DeptId);

        setDatePickerDialog();
    }

    void findAllViewsById() {
        tvHeadName = findViewById(R.id.tvHeadName);
        tvMessage = findViewById(R.id.tvMessage);
        etStartDate = findViewById(R.id.etStartDate);
        etStartDate.setInputType(InputType.TYPE_NULL);
        etEndDate = findViewById(R.id.etEndDate);
        etEndDate.setInputType(InputType.TYPE_NULL);
        btnUpdate = findViewById(R.id.btnUpdate);
        btnRemove = findViewById(R.id.btnRemove);
        btnEmpList = findViewById(R.id.btnEmpList);
    }

    void setDatePickerDialog() {
        final Calendar calendar = Calendar.getInstance();
        int year = calendar.get(Calendar.YEAR);
        int month = calendar.get(Calendar.MONTH);
        int day = calendar.get(Calendar.DAY_OF_MONTH);

        etStartDate.setOnClickListener(this);
        startDatePickerDialog = new DatePickerDialog(this, new DatePickerDialog.OnDateSetListener() {

            @Override
            public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
                Calendar startDate = Calendar.getInstance();
                startDate.set(year, month, dayOfMonth);
                etStartDate.setText(dateFormat.format(startDate.getTime()));
            }
        }, year, month, day);
        startDatePickerDialog.getDatePicker().setMinDate(System.currentTimeMillis());

        etEndDate.setOnClickListener(this);
        endDatePickerDialog = new DatePickerDialog(this, new DatePickerDialog.OnDateSetListener() {
            @Override
            public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
                Calendar endDate = Calendar.getInstance();
                endDate.set(year, month, dayOfMonth);
                etEndDate.setText(dateFormat.format(endDate.getTime()));
            }
        }, year, month, day);
        endDatePickerDialog.getDatePicker().setMinDate(System.currentTimeMillis());
    }

    @Override
    public void onClick(View v) {
        if (v == etStartDate) {
            if (!pref.getString("EmployeeId", "EmployeeId").equalsIgnoreCase(EmployeeId)) {
                startDatePickerDialog.show();
            }
        } else if (v == etEndDate) {
            if (!pref.getString("EmployeeId", "EmployeeId").equalsIgnoreCase(EmployeeId)) {
                endDatePickerDialog.show();
            }
        } else if (v == btnUpdate) {
            new AlertDialog.Builder(ManageActHead.this)
                    .setTitle("Save Delegation")
                    .setMessage("Are you sure to save?")
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
        } else if (v == btnRemove) {
            new AlertDialog.Builder(ManageActHead.this)
                    .setTitle("Remove Delegation")
                    .setMessage("Are you sure to remove?")
                    .setCancelable(false)
                    .setPositiveButton(android.R.string.yes, new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog, int which) {
                            remove();
                        }
                    })
                    .setNegativeButton(android.R.string.no, new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog, int which) {
                        }
                    })
                    .show();
        } else if (v == btnEmpList) {
            showEmpList();
        }
    }

    void show(Assignment assignment) {
        EmployeeId = assignment.get("EmployeeId");
        tvHeadName.setText(assignment.get("EmployeeName"));
        etStartDate.setText(assignment.get("StartDate"));
        etEndDate.setText(assignment.get("EndDate"));
        tvMessage.setText("");
        btnUpdate.setText("Update");
        btnRemove.setVisibility(View.VISIBLE);
        btnEmpList.setVisibility(View.GONE);
        isNew = false;

        if (pref.getString("EmployeeId", "EmployeeId").equalsIgnoreCase(EmployeeId)) {
            etStartDate.setClickable(false);
            etEndDate.setClickable(false);
            btnUpdate.setVisibility(View.GONE);
            btnRemove.setVisibility(View.GONE);
        }
    }

    void showEmpty(Intent intent) {
        if (intent.hasExtra("edited")) {
            EmployeeId = intent.getStringExtra("EmployeeId");
            EmployeeName = intent.getStringExtra("EmployeeName");
            tvHeadName.setText(EmployeeName);
        } else if (!EmployeeName.isEmpty()) {
            tvHeadName.setText(EmployeeName);
        } else {
            tvHeadName.setText("No Delegation");
        }
        etStartDate.setText("");
        etEndDate.setText("");
        tvMessage.setText("");
        btnUpdate.setText("Delegate");
        btnRemove.setVisibility(View.GONE);
        btnEmpList.setVisibility(View.VISIBLE);
        isNew = true;
    }

    void update() {
        String startDate = etStartDate.getText().toString();
        String endDate = etEndDate.getText().toString();
        Date convertedStartDate = new Date();
        Date convertedEndDate = new Date();

        try {
            convertedStartDate = dateFormat.parse(startDate);
            convertedEndDate = dateFormat.parse(endDate);
        } catch (Exception e) {
            e.getStackTrace();
            Log.e("Parsing both dates", "error occurs");
        }

        if (EmployeeId.isEmpty()) {
            tvMessage.setText("*Please select employee");
        } else if (startDate.isEmpty() || endDate.isEmpty()) {
            tvMessage.setText("*Please select date");
        } else if (convertedStartDate.after(convertedEndDate)) {
            tvMessage.setText("*Start Date must be earlier than End Date");
        } else {
            tvMessage.setText("");
            Assignment assignment = new Assignment();
            assignment.put("EmployeeName", tvHeadName.getText().toString());
            assignment.put("TemporaryRoleId", "ActHead");
            assignment.put("EmployeeId", EmployeeId);
            assignment.put("StartDate", startDate);
            assignment.put("EndDate", endDate);

            new AsyncTask<Assignment, Void, String>() {

                @Override
                protected String doInBackground(Assignment... assignments) {
                    String result = Assignment.updateAssignment(assignments[0], isNew);
                    Log.i("background result", result);
                    return result;
                }

                @Override
                protected void onPostExecute(String result) {
                    String subResult = result.substring(1, 5);
                    Log.i("foreground result", subResult);
                    if (subResult.equalsIgnoreCase("true")) {
                        Toast.makeText(ManageActHead.this, "Saved", Toast.LENGTH_SHORT).show();
                        recreate();
                    } else {
                        Toast.makeText(ManageActHead.this, "Failed. Please try again", Toast.LENGTH_SHORT).show();
                    }
                }
            }.execute(assignment);
        }
    }

    void remove() {
        new AsyncTask<String, Void, String>() {

            @Override
            protected String doInBackground(String... strings) {
                String result = Assignment.deleteAssignment(strings[0]);
                Log.i("background result", result);
                return result;
            }

            @Override
            protected void onPostExecute(String result) {
                String subResult = result.substring(1, 5);
                Log.i("foreground result", subResult);
                if (subResult.equalsIgnoreCase("true")) {
                    EmployeeId = "";
                    EmployeeName = "";
                    Toast.makeText(ManageActHead.this, "Removed", Toast.LENGTH_SHORT).show();
                    recreate();
                } else {
                    Toast.makeText(ManageActHead.this, "Failed. Please try again", Toast.LENGTH_SHORT).show();
                }
            }
        }.execute(EmployeeId);
    }

    void showEmpList() {
        Intent intent = new Intent(getApplicationContext(), EmployeeList.class);
        startActivityForResult(intent, 131);
    }

    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode == RESULT_OK && requestCode == 131) {
            if (data.hasExtra("edited")) {
                int result = data.getExtras().getInt("edited");
                if (result == 1) {
                    showEmpty(data);
                    Toast.makeText(getApplicationContext(), "Selected employee", Toast.LENGTH_SHORT).show();
                }
            }
        }
    }

    void restoreInstance(Bundle state) {
        if (state != null) {
            EmployeeId = state.getString("EmployeeId");
            EmployeeName = state.getString("EmployeeName");
            etStartDate.setText(state.getString("StartDate"));
            etEndDate.setText(state.getString("EndDate"));
        }
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putString("EmployeeId", EmployeeId);
        outState.putString("EmployeeName", EmployeeName);
        outState.putString("StartDate", etStartDate.getText().toString());
        outState.putString("EndDate", etEndDate.getText().toString());
    }

}