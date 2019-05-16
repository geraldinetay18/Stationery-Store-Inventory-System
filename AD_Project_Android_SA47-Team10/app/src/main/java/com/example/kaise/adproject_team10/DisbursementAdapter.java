package com.example.kaise.adproject_team10;

import android.widget.ArrayAdapter;

import android.app.Activity;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.kaise.adproject_team10.Entities.Disbursement;

import java.util.List;

/*created by Nguyen Ngoc Doan Trang*/

public class DisbursementAdapter extends ArrayAdapter<Disbursement> {

    private List<Disbursement> items;
    int resource;

    public DisbursementAdapter(Context context, List<Disbursement> items) {
        super(context, R.layout.row, items);
        this.resource = R.layout.row;
        this.items = items;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        LayoutInflater inflater = (LayoutInflater) getContext().getSystemService(Activity.LAYOUT_INFLATER_SERVICE);
        View v = inflater.inflate(resource, null);
        Disbursement disbursement = items.get(position);

        if (disbursement != null) {

            int[] dest1 = new int[]{R.id.textView1};
            String[] src1 = new String[]{"DateOfDisbursement"};

            int[] dest2 = new int[]{R.id.tvMessage};
            String[] src2 = new String[]{"Status"};

            for (int n = 0; n < dest1.length; n++) {

                TextView txt1 = v.findViewById(dest1[n]);
                txt1.setText(disbursement.get(src1[n]));

                TextView txt2 = v.findViewById(dest2[n]);
                txt2.setText(disbursement.get(src2[n]));
            }
        }
        return v;
    }
}
