using System;
using System.Linq;
using NUnit.Framework;

namespace Tests
{
    public class DatabaseTests
    {
        private Database.Database database;

        [SetUp]
        public void Setup()
        {
            database = new Database.Database();
        }

        [Test]
        public void Ctor_ThrExDbCountExceed()
        {
            Assert.Throws<InvalidOperationException>(() => database = new Database.Database(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17));
        }

        [Test]
        public void Ctor_AddValidItemsDb()
        {
            var elements = new int[] {1, 2, 3};
            database = new Database.Database(elements);
            Assert.That(database.Count, Is.EqualTo(elements.Length));
        }

        [Test]
        public void Add_IsValid()
        {
            var n = 5;
            for (int i = 0; i < n; i++)
            {
                database.Add(i);
            }
            Assert.That(database.Count, Is.EqualTo(n));
        }

        [Test]
        public void Add_ThrExCapacity()
        {
            var n = 16;
            for (int i = 0; i < n; i++)
            {
                database.Add(i);
            }
            Assert.Throws<InvalidOperationException>((() => database.Add(17)));
        }

        [Test]
        public void Remove_DecreesCapacity()
        {
            var n = 3;
            for (int i = 0; i < n; i++)
            {
                database.Add(i);
            }
            database.Remove();
            var expectResult = 2;
            Assert.That(database.Count, Is.EqualTo(expectResult));
        }

        [Test]
        public void Remove_ThrExCountEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void Remove_ElementFromDb()
        {
            var n = 3;
            var lastElement = 3;

            for (int i = 0; i < n; i++)
            {
                database.Add(i);
            }

            database.Remove();

            var elements = database.Fetch();
            
            Assert.IsFalse(elements.Contains(lastElement));
        }

        [Test]
        public void Fetch_ReturnsCopyNotReference()
        {
            database.Add(1);
            database.Add(2);
            var firstCopy = database.Fetch();
            database.Add(3);
            var secondCopy = database.Fetch();
            Assert.That(false, Is.Not.EqualTo(secondCopy));
        }

        [Test]
        public void Count_IsEmpty()
        {
            Assert.That(database.Count, Is.EqualTo(0));
        }

    }
}