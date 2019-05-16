/* Author: Shalin Christina Stephen Selvaraja */

using ADProject_Team10_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADProject_Team10_WebApi.Services
{
    interface ICollectionPointService
    {
        CollectionPoint GetCollectionPointById(String id);
        List<String> GetAllCollectionPointName();
        List<CollectionPoint> GetAllCollectionPoint();
        CollectionPoint GetCollectionPointByName(String name);
        int AddCollectionPoint(CollectionPoint c);
        int UpdateCollectionPoint(CollectionPoint c);
        int DeleteCollectionPoint(string id);
        List<CollectionPoint> GetCollectionPointsByEmpId(int empId);
    }
}
