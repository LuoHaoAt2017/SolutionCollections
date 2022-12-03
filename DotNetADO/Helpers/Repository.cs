using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DotNetADO.Models;

namespace DotNetADO.Helpers
{
	public class Repository
	{
		public static IEnumerable<Customer> GetCustomers(int count = 1)
		{
			Randomizer.Seed = new Random(123456);
			var generator = new Faker<Customer>()
				.RuleFor(c => c.customerId, Guid.NewGuid().ToString())
				.RuleFor(c => c.customerName, f => f.Name.FullName())
				.RuleFor(c => c.contactName, f => f.Company.CompanyName())
				.RuleFor(c => c.country, f => f.Address.Country())
				.RuleFor(c => c.address, f => f.Address.FullAddress())
				.RuleFor(c => c.city, f => f.Address.City())
				.RuleFor(c => c.postalCode, f => f.Address.ZipCode());
			return generator.Generate(count);
		}
	}
}
