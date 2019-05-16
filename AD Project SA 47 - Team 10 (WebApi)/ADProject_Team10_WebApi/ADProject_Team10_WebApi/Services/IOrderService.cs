/* Author: Zhao Pengkai */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADProject_Team10_WebApi.Services
{
    using Models;

    interface IOrderService
    {
        int AddOrder(Order order);
        List<Order> FindAllOrders();
        Order FindOrderById(int poNumber);
        List<Order> FindOrderBySupplierCode(string supplierCode);
        List<Order> FindOrderByDeliveryDate(DateTime datedelivery);
        int UpdateOrder(Order order);
        int DeleteOrderById(int poNumber);
    }
}
