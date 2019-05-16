package com.example.kaise.adproject_team10.Entities;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/*created by Lee Kai Seng*/

public class BriefDisbursement extends HashMap<String, String> {
    final static String url = Host.URL + "/Disbursement";

    public BriefDisbursement(String DisbursementId, String DeptId, String DeptName, String DateDisbursed, String CollectionPointId, String CollectionPointName, String CollectionPointTime, String Status, String RepresentativeId, String RepresentativeName, String RepresentativePhone) {
        put("DisbursementId", DisbursementId);
        put("DeptId", DeptId);
        put("DeptName", DeptName);
        put("DateDisbursed", DateDisbursed);
        put("CollectionPointId", CollectionPointId);
        put("CollectionPointName", CollectionPointName);
        put("CollectionPointTime", CollectionPointTime);
        put("Status", Status);
        put("RepresentativeId", RepresentativeId);
        put("RepresentativeName", RepresentativeName);
        put("RepresentativePhone", RepresentativePhone);
    }

    public BriefDisbursement() {

    }

    public static List<BriefDisbursement> GetBriefDisbursements(String storeClerkId) {
        List<BriefDisbursement> briefDisbursementList = new ArrayList<>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(url + "/" + storeClerkId);
            if (a != null) {
                for (int i = 0; i < a.length(); i++) {
                    JSONObject b = a.getJSONObject(i);
                    BriefDisbursement briefDisbursement = new BriefDisbursement(
                            b.getString("DisbursementId"),
                            b.getString("DeptId"),
                            b.getString("DeptName"),
                            b.getString("DateDisbursed"),
                            b.getString("CollectionPointId"),
                            b.getString("CollectionPointName"),
                            b.getString("CollectionPointTime"),
                            b.getString("Status"),
                            b.getString("RepresentativeId"),
                            b.getString("RepresentativeName"),
                            b.getString("RepresentativePhone"));
                    briefDisbursementList.add(briefDisbursement);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("getBriefDisbursements", "error occurs");
        }
        return briefDisbursementList;
    }

    public static String MarkAsNotCollected(String storeClerkId, String currentDisbId) {
        String result = "";
        try {
            result = JSONParser.getStream(url + "/notcollected/" + storeClerkId + "/" + currentDisbId);
        } catch (Exception e) {
            Log.e("MarkAsNotCollected", "error occurs");
        }
        return result;
    }

    public static String RequestForAcknowledgement(String storeClerkId, String currentDisbId) {
        String result = "";
        try {
            result = JSONParser.getStream(url + "/acknowledge/" + storeClerkId + "/" + currentDisbId);
        } catch (Exception e) {
            Log.e("Acknowledgement", "error occurs");
        }
        return result;
    }
}
