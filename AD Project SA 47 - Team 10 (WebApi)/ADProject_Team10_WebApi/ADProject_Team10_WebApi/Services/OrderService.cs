/* Author: Zhao Pengkai */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADProject_Team10_WebApi.Services
{
    using Models;

    public class OrderService : IOrderService
    {
        public int AddOrder(Order order)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                context.Orders.Add(order);
                return context.SaveChanges(); // 1 row added
            }
            catch (Exception e)
            {
                return 0; // 0 row added
            }
        }

        public List<Order> FindAllOrders()
        {
            SSAEntities context = new SSAEntities();
            List<Order> listOrders = context.Orders.ToList<Order>();
            return listOrders;
        }

        public Order FindOrderById(int poNumber)
        {
            SSAEntities context = new SSAEntities();
            Order order = context.Orders.Where(x => x.PoNumber == poNumber).FirstOrDefault();
            return order;
        }

        public List<Order> FindOrderBySupplierCode(string supplierCode)
        {
            SSAEntities context = new SSAEntities();
            List<Order> orderlist = context.Orders.Where(x => x.SupplierCode == supplierCode).ToList<Order>();
            return orderlist;
        }

        public List<Order> FindOrderByDeliveryDate(DateTime datedelivery)
        {
            SSAEntities context = new SSAEntities();
            List<Order> orderlist = context.Orders.Where(x => x.DateDelivery == datedelivery).ToList<Order>();
            return orderlist;
        }

        public int UpdateOrder(Order order)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                Order o = context.Orders.Where(x => x.PoNumber == order.PoNumber).FirstOrDefault();

                // Dont update poNumber (PK)
                o.DoNumber = order.DoNumber;
                o.SupplierCode = order.SupplierCode;
                o.OrderEmployeeId = order.OrderEmployeeId;
                o.DateDelivery = order.DateDelivery;
                o.DateOrdered = order.DateOrdered;
                o.DateSupply = order.DateSupply;
                o.Status = order.Status;
                return context.SaveChanges(); // 1 row updated
            }
            catch (Exception e)
            {
                return 0; // 0 row updated
            }
        }

        public int DeleteOrderById(int poNumber)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                Order o = context.Orders.Where(x => x.PoNumber == poNumber).FirstOrDefault();
                context.Orders.Remove(o);
                return context.SaveChanges(); // 1 row deleted
            }
            catch (Exception e)
            {
                return 0; // 0 rows deleted
            }
        }
    }
}