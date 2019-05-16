/* Author: Zhao Pengkai */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADProject_Team10_WebApi.Services
{
    using Models;
    interface IOrderDetailsService
    {
        int AddOrderDetail(OrderDetail orderDetail);
        List<OrderDetail> FindAllOrderDetails();
        OrderDetail FindOrderDetailById(int id);
        List<OrderDetail> FindOrderDetailByPONumber(int poNumber);
        int UpdateOrderDetail(OrderDetail orderDetail);
        int DeleteOrderDetailById(int id);
    }
}
