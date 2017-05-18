using CustomerManager.Model;
using CustomerManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CustomerManager.Controllers {
	public class DataServiceController : ApiController {

		[HttpGet]
		public HttpResponseMessage Customers(int top, int skip) {
			using (CustomerManagerEntities db = new CustomerManagerEntities()) {
				Model.Customer[] customers = db.Customers.ToArray()
					.Select(c => new Model.Customer(c)).ToArray();
				HttpContext.Current.Response.Headers.Add("X-InlineCount", customers.Length.ToString());
				return Request.CreateResponse(HttpStatusCode.OK, customers.Skip(skip).Take(top));
			}
		}

		[HttpGet]
		public HttpResponseMessage CustomersSummary(int top, int skip) {
			using (CustomerManagerEntities db = new CustomerManagerEntities()) {
				CustomerSummary[] custSummary = db.Customers.OrderBy(c => c.LastName).Select(c => new CustomerSummary {
					Id = c.CustomerID,
					FirstName = c.FirstName,
					LastName = c.LastName,
					City = c.City,
					State = new Model.State {
						Id = c.State.StateID,
						Abbreviation = c.State.Abbreviation,
						Name = c.State.Name
					},
					OrderCount = c.Orders.Count(),
					GenderID = c.GenderID ?? 1
				}).ToArray();
				HttpContext.Current.Response.Headers.Add("X-InlineCount", custSummary.Length.ToString());
				return Request.CreateResponse(HttpStatusCode.OK, custSummary.Skip(skip).Take(top));
			}
		}

		[HttpGet]
		public HttpResponseMessage States() {
			using (CustomerManagerEntities db = new CustomerManagerEntities()) {
				Model.State[] states = db.States.OrderBy(s => s.Name).Select(s => new Model.State {
					Id = s.StateID,
					Abbreviation = s.Abbreviation,
					Name = s.Name
				}).ToArray();
				return Request.CreateResponse(HttpStatusCode.OK, states);
			}
		}

		[HttpGet]
		public HttpResponseMessage CheckUnique(int id, string property, string value) {
			using (CustomerManagerEntities db = new CustomerManagerEntities()) {
				OperationStatus opStatus;
				switch (property.ToLower()) {
					case "email":
						bool unique = !db.Customers.Any(c => c.CustomerID != id && c.Email == value);
						opStatus = new OperationStatus { Status = unique };
						break;
					default:
						opStatus = new OperationStatus();
						break;
				}
				return Request.CreateResponse(HttpStatusCode.OK, opStatus);
			}
		}

		[HttpPost]
		public HttpResponseMessage Login([FromBody]UserLogin userLogin) {
			//Simulated login
			return Request.CreateResponse(HttpStatusCode.OK, new { status = true });
		}

		[HttpPost]
		public HttpResponseMessage Logout() {
			//Simulated logout
			return Request.CreateResponse(HttpStatusCode.OK, new { status = true });
		}

		// GET api/<controller>/5
		[HttpGet]
		public HttpResponseMessage CustomerById(int id) {
			using (CustomerManagerEntities db = new CustomerManagerEntities()) {
				Repository.Customer customerEntity = db.Customers.SingleOrDefault(c => c.CustomerID == id);
				if (customerEntity == null)
					return Request.CreateResponse(HttpStatusCode.NotFound);
				return Request.CreateResponse(HttpStatusCode.OK, new Model.Customer(customerEntity));
			}
		}

		// POST api/<controller>
		public HttpResponseMessage PostCustomer([FromBody]Model.Customer customer) {
			using (CustomerManagerEntities db = new CustomerManagerEntities()) {
				OperationStatus opStatus = new OperationStatus { Status = true };
				Repository.Customer newCustomer = new Repository.Customer {
					FirstName = customer.FirstName,
					LastName = customer.LastName,
					Email = customer.Email,
					Address = customer.Address,
					City = customer.City,
					StateID = customer.StateId,
					Zip = customer.Zip,
					GenderID = (int)customer.Gender
				};
				try {
					db.Customers.Add(newCustomer);
					db.SaveChanges();
				} catch (Exception exp) {
					opStatus.Status = false;
					opStatus.ExceptionMessage = exp.Message;
				}
				if (opStatus.Status) {
					var response = Request.CreateResponse<Model.Customer>(HttpStatusCode.Created, customer);
					string uri = Url.Link("DefaultApi", new { id = customer.Id });
					response.Headers.Location = new Uri(uri);
					return response;
				}
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
			}
		}

		// PUT api/<controller>/5
		public HttpResponseMessage PutCustomer(int id, [FromBody]Model.Customer customer) {
			using (CustomerManagerEntities db = new CustomerManagerEntities()) {
				OperationStatus opStatus;
				Repository.Customer custEntity = db.Customers.SingleOrDefault(c => c.CustomerID == id);
				if (custEntity == null)
					opStatus = new OperationStatus {
						Status = false,
						ExceptionInnerMessage = "Customer not found"
					};
				else {
					try {
						custEntity.FirstName = customer.FirstName;
						custEntity.LastName = customer.LastName;
						custEntity.Email = customer.Email;
						custEntity.Address = customer.Address;
						custEntity.City = customer.City;
						custEntity.StateID = customer.StateId;
						custEntity.Zip = customer.Zip;
						custEntity.GenderID = (int)customer.Gender;
						db.SaveChanges();
						opStatus = new OperationStatus { Status = true };
					} catch (Exception e) {
						opStatus = new OperationStatus {
							Status = false,
							ExceptionMessage = e.Message
						};
					}
				}
				if (opStatus.Status)
					return Request.CreateResponse<Model.Customer>(HttpStatusCode.Accepted, customer);
				return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
			}
		}

		// DELETE api/<controller>/5
		public HttpResponseMessage DeleteCustomer(int id) {
			using (CustomerManagerEntities db = new CustomerManagerEntities()) {
				var opStatus = new OperationStatus { Status = true };
				try {
					var cust = db.Customers.SingleOrDefault(c => c.CustomerID == id);
					if (cust != null) {
						db.Customers.Remove(cust);
						db.SaveChanges();
					} else {
						opStatus.Status = false;
						opStatus.ExceptionMessage = "Customer not found";
					}
				} catch (Exception exp) {
					opStatus.Status = false;
					opStatus.ExceptionMessage = exp.Message;
				}
				if (opStatus.Status) {
					return Request.CreateResponse(HttpStatusCode.OK);
				} else {
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
				}
			}
		}

		/*[HttpGet]
		public HttpResponseMessage BootstrapData() {
			using (CustomerManagerEntities db = new CustomerManagerEntities()) {
				try {
					DataInitializer.Initialize(db);
					db.SaveChanges();
					return Request.CreateResponse(HttpStatusCode.OK);
				} catch(Exception e) {
					return Request.CreateResponse(HttpStatusCode.InternalServerError, OperationStatus.CreateFromException(e.Message, e));
				}
			}
		}*/
	}
}