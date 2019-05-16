package com.example.kaise.adproject_team10.Entities;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/*created by Nguyen Ngoc Doan Trang*/

public class Disbursement extends HashMap<String, String> {

    private String disbursementId;
    private String dateOfDisbursement;
    private String status;
    private String remark;
    private String collectionPoint;

    public Disbursement() {
    }

    public Disbursement(String disbursementId, String dateOfDisbursement, String status, String remark, String collectionPoint) {
        put("DisbursementId", disbursementId);
        put("DateOfDisbursement", dateOfDisbursement);
        put("Status", status);
        put("Remark", remark);
        put("CollectionPoint", collectionPoint);
    }

    public static List<Disbursement> getDisbursements(String year, String deptId) {
        List<Disbursement> list = new ArrayList<Disbursement>();
        try {
            String url = Host.URL + "/StationeryCollection/" + year + "/" + deptId;
            JSONArray a = JSONParser.getJSONArrayFromUrl(url);
            if (a != null) {
                for (int i = 0; i < a.length(); i++) {
                    JSONObject json = a.getJSONObject(i);
                    String disbursementId = json.getString("DisbursementId");
                    String dateOfDisbursement = json.getString("DateOfDisbursement");
                    String status = json.getString("Status");
                    String remark = json.getString("Remark");
                    Disbursement disbursement = new Disbursement(disbursementId, dateOfDisbursement, status, remark, null);
                    list.add(disbursement);
                }
            }
        } catch (Exception e) {
            Log.e("In getDisbursements", "JSONArray error");
        }
        return list;
    }

    public static Disbursement getDisbursement(String disbursementId) {
        Disbursement disbursement = null;
        try {
            String url = Host.URL + "/StationeryCollection/" + disbursementId;
            JSONObject json = JSONParser.getJSONFromUrl(url);
            if (json != null) {
                disbursement = new Disbursement();
                disbursement.setStatus(json.getString("Status"));
                disbursement.setCollectionPoint(json.getString("CollectionPoint"));
                disbursement.setDateOfDisbursement(json.getString("Date"));
            }
        } catch (Exception e) {
            Log.e("In getDisbursement", "JSONArray error");
        }
        return disbursement;
    }

    public static int acknowledge(String disbursementId) {
        try {
            String url = Host.URL + "/sc/acknowledge/" + disbursementId;
            JSONParser.getStream(url);
            return 1;
        } catch (Exception e) {
            Log.e("In acknowledge", "JSONArray error");
            return 0;
        }
    }

    public String getDisbursementId() {
        return disbursementId;
    }

    public void setDisbursementId(String disbursementId) {
        this.disbursementId = disbursementId;
    }

    public String getDateOfDisbursement() {
        return dateOfDisbursement;
    }

    public void setDateOfDisbursement(String dateOfDisbursement) {
        this.dateOfDisbursement = dateOfDisbursement;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }

    public String getCollectionPoint() {
        return collectionPoint;
    }

    public void setCollectionPoint(String collectionPoint) {
        this.collectionPoint = collectionPoint;
    }
}