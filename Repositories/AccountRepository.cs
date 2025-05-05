using Api.Models;
using Bogus;

namespace Api.Repositories;

internal static class AccountRepository
{
    internal static List<Account> Accounts = [];

    static AccountRepository()
    {
        var adminFaker = new Faker<Admin>()
            .RuleFor(oo => oo.FirstName, f => f.Person.FirstName)
            .RuleFor(oo => oo.AccountType, _ => AccountType.Admin)
            .RuleFor(oo => oo.LastName, f => f.Person.LastName);

        var index = 1;
        foreach (var admin in adminFaker.Generate(10))
        {
            admin.Id = index;
            Accounts.Add(admin);
            index++;
        }

        var customerFaker = new Faker<Customer>()
            .RuleFor(oo => oo.FirstName, f => f.Person.FirstName)
            .RuleFor(oo => oo.AccountType, _ => AccountType.Customer)
            .RuleFor(oo => oo.LastName, f => f.Person.LastName);

        foreach (var customer in customerFaker.Generate(15))
        {
            customer.Id = index;
            Accounts.Add(customer);
            index++;
        }

        var accountGroupFaker = new Faker<AccountGroup>()
            .RuleFor(oo => oo.AccountType, _ => AccountType.AccountGroup)
            .RuleFor(oo => oo.Name, f => f.Company.CompanyName());

        foreach (var accountGroup in accountGroupFaker.Generate(1))
        {
            accountGroup.Id = index;

            var admins = Accounts.Where(x => x.AccountType == AccountType.Admin).OrderBy(_ => Guid.NewGuid()).Take(2);
            var customers = Accounts.Where(x => x.AccountType == AccountType.Customer).OrderBy(_ => Guid.NewGuid()).Take(3);

            accountGroup.Accounts.AddRange(admins);
            accountGroup.Accounts.AddRange(customers);

            Accounts.Add(accountGroup);
            index++;
        }
    }
}