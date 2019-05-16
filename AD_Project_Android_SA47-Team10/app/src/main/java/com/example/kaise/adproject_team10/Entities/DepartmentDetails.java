package com.example.kaise.adproject_team10.Entities;

import android.util.Log;

import org.json.JSONObject;

import java.util.HashMap;

/*created by Shalin Christina Stephen Selvaraja*/

public class DepartmentDetails extends HashMap<String, String> {
    final static String url = Host.URL + "/Department";

    public DepartmentDetails() {
    }

    public DepartmentDetails(String DeptId, String LocationId, String StoreClerkId,
                             String RepresentativeId, String HeadId, String DeptName,
                             String Contact_Name, String TelephoneNo, String Fax) {

        put("DeptId", DeptId);
        put("LocationId", LocationId);
        put("StoreClerkId", StoreClerkId);
        put("RepresentativeId", RepresentativeId);
        put("HeadId", HeadId);
        put("DeptName", DeptName);
        put("Contact_Name", Contact_Name);
        put("TelephoneNo", TelephoneNo);
        put("Fax", Fax);
    }

    public static DepartmentDetails getDepartmentById(String DeptId) {
        DepartmentDetails departmentDetails = null;
        try {
            JSONObject b = JSONParser.getJSONFromUrl(url + "/" + DeptId);
            Log.i("DeptId", url + "/" + DeptId);
            if (b != null) {
                departmentDetails = new DepartmentDetails(
                        b.getString("DeptId"),
                        b.getString("LocationId"),
                        b.getString("StoreClerkId"),
                        b.getString("RepresentativeId"),
                        b.getString("HeadId"),
                        b.getString("DeptName"),
                        b.getString("Contact_Name"),
                        b.getString("TelephoneNo"),
                        b.getString("Fax"));
                Log.i("DepartmentDetails", departmentDetails.toString());
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("getDepartmentById", "error occurs");
        }
        return departmentDetails;
    }

    public static String updateCollectionPoint(String newlocationId, String deptId, String oldlocationId) {
        String result = "";
        try {
            result = JSONParser.getStream(url + "/updatecollection/" + newlocationId + "/" + deptId + "/" + oldlocationId);
            Log.i("Update result", result);
        } catch (Exception e) {
            Log.e("updateCollectionPoint", "error occurs");
        }
        return result;

    }

}