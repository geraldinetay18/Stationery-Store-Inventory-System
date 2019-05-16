/* Author: Nguyen Ngoc Doan Trang */

using ADProject_Team10.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ADProject_Team10.BizLogic
{
    public class AssignDepartment
    {
        public static List<StoreClerkDepartmentAssignDept> getStoreClerkDepartmentList()
        {
            List<StoreClerkDepartmentAssignDept> storeClerkDepartments = new List<StoreClerkDepartmentAssignDept>();

            String connectionString = "Data Source=.;Initial Catalog=SSIS10;Integrated Security=True";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            String query = "SELECT Department.StoreClerkId as StoreClerkId, Employee.EmployeeName as EmployeeName, Department.DeptId as DeptId, Department.DeptName as DeptName " +
                "FROM Department INNER JOIN Employee ON Department.StoreClerkId = Employee.EmployeeId " +
                "WHERE Department.DeptId <> 'STOR' " +
                "ORDER BY Department.DeptId";
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                StoreClerkDepartmentAssignDept storeClerkDepartment = new StoreClerkDepartmentAssignDept();

                int storeClerkId = reader.GetInt32(0);
                String employeeName = reader.GetString(1);
                String DeptId = reader.GetString(2);
                String DeptName = reader.GetString(3);

                storeClerkDepartment.StoreClerkId1 = storeClerkId;
                storeClerkDepartment.StoreClerkName1 = employeeName;
                storeClerkDepartment.DeptId1 = DeptId;
                storeClerkDepartment.DeptName1 = DeptName;

                storeClerkDepartments.Add(storeClerkDepartment);
            }
            connection.Close();

            return storeClerkDepartments;
        }

        public static void updateStoreClerkDepartment(int storeClerkId, String deptId)
        {
            String connectionString = "Data Source=.;Initial Catalog=SSIS10;Integrated Security=True";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            String query = "UPDATE Department " +
                "SET Department.StoreClerkId = " + storeClerkId +
                "WHERE Department.DeptId = '" + deptId + "'";
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();
            connection.Close();
        }
    }
}