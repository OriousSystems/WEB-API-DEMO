using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CustomerDbAccess;
using System.Data.SqlClient;
using System.Configuration;

namespace CustomerService.Controllers
{
    public class CustomerController : ApiController
    {
        // GET api/Customer
        public IEnumerable<Customer> Get()
        {            
            CustomerDbEntities entities = new CustomerDbEntities();
            var list = from c in entities.Customers
                       select c;
            return list;

        }

        // GET api/Customer/
        public IEnumerable<Customer> GetCustomer(int id)
        {            
            CustomerDbEntities entities = new CustomerDbEntities();
            var list = from g in entities.Customers
                       where  g.ID==id
                       select g;
            return list;
        }

        // POST api/customer
        [HttpPost]
        public HttpResponseMessage Post(Customer cust)
        {            
            CustomerDbEntities entity = new CustomerDbEntities();
            entity.Customers.Add(cust);
            entity.SaveChanges();
            var response = Request.CreateResponse(HttpStatusCode.OK, cust);
            string url = Url.Link("DefaultApi", new { cust.ID });
            response.Headers.Location = new Uri(url);
            return response;
        }

        // DELETE api/customer/
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            CustomerDbEntities entit = new CustomerDbEntities();         
            var del = entit.Customers.Where(d => d.ID == id).FirstOrDefault();

            entit.Customers.Remove(del);
            entit.SaveChanges();
            var response = Request.CreateResponse(HttpStatusCode.OK, id);
            return response;
        }

        //PUT api/Customer/
        [HttpPut]
        public HttpResponseMessage Put(Customer cust,int id)
        {
            
            CustomerDbEntities entit = new CustomerDbEntities();
            var update = (from a in entit.Customers
                          where a.ID == id
                          select a).FirstOrDefault();
            if(update!=null)
            {
                update.FIRSTNAME= cust.FIRSTNAME;
                update.LASTNAME= cust.LASTNAME;
                update.GENDER= cust.GENDER;
                entit.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK,update);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,"invalid id");
            }
        }
        
    }
}
