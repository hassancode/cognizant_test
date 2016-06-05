using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerManager.Model {
	public class Customer {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public State State { get; set; }
		public int? StateId { get; set; }
		public string Zip { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public Gender Gender { get; set; }
		public IEnumerable<Order> Orders { get; set; }

		public Customer() { }
		public Customer(Repository.Customer c) {
			Id = c.CustomerID;
			FirstName = c.FirstName;
			LastName = c.LastName;
			Email = c.Email;
			Address = c.Address;
			City = c.City;
			StateId = c.StateID;
			State = new Model.State {
				Id = c.State.StateID,
				Abbreviation = c.State.Abbreviation,
				Name = c.State.Name
			};
			Zip = c.Zip;
			Orders = c.Orders.Select(o => new Model.Order {
				Id = o.OrderID,
				Product = o.Product,
				Price = o.Price,
				Quantity = o.Quantity,
				Date = o.Date
			}).ToArray();
		}
	}

	public enum Gender {
		Female,
		Male
	}
}