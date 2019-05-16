package com.example.kaise.adproject_team10.Entities;

import android.util.Log;

import org.json.JSONObject;

import java.util.HashMap;

/*created by Lee Kai Seng*/

public class Assignment extends HashMap<String, String> {
    final static String url = Host.URL + "/Assignment";

    public Assignment(String EmployeeName, String TemporaryRoleId, String EmployeeId, String StartDate, String EndDate) {
        put("EmployeeName", EmployeeName);
        put("TemporaryRoleId", TemporaryRoleId);
        put("EmployeeId", EmployeeId);
        put("StartDate", StartDate);
        put("EndDate", EndDate);
    }

    public Assignment() {
    }

    public static Assignment getAssignment(String deptId) {
        Assignment actingHead = null;
        try {
            JSONObject b = JSONParser.getJSONFromUrl(url + "/" + deptId);
            Log.i("ActingHeadId", url + "/" + deptId);
            if (b != null) {
                actingHead = new Assignment(
                        b.getString("EmployeeName"),
                        b.getString("TemporaryRoleId"),
                        b.getString("EmployeeId"),
                        b.getString("StartDate"),
                        b.getString("EndDate")
                );
                Log.i("ActingHead", actingHead.toString());
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("In getAssignment", "error occurs, returning null");
        }
        return actingHead;
    }

    public static String updateAssignment(Assignment a, boolean isNew) {
        JSONObject jassignment = new JSONObject();
        try {
            jassignment.put("EmployeeName", a.get("EmployeeName"));
            jassignment.put("TemporaryRoleId", a.get("TemporaryRoleId"));
            jassignment.put("EmployeeId", a.get("EmployeeId"));
            jassignment.put("StartDate", a.get("StartDate"));
            jassignment.put("EndDate", a.get("EndDate"));
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("In updateAssignment", "errors occurs");
        }
        String result;
        if (isNew) {
            result = JSONParser.postStream(url + "/add", true, jassignment.toString());
            Log.i("addAssignment result", result);
        } else {
            result = JSONParser.postStream(url + "/update", true, jassignment.toString());
            Log.i("updateAssignment result", result);
        }
        return result;
    }

    public static String deleteAssignment(String EmployeeId) {
        String result = JSONParser.getStream(url + "/delete/" + EmployeeId);
        Log.i("deleteAssignment result", result);
        return result;
    }
}