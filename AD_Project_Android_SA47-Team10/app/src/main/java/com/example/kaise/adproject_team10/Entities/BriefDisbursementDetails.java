package com.example.kaise.adproject_team10.Entities;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/*created by Lee Kai Seng*/

public class BriefDisbursementDetails extends HashMap<String, String> {
    final static String url = Host.URL + "/Disbursement/details";

    public BriefDisbursementDetails(String DisbursementDetailsId, String ItemCode, String ItemDescription, String QuantityRequested, String QuantityReceived) {
        put("DisbursementDetailsId", DisbursementDetailsId);
        put("ItemCode", ItemCode);
        put("ItemDescription", ItemDescription);
        put("QuantityRequested", QuantityRequested);
        put("QuantityReceived", QuantityReceived);
    }

    public BriefDisbursementDetails() {
    }

    public static List<BriefDisbursementDetails> getBriefDisbursementDetails(String disbursementId) {
        List<BriefDisbursementDetails> briefDisbursementDetailsList = new ArrayList<>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(url + "/" + disbursementId);
            if (a != null) {
                for (int i = 0; i < a.length(); i++) {
                    JSONObject b = a.getJSONObject(i);
                    BriefDisbursementDetails briefDisbursementDetails = new BriefDisbursementDetails(
                            b.getString("DisbursementDetailsId"),
                            b.getString("ItemCode"),
                            b.getString("ItemDescription"),
                            b.getString("QuantityRequested"),
                            b.getString("QuantityReceived")
                    );
                    briefDisbursementDetailsList.add(briefDisbursementDetails);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("getBriefDisbursementDet", "error occurs");
        }
        return briefDisbursementDetailsList;
    }

    public static String saveEachDisbursingDetail(String storeClerkId, String disbursementDetailId, String receivedQty, String SAVQty, String reason) {
        String result = "";
        try {
            result = JSONParser.getStream(url + "/save/" + storeClerkId + "/" + disbursementDetailId + "/" + receivedQty + "/" + SAVQty + "/" + reason);
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("saveDisbursingDetail", "error occurs");
        }
        return result;
    }
}
