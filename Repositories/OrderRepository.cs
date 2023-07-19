using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {

        public OrderRepository(RepositoryContext context) : base(context)
        {
        }

        public IQueryable<Order> Orders => _context.Orders
            .Include(o=>o.Lines)
            .ThenInclude(c1=>c1.Product)
            .OrderBy(o=>o.Shipped)
            .ThenByDescending(o=>o.OrderId);//son verilen siparişleri görmek adına

        public int NumberOfInProcess => _context.Orders
            .Count(o=>o.Shipped.Equals(false));//gönderimde olmayan kayıtlar

        public void Complate(int id)
        {
            //tamamlanan siparişin  Shipped alanını update yapıcağız
            var order = FindByCondition(o => o.OrderId.Equals(id),true);
            if (order is null)
                throw new Exception("Order Could Not found!");
            order.Shipped = true;
           
        }

        public Order? GetOneOrder(int id)
            => FindByCondition(o=> o.OrderId.Equals(id),false);

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l=>l.Product));
            if(order.OrderId==0) 
                _context.Orders.Add(order);
           
        }
    }
}
