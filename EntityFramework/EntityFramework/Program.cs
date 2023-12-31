﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace EntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteLine("***** Fun with ADO.NET EF *****\n");
            //int carId = AddNewRecord();
            // WriteLine(carId);
            WriteLine("***** Chaining LINQ query");
            ChainingLinqQueries();
            WriteLine("***** Print all cars *****");
            PrintAllInventory();
            WriteLine("***** Print all short cars *****");
            PrintAllShortCar();
            WriteLine("***** Print all short cars *****");
            FunWithLinqQueries();
            WriteLine("***** Print any car *****");
            FindWithLinqQueries();
            WriteLine("***** Lazy loading *****");
            LazyLoading();
            WriteLine("***** Eager loading *****");
            EagerLoading();
            WriteLine("***** Explicited loading *****");
            ExplicitedLoading();
            WriteLine("***** Remove a single record");
            RemoveRecord(20);
            WriteLine("***** Update a single record");
            UpdateRecord(19);
            ReadLine();
        }

        private static int AddNewRecord()
        {
            // Add record to the Inventory table of the AutoLot database.
            using (var context = new AutoLotEntities())
            {
                try
                {
                    // Hard-code data for a new record, for testing.
                    var car = new Car() { Make = "Yugo", Color = "Brown", CarNickName = "Brownie" };
                    context.Cars.Add(car);
                    context.SaveChanges();
                    // On a successful save, EF populates the database generated identity field.
                    return car.CarId;
                }
                catch (Exception ex)
                {
                    WriteLine(ex.InnerException?.Message);
                    return 0;
                }
            }
        }

        private static void AddNewRecords(IEnumerable<Car> carsToAdd)
        {
            using (var context = new AutoLotEntities())
            {
                context.Cars.AddRange(carsToAdd);
                context.SaveChanges();
            }
        }

        private static void PrintAllInventory()
        {
            // Select all items from the Inventory table of AutoLot,
            // and print out the data using our custom ToString() of the Car entity class.
            using (var context = new AutoLotEntities())
            {
                // First way: using loop
                //foreach (Car c in context.Cars)
                //{
                //    WriteLine(c);
                //}

                // Second way: using query
                foreach (Car c in context.Cars.SqlQuery("Select CarId,Make,Color,PetName as CarNickName from Inventory where Make = @p0", "BMW"))
                {
                    WriteLine(c);
                }

                // Third way: using query with LINQ
                foreach (Car c in context.Cars.Where(c => c.Make == "BMW"))
                {
                    WriteLine(c);
                }
            }
        }

        private static void PrintAllShortCar()
        {
            using (AutoLotEntities context = new AutoLotEntities())
            {
                foreach (ShortCar c in context.Database.SqlQuery(typeof(ShortCar), "Select CarId,Make from Inventory"))
                {
                    WriteLine(c);
                }
            }
        }

        private static void FunWithLinqQueries()
        {
            using (var context = new AutoLotEntities())
            {
                // Get a projection of new data.
                var colorsMakes = from item in context.Cars select new { item.Color, item.Make };
                foreach (var item in colorsMakes)
                {
                    WriteLine(item);
                }
                // Get only items where Color == "Black"
                var blackCars = from item in context.Cars where item.Color == "Black" select item;
                foreach (var item in blackCars)
                {
                    WriteLine(item);
                }
            }
        }

        private static void FindWithLinqQueries()
        {
            using (var context = new AutoLotEntities())
            {
                WriteLine(context.Cars.Find(5));
            }
        }

        private static void ChainingLinqQueries()
        {
            using (var context = new AutoLotEntities())
            {
                //Not executed
                DbSet<Car> allData = context.Cars;
                //Not Executed.
                var colorsMakes = (from item in allData select new { item.Color, item.Make });

                //Now it's executed
                foreach (var item in colorsMakes)
                {
                    WriteLine(item);
                }
            }
        }

        private static void LazyLoading()
        {
            using (var context = new AutoLotEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                foreach (Car c in context.Cars)
                {
                    foreach (Order o in c.Orders)
                    {
                        WriteLine(o.OrderId);
                    }
                }
            }
        }

        private static void EagerLoading()
        {
            using (var context = new AutoLotEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                foreach (Car c in context.Cars.Include(c => c.Orders))
                {
                    foreach (Order o in c.Orders)
                    {
                        WriteLine(o.OrderId);
                    }
                }
            }
        }

        private static void ExplicitedLoading()
        {
            using (var context = new AutoLotEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                foreach (Car c in context.Cars)
                {
                    context.Entry(c).Collection(x => x.Orders).Load();
                    foreach (Order o in c.Orders)
                    {
                        WriteLine(o.OrderId);
                    }
                }
            }
        }

        private static void RemoveRecord(int carId)
        {
            // Find a car to delete by primary key.
            using (var context = new AutoLotEntities())
            {
                // See if we have it.
                Car carToDelete = context.Cars.Find(carId);
                if (carToDelete != null)
                {
                    context.Cars.Remove(carToDelete);
                    //This code is purely demonstrative to show the entity state changed to Deleted
                    if (context.Entry(carToDelete).State != EntityState.Deleted)
                    {
                        throw new Exception("Unable to delete the record");
                    }
                    context.SaveChanges();
                }
            }
        }

        private static void RemoveMultipleRecords(IEnumerable<Car> carsToRemove)
        {
            using (var context = new AutoLotEntities())
            {
                //Each record must be loaded in the DbChangeTracker
                context.Cars.RemoveRange(carsToRemove);
                context.SaveChanges();
            }
        }

        private static void RemoveRecordUsingEntityState(int carId)
        {
            using (var context = new AutoLotEntities())
            {
                Car carToDelete = new Car() { CarId = carId };
                context.Entry(carToDelete).State = EntityState.Deleted;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    WriteLine(ex);
                }
            }
        }

        private static void UpdateRecord(int carId)
        {
            // Find a car to delete by primary key.
            using (var context = new AutoLotEntities())
            {
                // Grab the car, change it, save!
                Car carToUpdate = context.Cars.Find(carId);
                if (carToUpdate != null)
                {
                    WriteLine(context.Entry(carToUpdate).State);
                    carToUpdate.Color = "Blue";
                    WriteLine(context.Entry(carToUpdate).State);
                }
                context.SaveChanges();
            }
        }
    }
}
