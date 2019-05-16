/* Author: Zhao Pengkai */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADProject_Team10_WebApi.Models;

namespace ADProject_Team10_WebApi.Services
{
    public class StationeryService : IStationeryService
    {
        public int AddStationery(Stationery stationery)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                context.Stationeries.Add(stationery);
                return context.SaveChanges(); // 1 row added
            }
            catch (Exception e)
            {
                return 0; // 0 row added
            }
        }

        public List<Stationery> FindAllStationery()
        {
            SSAEntities context = new SSAEntities();
            List<Stationery> listStationery = context.Stationeries.ToList();
            return listStationery;
        }

        public Stationery FindStationeryById(string code)
        {
            SSAEntities context = new SSAEntities();
            Stationery stationery = context.Stationeries.Where(x => x.ItemCode == code).FirstOrDefault();
            return stationery;
        }

        public List<Stationery> FindStationeryByName(string name)
        {
            SSAEntities context = new SSAEntities();
            List<Stationery> stationerylist = context.Stationeries.Where(x => x.Description.Contains(name)).ToList<Stationery>();
            return stationerylist;
        }

        public int UpdateStationery(Stationery stationery)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                Stationery s = context.Stationeries.Where(x => x.ItemCode == stationery.ItemCode).FirstOrDefault();

                // Dont update itemCode (PK)
                s.CategoryId = stationery.CategoryId;
                s.Description = stationery.Description;
                s.QuantityInStock = stationery.QuantityInStock;
                s.QuantityReorder = stationery.QuantityReorder;
                s.ReorderLevel = stationery.ReorderLevel;
                s.UnitOfMeasure = stationery.UnitOfMeasure;
                s.Bin = stationery.Bin;
                s.AdjustmentRemark = stationery.AdjustmentRemark;
                return context.SaveChanges(); // 1 row updated
            }
            catch (Exception e)
            {
                return 0; // 0 row updated
            }
        }

        public int DeleteStationeryById(string code)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                Stationery stationery = context.Stationeries.Where(x => x.ItemCode == code).FirstOrDefault();
                context.Stationeries.Remove(stationery);
                return context.SaveChanges(); // 1 row deleted
            }
            catch (Exception e)
            {
                return 0; // 0 rows deleted
            }
        }

        public List<string> FindAllStationeryName()
        {
            SSAEntities context = new SSAEntities();
            List<String> stationery = new List<string>();
            var query = from sta in context.Stationeries select new { sta.Description };
            for (int i = 0; i < query.Count(); i++)
            {
                stationery.Add(query.ElementAt(i).Description);
            }
            return stationery;
        }

        public List<Stationery> FindStationeryByCatagoryName(string name)
        {
            SSAEntities context = new SSAEntities();
            int categoryId = context.Categories.Where(x => x.CategoryName.Contains(name)).Select(x => x.CategoryId).FirstOrDefault();
            List<Stationery> stationery = context.Stationeries.Where(x => x.CategoryId == categoryId).ToList();
            return stationery;
        }

        public List<Stationery> FindAllStationeryByCategory(int categoryId)
        {
            SSAEntities context = new SSAEntities();

            return context.Stationeries.Where(x => x.CategoryId == categoryId).ToList();
        }

        public int FindQuantityInStockById(string code)
        {
            SSAEntities context = new SSAEntities();
            return context.Stationeries.Where(x => x.ItemCode == code).Select(x => x.QuantityInStock).FirstOrDefault();
        }

        public string FindLastStationeryId()
        {
            SSAEntities s = new SSAEntities();
            Stationery stationery = s.Stationeries.OrderByDescending(x => x.ItemCode).FirstOrDefault();
            return stationery.ItemCode;
        }

        public int AddCategory(Category category)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                context.Categories.Add(category);
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<Category> FindAllCategory()
        {
            SSAEntities ssa = new SSAEntities();
            return ssa.Categories.ToList();
        }

        public int CheckItemCodeUniqueness(string itemcode)
        {
            SSAEntities ssa = new SSAEntities();
            try
            {
                Stationery s = ssa.Stationeries.Where(x => x.ItemCode == itemcode).First();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public Category FindCategoryByName(string name)
        {
            SSAEntities ssa = new SSAEntities();
            try
            {
                return ssa.Categories.Where(x => x.CategoryName == name).First();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int CheckDescriptionUniqueness(string name)
        {
            SSAEntities ssa = new SSAEntities();
            try
            {
                Stationery s = ssa.Stationeries.Where(x => x.Description == name).First();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<Stationery> FindAllLowStock()
        {
            SSAEntities ssa = new SSAEntities();
            return ssa.Stationeries.Where(x => x.QuantityInStock <= x.ReorderLevel).ToList();
        }

        public bool ValidateLowStock(string itemcode)
        {
            Stationery s = FindStationeryById(itemcode);
            return (s != null && (s.QuantityInStock <= s.ReorderLevel)) ? true : false;
        }
    }
}