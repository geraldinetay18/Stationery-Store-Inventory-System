package com.example.kaise.adproject_team10.Dept;

import android.support.v7.app.AppCompatActivity;
import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.AdapterView.*;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

import com.example.kaise.adproject_team10.R;

/*created by Nguyen Ngoc Doan Trang*/

public class StationeryCollection extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_stationery_collection);

        final Spinner dropdown = findViewById(R.id.spinner);
        String[] items = new String[]{"", "2019", "2018", "2017", "2016", "2015", "2014", "2013", "2012", "2011", "2010"};
        ArrayAdapter<String> adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_dropdown_item, items);
        dropdown.setAdapter(adapter);

        dropdown.setOnItemSelectedListener(new OnItemSelectedListener() {
            @SuppressLint("LongLogTag")
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                String year = dropdown.getSelectedItem().toString();
                if (year != "") {
                    Intent intent = new Intent(getApplicationContext(), StationeryCollection02.class);
                    intent.putExtra("year", year);
                    startActivityForResult(intent, 150);
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {
            }
        });
    }
}
