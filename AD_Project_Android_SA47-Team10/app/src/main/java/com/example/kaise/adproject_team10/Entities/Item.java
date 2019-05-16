package com.example.kaise.adproject_team10.Entities;

import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/*created by Nguyen Ngoc Doan Trang*/

public class Item {

    private String itemName;
    private String qtyNeed;
    private String qtyDisbursed;
    private String status;
    private String requestDate;

    public static List<Item> getItems(String disbursementId) {
        List<Item> list = new ArrayList<Item>();
        try {
            String url = Host.URL + "/Stationery/" + disbursementId;
            JSONArray a = JSONParser.getJSONArrayFromUrl(url);
            if (a != null) {
                for (int i = 0; i < a.length(); i++) {
                    JSONObject json = a.getJSONObject(i);
                    Item item = new Item();
                    item.setItemName(json.getString("StationeryDescription"));
                    item.setQtyNeed(json.getString("QuantityNeed"));
                    item.setQtyDisbursed(json.getString("QuantityDisbursed"));
                    item.setRequestDate(json.getString("RequestDate"));
                    list.add(item);
                }
            }
        } catch (Exception e) {
            Log.e("In getItems", "JSONArray error");
        }
        return list;
    }

    public String getItemName() {
        return itemName;
    }

    public void setItemName(String itemName) {
        this.itemName = itemName;
    }

    public String getQtyNeed() {
        return qtyNeed;
    }

    public void setQtyNeed(String qtyNeed) {
        this.qtyNeed = qtyNeed;
    }

    public String getQtyDisbursed() {
        return qtyDisbursed;
    }

    public void setQtyDisbursed(String qtyDisbursed) {
        this.qtyDisbursed = qtyDisbursed;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getRequestDate() {
        return requestDate;
    }

    public void setRequestDate(String requestDate) {
        this.requestDate = requestDate;
    }
}