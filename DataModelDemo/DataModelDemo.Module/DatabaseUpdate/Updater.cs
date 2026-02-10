using DataModelDemo.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EF;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.Extensions.DependencyInjection;

namespace DataModelDemo.Module.DatabaseUpdate
{
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            // Create Employee 1
            Employee employee1 = ObjectSpace.FirstOrDefault<Employee>(x => x.FirstName == "John" && x.LastName == "Smith");
            if (employee1 == null)
            {
                employee1 = ObjectSpace.CreateObject<Employee>();
                employee1.FirstName = "John";
                employee1.LastName = "Smith";
                employee1.Email = "john.smith@example.com";
                employee1.Birthday = new DateTime(1990, 05, 15);
            }

            // Create Employee 2
            Employee employee2 = ObjectSpace.FirstOrDefault<Employee>(x => x.FirstName == "Jane" && x.LastName == "Doe");
            if (employee2 == null)
            {
                employee2 = ObjectSpace.CreateObject<Employee>();
                employee2.FirstName = "Jane";
                employee2.LastName = "Doe";
                employee2.Email = "jane.doe@example.com";
                employee2.Birthday = new DateTime(1992, 08, 20);
            }

            // Create Demo Task 1 for Employee 1
            DemoTask task1 = ObjectSpace.FirstOrDefault<DemoTask>(x => x.Subject == "Complete Project Documentation");
            if (task1 == null)
            {
                task1 = ObjectSpace.CreateObject<DemoTask>();
                task1.Subject = "Complete Project Documentation";
                task1.Description = "Write comprehensive documentation for the new feature";
                task1.StartDate = DateTime.Now;
                task1.DueDate = DateTime.Now.AddDays(7);
                task1.Status = BusinessObjects.TaskStatus.InProgress;
                task1.PercentCompleted = 50;
                task1.Employee = employee1;
                employee1.DemoTasks.Add(task1);
            }

            // Create Demo Task 2 for Employee 1
            DemoTask task2 = ObjectSpace.FirstOrDefault<DemoTask>(x => x.Subject == "Review Code Changes");
            if (task2 == null)
            {
                task2 = ObjectSpace.CreateObject<DemoTask>();
                task2.Subject = "Review Code Changes";
                task2.Description = "Review and approve the latest pull requests";
                task2.StartDate = DateTime.Now;
                task2.DueDate = DateTime.Now.AddDays(3);
                task2.Status = BusinessObjects.TaskStatus.NotStarted;
                task2.PercentCompleted = 0;
                task2.Employee = employee1;
                employee1.DemoTasks.Add(task2);
            }

            // Create Demo Task 3 for Employee 2
            DemoTask task3 = ObjectSpace.FirstOrDefault<DemoTask>(x => x.Subject == "Update Database Schema");
            if (task3 == null)
            {
                task3 = ObjectSpace.CreateObject<DemoTask>();
                task3.Subject = "Update Database Schema";
                task3.Description = "Implement the new database schema changes";
                task3.StartDate = DateTime.Now;
                task3.DueDate = DateTime.Now.AddDays(10);
                task3.Status = BusinessObjects.TaskStatus.InProgress;
                task3.PercentCompleted = 75;
                task3.Employee = employee2;
                employee2.DemoTasks.Add(task3);
            }

            // Avoid duplicate data
            if (ObjectSpace.GetObjectsCount(typeof(Product), null) > 0)
            {
                return;
            }

            // PRODUCTS
            var laptop = ObjectSpace.CreateObject<Product>();
            laptop.Name = "Laptop";

            var phone = ObjectSpace.CreateObject<Product>();
            phone.Name = "Smart Phone";

            // ACCESSORIES
            var charger = ObjectSpace.CreateObject<Accessory>();
            charger.Name = "Laptop Charger";
            charger.Product = laptop;

            var mouse = ObjectSpace.CreateObject<Accessory>();
            mouse.Name = "Wireless Mouse";
            mouse.Product = laptop;

            var phoneCase = ObjectSpace.CreateObject<Accessory>();
            phoneCase.Name = "Phone Case";
            phoneCase.Product = phone;

            // ORDERS
            var order1 = ObjectSpace.CreateObject<Order>();
            order1.OrderNumber = "ORD-001";
            order1.Product = laptop;
            order1.Accessory = charger;

            var order2 = ObjectSpace.CreateObject<Order>();
            order2.OrderNumber = "ORD-002";
            order2.Product = phone;
            order2.Accessory = phoneCase;
            ObjectSpace.CommitChanges();
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
        }
    }
}
