/* Author: Shalin Christina Stephen Selvaraja */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADProject_Team10_WebApi.Models;

namespace ADProject_Team10_WebApi.Services
{
    public class CollectionPointService : ICollectionPointService
    {
        SSAEntities se = new SSAEntities();
        public CollectionPoint GetCollectionPointById(string id)
        {
            CollectionPoint cp = se.CollectionPoints.Where(x => x.LocationId == id).First();
            return cp;
        }
        public List<String> GetAllCollectionPointName()
        {
            return (se.CollectionPoints.Select(x => x.LocationName).ToList());
        }

        public List<CollectionPoint> GetAllCollectionPoint()
        {
            return (se.CollectionPoints.ToList());
        }

        public CollectionPoint GetCollectionPointByName(string name)
        {
            return (se.CollectionPoints.Where(x => x.LocationName.Contains(name)).FirstOrDefault());
        }

        public int AddCollectionPoint(CollectionPoint c)
        {
            int result = -1;
            try
            {
                se.CollectionPoints.Add(c);
                result = se.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateCollectionPoint(CollectionPoint c)
        {
            CollectionPoint collectionPoint = se.CollectionPoints.Where(x => x.LocationId == c.LocationId).FirstOrDefault();
            if (collectionPoint != null)
            {
                collectionPoint.LocationName = c.LocationName;
                collectionPoint.Time = c.Time;
                collectionPoint.EmployeeId = c.EmployeeId;
                return se.SaveChanges();
            }
            else
                return 0;
        }
        public int DeleteCollectionPoint(string id)
        {
            try
            {
                CollectionPoint collectionPoint = se.CollectionPoints.Where(x => x.LocationId == id).FirstOrDefault();
                se.CollectionPoints.Remove(collectionPoint);
                se.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public List<CollectionPoint> GetCollectionPointsByEmpId(int empId)
        {
            return se.CollectionPoints.Where(x => x.EmployeeId == empId).ToList();
        }
    }
}