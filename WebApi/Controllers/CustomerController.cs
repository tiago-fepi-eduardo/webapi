using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        // GET api/values
        public IHttpActionResult Get()
        {
            List<CustomerViewModel> oList = new List<CustomerViewModel>();

            using (var ctx = new CustomerConnection())
            {
                foreach (var item in ctx.Customers)
                {
                    oList.Add(
                        new CustomerViewModel()
                        {
                            Name = item.Name,
                            Address = item.Address,
                            Birthday = item.Birthday,
                            CostumerId = item.CustomerId,
                            EmailId = item.EmailId,
                            MobileNo = item.MobileNo
                        });
                }
            }

            if (oList.Count == 0)
            {
                return NotFound();
            }

            return Ok(oList);
        }

        public IHttpActionResult Post()
        {
            using (var ctx = new CustomerConnection())
            {
                ctx.Customers.Add(new Customer()
                {
                    Name = "Eduardo",
                    Address = "Rua Das Margaridas",
                    EmailId = "@google.com",
                    MobileNo = "1199993333",
                    Birthday = DateTime.Now
                });

                ctx.SaveChanges();
            }
            
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new CustomerConnection())
            {
                var customer = ctx.Customers
                    .Where(s => s.CustomerId == id)
                    .FirstOrDefault();

                ctx.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new CustomerConnection())
            {
                var existingCustomer = ctx.Customers.Where(s => s.CustomerId == customer.CostumerId)
                                                        .FirstOrDefault<Customer>();

                if (existingCustomer != null)
                {
                    existingCustomer.Name = customer.Name;
                    existingCustomer.EmailId = customer.EmailId;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.MobileNo = customer.MobileNo;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
    }
}
