package com.example.kaise.adproject_team10.Store;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.kaise.adproject_team10.Entities.RetrievalItem;
import com.example.kaise.adproject_team10.R;

public class NewRetrievalDetail extends AppCompatActivity {

    String RetrievalDetailsId;
    String ItemCode;
    String RetrievalId;
    String QuantityRetrieved;
    String QuantityNeeded;
    String Description;
    String QuantityInStock;
    String Bin;
    String AdjustedQty;
    String Reason;
    String empId = "10032";
    TextView tvDescription1;
    TextView tvBin1;
    TextView tvStock1;
    TextView tvRequestedQty1;
    TextView tvMessage;
    EditText etRetrievedQty;
    EditText etAdjustedQty;
    Button btnSave;
    Spinner spinner;
    SharedPreferences pref;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_retrieval_detail);

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        empId = pref.getString("EmployeeId", "EmployeeId");

        Intent intent = getIntent();
        if (intent.hasExtra("RetrievalDetailsId")) {
            RetrievalDetailsId = intent.getStringExtra("RetrievalDetailsId");
            ItemCode = intent.getStringExtra("ItemCode");
            RetrievalId = intent.getStringExtra("RetrievalId");
            QuantityRetrieved = intent.getStringExtra("QuantityRetrieved");
            QuantityNeeded = intent.getStringExtra("QuantityNeeded");
            Description = intent.getStringExtra("Description");
            QuantityInStock = intent.getStringExtra("QuantityInStock");
            Bin = intent.getStringExtra("Bin");
        }

        findViewsById();
        show();
    }

    void findViewsById() {
        tvDescription1 = findViewById(R.id.tvDescription1);
        tvBin1 = findViewById(R.id.tvBin1);
        tvStock1 = findViewById(R.id.tvStock1);
        tvRequestedQty1 = findViewById(R.id.tvRequestedQty1);
        tvMessage = findViewById(R.id.tvMessage);
        etRetrievedQty = findViewById(R.id.etRetrievedQty);
        etAdjustedQty = findViewById(R.id.etAdjustedQty);

        btnSave = findViewById(R.id.btnSave);
        btnSave.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                save();
            }
        });

        spinner = findViewById(R.id.spinnerReason);
        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this,
                R.array.AdjustmentReason, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(adapter);
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
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
        tvDescription1.setText(Description);
        tvBin1.setText(Bin);
        tvStock1.setText(QuantityInStock);
        tvRequestedQty1.setText(QuantityNeeded);
        etRetrievedQty.setText(QuantityRetrieved);
    }

    void save() {
        if (etRetrievedQty.getText().toString().isEmpty()) {
            tvMessage.setText("*Retrieved Qty cannot be empty");
        } else {
            QuantityRetrieved = etRetrievedQty.getText().toString();
            int RetrievedQty = Integer.parseInt(QuantityRetrieved);
            int StockQty = Integer.parseInt(QuantityInStock);
            int RequestedQty = Integer.parseInt(QuantityNeeded);
            if (StockQty < RetrievedQty) {
                tvMessage.setText("*Not enough stock");
            } else if (RequestedQty < RetrievedQty) {
                tvMessage.setText("*Cannot retrieve more than Requested Qty");
            } else {
                if (etAdjustedQty.getText().toString().isEmpty()) {
                    AdjustedQty = "0";
                } else {
                    AdjustedQty = etAdjustedQty.getText().toString();
                }

                new AsyncTask<String, Void, String>() {

                    @Override
                    protected String doInBackground(String... params) {
                        String result = RetrievalItem.updateRetrievalItem(params[0], params[1], params[2], params[3], params[4], params[5]);
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
                }.execute(RetrievalId, ItemCode, QuantityRetrieved, AdjustedQty, Reason, empId);
            }
        }
    }
}
