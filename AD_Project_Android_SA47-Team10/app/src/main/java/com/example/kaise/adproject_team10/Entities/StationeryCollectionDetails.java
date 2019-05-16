package com.example.kaise.adproject_team10.Entities;

import java.util.HashMap;
import java.util.List;

/*created by Nguyen Ngoc Doan Trang*/

public class StationeryCollectionDetails {

    private String date;
    private String collectionPoint;
    private String status;
    private List<Item> items;

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public String getCollectionPoint() {
        return collectionPoint;
    }

    public void setCollectionPoint(String collectionPoint) {
        this.collectionPoint = collectionPoint;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public List<Item> getItems() {
        return items;
    }

    public void setItems(List<Item> items) {
        this.items = items;
    }
}