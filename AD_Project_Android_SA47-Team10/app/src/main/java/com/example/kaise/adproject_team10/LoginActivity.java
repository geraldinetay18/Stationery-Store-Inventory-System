package com.example.kaise.adproject_team10;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.kaise.adproject_team10.Entities.BriefEmployee;
import com.example.kaise.adproject_team10.Entities.JSONParser;

/*created by Lee Kai Seng*/

public class LoginActivity extends AppCompatActivity {

    SharedPreferences pref;
    EditText etEmail, etPassword;
    Button btnLogin;
    TextView tvMessage;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        if (!JSONParser.access_token.isEmpty()) {
            startActivity(new Intent(getApplicationContext(), MainActivity.class));
            finish();
        }

        findViewsById();

        pref = PreferenceManager.getDefaultSharedPreferences(getApplicationContext());
        tvMessage.setText("");

        btnLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (etEmail.getText().toString().isEmpty()) {
                    tvMessage.setText("*Email is required");
                } else if (etPassword.getText().toString().isEmpty()) {
                    tvMessage.setText("*Password is required");
                } else {
                    SharedPreferences.Editor editor = pref.edit();
                    editor.putString("Email", etEmail.getText().toString().trim());
                    editor.putString("Password", etPassword.getText().toString());
                    editor.commit();

                    new AsyncTask<Void, Void, Void>() {
                        @Override
                        protected Void doInBackground(Void... v) {
                            BriefEmployee.Login(pref);
                            return null;
                        }

                        @Override
                        protected void onPostExecute(Void aVoid) {
                            if (!JSONParser.access_token.isEmpty()) {
                                startActivity(new Intent(getApplicationContext(), MainActivity.class));
                                finish();
                            } else {
                                tvMessage.setText("*Log in failed. Please try again.");
                            }
                        }
                    }.execute();
                }
            }
        });
    }

    void findViewsById() {
        etEmail = findViewById(R.id.etEmail);
        etPassword = findViewById(R.id.etPassword);
        btnLogin = findViewById(R.id.btnLogin);
        tvMessage = findViewById(R.id.tvMessage);
    }
}
