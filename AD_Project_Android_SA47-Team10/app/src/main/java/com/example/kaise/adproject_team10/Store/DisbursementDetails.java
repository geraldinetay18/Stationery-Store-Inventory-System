package com.example.kaise.adproject_team10.Store;

import android.content.SharedPreferences;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.content.Intent;
import android.os.AsyncTask;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.kaise.adproject_team10.Entities.BriefDisbursementDetails;
import com.example.kaise.adproject_team10.R;

public class DisbursementDetails extends AppCompatActivity {
    String empId;
    String DisbursementDetailsId;
    String ItemCode;
    String ItemDescription;
    String QuantityRequested;
    String QuantityReceived;
    String QuantityDisbursing;
    String QuantityRejected;
    String Reason;

    TextView tvItemDescription;
    TextView tvRequestedQty2;
    TextView tvDisbursingQty2;
    EditText etReceivingQty;
    TextView tvRejectedQty2;
    Spinner spinnerReason;
    TextView tvMessage;
    Button btnSave;
    SharedPreferences pref;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_disbursement_details);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        empId = pref.getString("EmployeeId", "EmployeeId");

        Intent intent = getIntent();
        if (intent.hasExtra("DisbursementDetailsId")) {
            DisbursementDetailsId = intent.getStringExtra("DisbursementDetailsId");
            ItemCode = intent.getStringExtra("ItemCode");
            ItemDescription = intent.getStringExtra("ItemDescription");
            QuantityRequested = intent.getStringExtra("QuantityRequested");
            QuantityReceived = intent.getStringExtra("QuantityReceived");
            QuantityDisbursing = intent.getStringExtra("QuantityReceived");
        }

        findViewsById();
        show();
    }

    void findViewsById() {
        tvItemDescription = findViewById(R.id.tvItemDescription);
        tvRequestedQty2 = findViewById(R.id.tvRequestedQty2);
        tvDisbursingQty2 = findViewById(R.id.tvDisbursingQty2);
        etReceivingQty = findViewById(R.id.etReceivingQty);
        tvRejectedQty2 = findViewById(R.id.tvRejectedQty2);
        tvMessage = findViewById(R.id.tvMessage);

        btnSave = findViewById(R.id.btnSave);
        btnSave.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Save();
            }
        });

        spinnerReason = findViewById(R.id.spinnerReason);
        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this, R.array.AdjustmentReason, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerReason.setAdapter(adapter);
        spinnerReason.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                String s = parent.getItemAtPosition(position).toString();
                Reason = s;
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
            }
        });

    }

    void show() {
        tvItemDescription.setText(ItemDescription);
        tvRequestedQty2.setText(QuantityRequested);
        tvDisbursingQty2.setText(QuantityDisbursing);
        etReceivingQty.setText(QuantityReceived);
        etReceivingQty.addTextChangedListener(new TextWatcher() {
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                CalculateAdjustedQuantity(s);
            }

            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            public void afterTextChanged(Editable s) {
            }
        });
        CalculateAdjustedQuantity(QuantityReceived);
    }

    void CalculateAdjustedQuantity(CharSequence s) {
        int QtyDisbursing = Integer.parseInt(QuantityDisbursing);
        int QtyReceiving;
        if (s.toString().isEmpty()) {
            QtyReceiving = 0;
        } else {
            QtyReceiving = Integer.parseInt(s.toString());
        }
        int QtyRejected = QtyReceiving - QtyDisbursing;
        tvRejectedQty2.setText(Integer.toString(QtyRejected));
    }

    void Save() {
        if (etReceivingQty.getText().toString().isEmpty()) {
            tvMessage.setText("*Receiving Qty cannot be empty");
        } else if (etReceivingQty.getText().toString().equalsIgnoreCase(QuantityDisbursing)) {
            tvMessage.setText("*No changes");
        } else {
            QuantityRejected = tvRejectedQty2.getText().toString();
            int RejectedQty = Integer.parseInt(QuantityRejected);
            if (RejectedQty > 0) {
                tvMessage.setText("*Receiving Qty cannot be more than Disbursing Qty");
            } else {
                QuantityReceived = etReceivingQty.getText().toString();
                new AsyncTask<String, Void, String>() {
                    @Override
                    protected String doInBackground(String... params) {
                        String result = BriefDisbursementDetails.saveEachDisbursingDetail(params[0], params[1], params[2], params[3], params[4]);
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
                }.execute(empId, DisbursementDetailsId, QuantityReceived, QuantityRejected, Reason);
            }
        }
    }
}