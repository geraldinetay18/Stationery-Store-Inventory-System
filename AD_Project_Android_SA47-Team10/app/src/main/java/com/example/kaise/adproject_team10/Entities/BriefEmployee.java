package com.example.kaise.adproject_team10.Entities;

import android.content.SharedPreferences;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONObject;

import java.net.URLEncoder;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/*created by Lee Kai Seng*/

public class BriefEmployee extends HashMap<String, String> {
    final static String url = Host.URL + "/Employee";

    public BriefEmployee(String EmployeeId, String DeptId, String EmployeeName,
                         String Role, String Email, String Phone) {
        put("EmployeeId", EmployeeId);
        put("DeptId", DeptId);
        put("EmployeeName", EmployeeName);
        put("Role", Role);
        put("Email", Email);
        put("Phone", Phone);
    }

    public BriefEmployee() {
    }

    public static List<BriefEmployee> getBriefEmployeesUnderSameDeptExclActHead(String email) {
        List<BriefEmployee> briefEmployeeList = new ArrayList<BriefEmployee>();
        try {
            JSONArray a = JSONParser.getJSONArrayFromUrl(url + "/" + email + "/list");
            if (a != null) {
                for (int i = 0; i < a.length(); i++) {
                    JSONObject b = a.getJSONObject(i);
                    BriefEmployee briefEmployee = new BriefEmployee(
                            b.getString("EmployeeId"),
                            b.getString("DeptId"),
                            b.getString("EmployeeName"),
                            b.getString("Role"),
                            b.getString("Email"),
                            b.getString("Phone"));
                    briefEmployeeList.add(briefEmployee);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("listBriefEmployee", "error occurs");
        }
        return briefEmployeeList;
    }

    public static BriefEmployee getBriefEmployee(String EmployeeId) {
        BriefEmployee briefEmployee = null;
        try {
            JSONObject b = JSONParser.getJSONFromUrl(url + "/" + EmployeeId);
            if (b != null) {
                Log.i("EmployeeId", url + "/" + EmployeeId);
                briefEmployee = new BriefEmployee(
                        b.getString("EmployeeId"),
                        b.getString("DeptId"),
                        b.getString("EmployeeName"),
                        b.getString("Role"),
                        b.getString("Email"),
                        b.getString("Phone"));
                Log.i("BriefEmployee", briefEmployee.toString());
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("getBriefEmployee", "error occurs");
        }
        return briefEmployee;
    }

    public static BriefEmployee getBriefEmployeeByEmail(String Email) {
        BriefEmployee briefEmployee = null;
        try {
            JSONObject b = JSONParser.getJSONFromUrl(url + "/" + Email + "/userdetail");
            if (b != null) {
                Log.i("Email", url + Email + "/userdetail");
                briefEmployee = new BriefEmployee(
                        b.getString("EmployeeId"),
                        b.getString("DeptId"),
                        b.getString("EmployeeName"),
                        b.getString("Role"),
                        b.getString("Email"),
                        b.getString("Phone"));
                Log.i("BriefEmployee", briefEmployee.toString());
            }
        } catch (Exception e) {
            e.printStackTrace();
            Log.e("getBriefEmployeeByEmail", "error occurs");
        }
        return briefEmployee;
    }

    public static void Login(SharedPreferences pref) {
        try {
            String email = URLEncoder.encode(pref.getString("Email", "Email"), "UTF-8");
            String password = URLEncoder.encode(pref.getString("Password", "Password"), "UTF-8");
            String credential = String.format("username=%s&password=%s&grant_type=password", email, password);
            String result = JSONParser.postStream(Host.tokenURL, false, credential);
            JSONObject res = new JSONObject(result);
            if (res.has("access_token")) {
                JSONParser.access_token = res.getString("access_token");
            }
        } catch (Exception e) {
            JSONParser.access_token = "";
            Log.e("in Login", e.toString());
        }
    }

    public static void Logout(SharedPreferences pref) {
        try {
            SharedPreferences.Editor editor = pref.edit();
            editor.clear();
            editor.commit();
            JSONParser.access_token = "";
        } catch (Exception e) {
            Log.e("in Logout falied", e.toString());
        }
    }

}