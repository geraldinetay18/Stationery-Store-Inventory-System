/* Author: Zhao Pengkai */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADProject_Team10_WebApi.Models;

namespace ADProject_Team10_WebApi.Services
{
    public class OrderDetailService : IOrderDetailsService
    {
        public int AddOrderDetail(OrderDetail orderDetail)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                context.OrderDetails.Add(orderDetail);
                return context.SaveChanges(); // returns 1
            }
            catch (Exception e)
            {
                return 0; // 0 rows added
            }
        }

        public List<OrderDetail> FindAllOrderDetails()
        {
            SSAEntities context = new SSAEntities();
            List<OrderDetail> listOrderDetails = context.OrderDetails.ToList();
            return listOrderDetails;
        }

        public OrderDetail FindOrderDetailById(int id)
        {
            SSAEntities context = new SSAEntities();
            OrderDetail orderDetail = context.OrderDetails.Where(x => x.OrderDetailsId == id).FirstOrDefault();
            return orderDetail;
        }

        public List<OrderDetail> FindOrderDetailByPONumber(int poNumber)
        {
            SSAEntities context = new SSAEntities();
            List<OrderDetail> orderDetail = context.OrderDetails.Where(x => x.PoNumber == poNumber).ToList<OrderDetail>();
            return orderDetail;
        }

        public int UpdateOrderDetail(OrderDetail orderDetail)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                OrderDetail o = context.OrderDetails.Where(x => x.OrderDetailsId == orderDetail.OrderDetailsId).FirstOrDefault();

                // Only need to update quantity and remark
                o.Quantity = orderDetail.Quantity;
                o.Remark = orderDetail.Remark;

                return context.SaveChanges(); // 1 row updated
            }
            catch (Exception e)
            {
                return 0; // 0 row updated
            }
        }

        public int DeleteOrderDetailById(int id)
        {
            SSAEntities context = new SSAEntities();
            try
            {
                OrderDetail o = context.OrderDetails.Where(x => x.OrderDetailsId == id).FirstOrDefault();
                context.OrderDetails.Remove(o);
                return context.SaveChanges(); // 1 row deleted
            }
            catch (Exception e)
            {
                return 0; // 0 rows deleted
            }
        }
    }
}