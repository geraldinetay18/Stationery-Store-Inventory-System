/* Author: Zhao Pengkai */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADProject_Team10_WebApi.Services
{
    using Models;
    interface IStationeryService
    {
        int AddStationery(Stationery stationery);
        List<Stationery> FindAllStationery();
        Stationery FindStationeryById(string code);
        List<Stationery> FindStationeryByName(string name);
        int UpdateStationery(Stationery stationery);
        int DeleteStationeryById(string code);
        List<string> FindAllStationeryName();
        List<Stationery> FindStationeryByCatagoryName(string name);
        List<Stationery> FindAllStationeryByCategory(int categoryId);
        int FindQuantityInStockById(string code);
        string FindLastStationeryId();
        int AddCategory(Category category);
        List<Category> FindAllCategory();
        int CheckItemCodeUniqueness(string itemcode);
        Category FindCategoryByName(string name);
        int CheckDescriptionUniqueness(string name);
        List<Stationery> FindAllLowStock();
        bool ValidateLowStock(string itemcode);
    }
}
