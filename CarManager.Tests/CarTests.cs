using System;

using CarManager;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;

namespace Tests
{
    public class CarTests
    {
        private Car car;

        [SetUp]
        public void Setup()
        {
            car = new Car("Toyota", "Verso", 10, 100);
        }

        [Test]
        [TestCase("", "Verso", 5, 100)]
        [TestCase(null, "Verso", 5, 100)]
        [TestCase("Toyota", "", 5, 100)]
        [TestCase("Toyota", null, 5, 100)]
        [TestCase("Toyota", "Verso", 0, 100)]
        [TestCase("Toyota", "Verso", -1, 100)]
        [TestCase("Toyota", "Verso", 5, 0)]
        [TestCase("Toyota", "Verso", 5, -10)]

        public void Ctor_ThrExInvalidData(string make, string model, double fuelConsumption, double fuelCapacity)
        {
            Assert.Throws<ArgumentException>((() => new Car(make, model, fuelConsumption, fuelCapacity)));
        }

        [Test]
        public void Ctor_SetValidData()
        {
            string make = "Toyota";
            string model = "Verso";
            double fuelConsumption = 5;
            double fuelCapacity = 100;
            car = new Car(make, model, fuelConsumption, fuelCapacity);
            Assert.That(car.Make, Is.EqualTo(make));
            Assert.That(car.Model, Is.EqualTo(model));
            Assert.That(car.FuelConsumption, Is.EqualTo(fuelConsumption));
            Assert.That(car.FuelCapacity, Is.EqualTo(fuelCapacity));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]

        public void Refuel_ThrExFuelIsZeroOrNegative(double fuelamount)
        {
            Assert.Throws<ArgumentException>(() => car.Refuel(fuelamount), "Fuel amount cannot be negative!");
        }

        [Test]

        public void Refuel_IncreaseFuelamount()
        {
            double refuelAmount = car.FuelCapacity / 2;
            car.Refuel(refuelAmount);
            Assert.That(car.FuelAmount, Is.EqualTo(refuelAmount));
        }

        [Test]

        public void Refuel_SetFuelamountToCapacity()
        {
            car.Refuel(car.FuelCapacity * 2);
            Assert.That(car.FuelAmount, Is.EqualTo(car.FuelCapacity));
        }

        [Test]
        public void Drive_ThrExZeroFuel()
        {
            Assert.Throws<InvalidOperationException>(() => car.Drive(100));
        }

        [Test]
        public void Drive_EnoughFuel()
        {
            double fuel = car.FuelCapacity;
            car.Refuel(fuel);
            car.Drive(100);
            Assert.That(car.FuelAmount, Is.EqualTo(fuel - car.FuelConsumption));
        }

        [Test]
        public void Drive_EnoughFuelForAllDistance()
        {
            car.Refuel(car.FuelCapacity);
            double distance = car.FuelCapacity * car.FuelConsumption;
            car.Drive(distance);
            Assert.That(car.FuelAmount, Is.EqualTo(0));
        }

        [Test]
        public void FuelAmount_ThrExNegativeValue()
        {
            car.Refuel(car.FuelCapacity);
            double beforeDrive = car.FuelAmount;
            car.Drive(100);
            double afterDrive = car.FuelAmount;
            Assert.That(afterDrive, Is.LessThan(beforeDrive));
        }

        //[Test]
        //public void FuelAmount_ThrExByNegativeValue()
        //{
        //    double fuel = car.FuelCapacity;
        //    car.Refuel(fuel);
        //    car.Drive(100);
        //    //Assert.Throws(new ArgumentException(car.FuelAmount < 0, "Fuel amount cannot be negative!"));
        //    //Assert.Throws<ArgumentException>(() => , "Fuel amount cannot be negative!");
        //}
    }
}