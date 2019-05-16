package com.example.kaise.adproject_team10.Dept;

import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import com.example.kaise.adproject_team10.Entities.CollectionPointDetails;
import com.example.kaise.adproject_team10.R;

import java.util.List;

/*created by Shalin Christina Stephen Selvaraja*/

public class CollectionPointList extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_collection_point_list);

        new AsyncTask<Void, Void, List<CollectionPointDetails>>() {

            @Override
            protected List<CollectionPointDetails> doInBackground(Void... params) {
                return CollectionPointDetails.getAllCollectionPoints();
            }

            @Override
            protected void onPostExecute(List<CollectionPointDetails> result) {
                ListView list = findViewById(R.id.lvList);
                list.setAdapter(new SimpleAdapter(getApplicationContext(), result,
                        R.layout.rowcollection, new String[]{"LocationName", "Time"},
                        new int[]{R.id.txtLocationName1, R.id.txtTime1}));
                list.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                    @Override
                    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                        CollectionPointDetails c = (CollectionPointDetails) parent.getAdapter().getItem(position);
                        Intent i = new Intent();
                        i.putExtra("edited", 1);
                        i.putExtra("LocationId", c.get("LocationId"));
                        i.putExtra("LocationName", c.get("LocationName"));
                        setResult(RESULT_OK, i);
                        finish();
                    }
                });
            }
        }.execute();
    }
}