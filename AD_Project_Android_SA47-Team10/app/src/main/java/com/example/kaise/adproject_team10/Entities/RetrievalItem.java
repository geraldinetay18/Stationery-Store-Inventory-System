package com.example.kaise.adproject_team10.Entities;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/*created by Lee Kai Seng*/

public class RetrievalItem extends HashMap<String, String> {
    final static String url = Host.URL + "/Retrieval";

    public RetrievalItem(String RetrievalDetailsId, String ItemCode, String RetrievalId, String QuantityRetrieved, String QuantityNeeded,
                         String Description, String QuantityInStock, String Bin) {
        put("RetrievalDetailsId", RetrievalDetailsId);
        put("ItemCode", ItemCode);
        put("RetrievalId", RetrievalId);
        put("QuantityRetrieved", QuantityRetrieved);
        put("QuantityNeeded", QuantityNeeded);
        put("Description", Description);
        put("QuantityInStock", QuantityInStock);
        put("Bin", Bin);
    }

    public RetrievalItem() {
    }

    public static List<RetrievalItem> getRetrievalItems(String empId) {
        List<RetrievalItem> retrievalItemList = new ArrayList<>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(url + "/" + empId);
            if (a != null) {
                for (int i = 0; i < a.length(); i++) {
                    JSONObject b = a.getJSONObject(i);
                    RetrievalItem retrievalItem = new RetrievalItem(
                            b.getString("RetrievalDetailsId"),
                            b.getString("ItemCode"),
                            b.getString("RetrievalId"),
                            b.getString("QuantityRetrieved"),
                            b.getString("QuantityNeeded"),
                            b.getString("Description"),
                            b.getString("QuantityInStock"),
                            b.getString("Bin"));
                    retrievalItemList.add(retrievalItem);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("getRetrievalItems", "error occurs");
        }
        return retrievalItemList;

    }

    public static String updateRetrievalItem(String RetrievalId, String ItemCode, String QuantityRetrieved, String AdjustedQty, String Reason, String StoreClerkId) {
        String result = "";
        try {
            result = JSONParser.getStream(url + "/update/" + RetrievalId + "/" + ItemCode + "/" + QuantityRetrieved + "/" + AdjustedQty + "/" + Reason + "/" + StoreClerkId);
        } catch (Exception e) {
            Log.e("updateRetrievalItem", "error occurs");
        }
        return result;
    }

    public static String confirmRetrieval(String StoreClerkId, String RetrievalId) {
        String result = "";
        try {
            result = JSONParser.getStream(url + "/confirm/" + StoreClerkId + "/" + RetrievalId);
        } catch (Exception e) {
            Log.e("confirmRetrieval", "error occurs");
        }
        return result;
    }
}