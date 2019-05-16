/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using ADProject_Team10.Models;

namespace ADProject_Team10.BizLogic
{
    public class StationeryCollectionLogic
    {
        SSAEntities ssae = new SSAEntities();

        public static List<Disbursement> getDisbursement(string deptId)
        {
            using (SSAEntities ssae = new SSAEntities())
            {

                return ssae.Disbursements.Where(x => x.DeptId.Equals(deptId)).OrderByDescending(x => x.DateDisbursed).ToList<Disbursement>();
            }
        }

        public static List<DisbursementDetail> getDisbursement(int disbursementId)
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                return ssae.DisbursementDetails.Where(x => x.DisbursementId.Equals(disbursementId)).OrderByDescending(x => x.DisbursementDetailsId).ToList<DisbursementDetail>();
            }
        }

        public static List<StationeryCollectionList> getStationeryCollectionList(string deptId)
        {
            List<StationeryCollectionList> stationeryCollectionLists = new List<StationeryCollectionList>();

            List<Disbursement> disbursements = getDisbursement(deptId);

            int no = 1;

            for (int i = 0; i < disbursements.Count; i++)
            {
                StationeryCollectionList stationeryCollectionList = new StationeryCollectionList();

                stationeryCollectionList.No = no;
                stationeryCollectionList.DisbursementId = disbursements[i].DisbursementId;

                if (disbursements[i].DateDisbursed != null)
                {
                    DateTime dateTime = (DateTime)disbursements[i].DateDisbursed;
                    string dateString = dateTime.ToString("dd MMM yyyy");
                    stationeryCollectionList.DateOfDisbursement = dateString;
                }

                stationeryCollectionList.Status = disbursements[i].Status;
                stationeryCollectionList.Remark = disbursements[i].Remark;

                stationeryCollectionLists.Add(stationeryCollectionList);

                no++;
            }

            return stationeryCollectionLists;
        }

        public static List<StationeryCollectionList> getStationeryCollectionListByYear(String year, String deptId)
        {
            List<StationeryCollectionList> stationeryCollectionLists = new List<StationeryCollectionList>();

            List<Disbursement> disbursements = getDisbursement(deptId);

            int no = 1;

            for (int i = 0; i < disbursements.Count; i++)
            {
                if (disbursements[i].DateDisbursed != null)
                {
                    StationeryCollectionList stationeryCollectionList = new StationeryCollectionList();

                    stationeryCollectionList.No = no;

                    DateTime dateTime = (DateTime)disbursements[i].DateDisbursed;
                    string currentYear = dateTime.ToString("yyyy");

                    if (currentYear == year)
                    {
                        stationeryCollectionList.DisbursementId = disbursements[i].DisbursementId;
                        string dateString = dateTime.ToString("dd MMM yyyy");
                        stationeryCollectionList.DateOfDisbursement = dateString;

                        stationeryCollectionList.Status = disbursements[i].Status;
                        stationeryCollectionList.Remark = disbursements[i].Remark;

                        stationeryCollectionLists.Add(stationeryCollectionList);

                        no++;
                    }
                }
            }
            return stationeryCollectionLists;
        }

        public static List<StationeryCollectionList> getStationeryCollectionListByDate(DateTime date, String deptId)
        {
            List<StationeryCollectionList> StationeryCollectionLists = new List<StationeryCollectionList>();

            List<Disbursement> disbursements = getDisbursement(deptId);

            int no = 1;

            for (int i = 0; i < disbursements.Count; i++)
            {

                if (disbursements[i].DateDisbursed != null)
                {
                    StationeryCollectionList stationeryCollectionList = new StationeryCollectionList();

                    stationeryCollectionList.No = no;

                    DateTime dateTime = (DateTime)disbursements[i].DateDisbursed;

                    if (dateTime == date)
                    {
                        stationeryCollectionList.DisbursementId = disbursements[i].DisbursementId;
                        string dateString = dateTime.ToString("dd MMM yyyy");
                        stationeryCollectionList.DateOfDisbursement = dateString;

                        stationeryCollectionList.Status = disbursements[i].Status;
                        stationeryCollectionList.Remark = disbursements[i].Remark;

                        StationeryCollectionLists.Add(stationeryCollectionList);

                        no++;
                    }
                }
            }
            return StationeryCollectionLists;
        }

        public static List<StationeryCollectionDetails> getStationeryCollectionDetailsLists(int disbursementId)
        {
            Disbursement disbursement = getFirstDisbursement(disbursementId.ToString());

            List<StationeryCollectionDetails> stationerycollectiondetailsLists = new List<StationeryCollectionDetails>();

            String connectionString = "Data Source=.;Initial Catalog=SSIS10;Integrated Security=True";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            String query = " SELECT Stationery.Description, DisbursementDetails.QuantityRequested, DisbursementDetails.QuantityReceived, Disbursement.Status, Disbursement.DateCreated " +
                " FROM Disbursement INNER JOIN DisbursementDetails ON Disbursement.DisbursementId = DisbursementDetails.DisbursementId " +
                " INNER JOIN Stationery ON Stationery.ItemCode = DisbursementDetails.ItemCode " +
                " WHERE DisbursementDetails.DisbursementId = " + disbursementId +
                " ORDER BY DisbursementDetails.DisbursementDetailsId ";
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                StationeryCollectionDetails stationerycollectiondetailsList = new StationeryCollectionDetails();

                String stationeryDescription = reader.GetString(0);
                int quantityNeed = reader.GetInt32(1);
                int QuantityReceived = reader.GetInt32(2);
                String status = disbursement.Status;
                DateTime requestDate = reader.GetDateTime(4);
                string requestDatetring = requestDate.ToString("dd MMM yyyy");

                stationerycollectiondetailsList.StationeryDescription = stationeryDescription;
                stationerycollectiondetailsList.QuantityNeed = quantityNeed;
                stationerycollectiondetailsList.QuantityDisbursed = QuantityReceived;
                stationerycollectiondetailsList.Status = status;
                stationerycollectiondetailsList.RequestDate = requestDatetring;

                stationerycollectiondetailsLists.Add(stationerycollectiondetailsList);
            }
            connection.Close();

            return stationerycollectiondetailsLists;
        }

        public static Disbursement getFirstDisbursement(String disbursementId)
        {
            int Id = Convert.ToInt32(disbursementId);

            SSAEntities ssae = new SSAEntities();
            var qry = from x in ssae.Disbursements.Where(x => x.DisbursementId.Equals(Id)) select x;
            Disbursement disbursement = qry.First<Disbursement>();

            return disbursement;
        }

        public static void acknowledge(String disbursementId, String remark)
        {
            int Id = Convert.ToInt32(disbursementId);

            String connectionString = "Data Source=.;Initial Catalog=SSIS10;Integrated Security=True";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            String query = " UPDATE [SSIS10].[dbo].[Disbursement] " +
                " SET [SSIS10].[dbo].[Disbursement].Status = 'Acknowledged', [SSIS10].[dbo].[Disbursement].Remark = '" + remark + "', " +
                "[SSIS10].[dbo].[Disbursement].DateDisbursed = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                " WHERE [SSIS10].[dbo].[Disbursement].DisbursementId = " + disbursementId;
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();
            connection.Close();
        }
    }
}