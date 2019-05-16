/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADProject_Team10.Models
{
    public class StoreClerkDepartmentAssignDept
    {
        private int StoreClerkId;

        private String StoreClerkName;

        private String DeptId;

        private String DeptName;

        public int StoreClerkId1 { get => StoreClerkId; set => StoreClerkId = value; }
        public string StoreClerkName1 { get => StoreClerkName; set => StoreClerkName = value; }
        public string DeptId1 { get => DeptId; set => DeptId = value; }
        public string DeptName1 { get => DeptName; set => DeptName = value; }
    }
}