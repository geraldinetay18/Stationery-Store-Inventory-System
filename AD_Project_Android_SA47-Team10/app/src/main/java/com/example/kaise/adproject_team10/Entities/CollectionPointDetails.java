package com.example.kaise.adproject_team10.Entities;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/*created by Shalin Christina Stephen Selvaraja*/

public class CollectionPointDetails extends HashMap<String, String> {
    final static String url = Host.URL + "/Collection";

    public CollectionPointDetails(String EmployeeId, String LocationId, String LocationName, String Time) {
        put("EmployeeId", EmployeeId);
        put("LocationId", LocationId);
        put("LocationName", LocationName);
        put("Time", Time);
    }

    public CollectionPointDetails() {
    }

    public static CollectionPointDetails getCollectionPointById(String LocationId) {
        CollectionPointDetails collectionPointDetails = null;
        try {
            JSONObject b = JSONParser.getJSONFromUrl(url + "/" + LocationId);
            Log.i("LocationId", url + "/" + LocationId);
            if (b != null) {
                collectionPointDetails = new CollectionPointDetails(
                        b.getString("EmployeeId"),
                        b.getString("LocationId"),
                        b.getString("LocationName"),
                        b.getString("Time"));
                Log.i("CollectionPointDetails", collectionPointDetails.toString());
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("getCollectionPointById", "error occurs");
        }
        return collectionPointDetails;
    }

    public static List<CollectionPointDetails> getAllCollectionPoints() {
        List<CollectionPointDetails> collectionPointDetails = new ArrayList<CollectionPointDetails>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(url + "/Collectionlist");
            if (a != null) {
                for (int i = 0; i < a.length(); i++) {
                    JSONObject b = a.getJSONObject(i);
                    CollectionPointDetails collections = new CollectionPointDetails(
                            b.getString("EmployeeId"),
                            b.getString("LocationId"),
                            b.getString("LocationName"),
                            b.getString("Time"));
                    collectionPointDetails.add(collections);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("CollectionPointList", "error occurs");
        }
        return collectionPointDetails;
    }
}