using System;
using ExtendedDatabase;
using NUnit.Framework;



namespace Tests
{
    public class ExtendedDatabaseTests
    {
        private ExtendedDatabase.ExtendedDatabase extendedDb;

        [SetUp]
        public void Setup()
        {
            extendedDb = new ExtendedDatabase.ExtendedDatabase();
        }

        [Test]
        public void Ctor_AddPersonToDb()
        {
            var persons = new Person[10];
            for (int i = 0; i < persons.Length; i++)
            {
                persons[i] = new Person(i, $"Name{i}");
            }
            extendedDb = new ExtendedDatabase.ExtendedDatabase(persons);
            Assert.That(extendedDb.Count, Is.EqualTo(persons.Length));

            foreach (var person in persons)
            {
                Person dbPerson = extendedDb.FindById(person.Id);
                Assert.That(person, Is.EqualTo(dbPerson));
            }
        }

        [Test]
        public void Ctor_ThrExCapacityExceeded()
        {
            var persons = new Person[17];
            for (int i = 0; i < persons.Length; i++)
            {
                persons[i] = new Person(i, $"Name{i}");
            }

            Assert.Throws<ArgumentException>(() => extendedDb = new ExtendedDatabase.ExtendedDatabase(persons));
        }

        [Test]
        public void Add_ThrExCountExceeded()
        {
            var n = 16;
            for (int i = 0; i < n; i++)
            {
                extendedDb.Add(new Person(i, $"Name{i}"));
            }

            Assert.Throws<InvalidOperationException>(() => extendedDb.Add(new Person(16, $"Toto")));
        }

        [Test]
        public void Add_ThrExExistingName()
        {
            var name = "Toto";
            extendedDb.Add(new Person(1, name));

            Assert.Throws<InvalidOperationException>(() => extendedDb.Add(new Person(2, name)));
        }

        [Test]
        public void Add_ThrExExistingId()
        {
            var iD = 1;
            extendedDb.Add(new Person(iD, "Toto"));

            Assert.Throws<InvalidOperationException>(() => extendedDb.Add(new Person(iD, "Koko")));
        }
        
        [Test]
        public void Add_IncrementCount()
        {
            var countAccount = 2;
            extendedDb.Add(new Person(1, "Toto"));
            extendedDb.Add(new Person(2, "Koko"));

            Assert.That(extendedDb.Count, Is.EqualTo(countAccount));
        }

        [Test]
        public void AddRange_ThrExCountExceeded()
        {
            var n = 16;
            for (int i = 0; i < n; i++)
            {
                extendedDb.Add(new Person(i, $"Name{i}"));
            }

            Assert.Throws<InvalidOperationException>(() => extendedDb.Add(new Person(16, $"Toto")));
        }

        [Test]
        public void Remove_ThrExEmptyDb()
        {
            Assert.Throws<InvalidOperationException>(() => extendedDb.Remove());
        }

        [Test]
        public void Remove_ElementFromDb()
        {
            var n = 3;

            for (int i = 0; i < n; i++)
            {
                extendedDb.Add(new Person(i, $"Name{i}"));
            }

            extendedDb.Remove();

            Assert.That(extendedDb.Count, Is.EqualTo(n-1));
            Assert.Throws<InvalidOperationException>(() => extendedDb.FindById(n - 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => extendedDb.FindById(-1));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void FindByUsername_ThrExInvalidUsername(string username)
        {
            Assert.Throws<ArgumentNullException>(() => extendedDb.FindByUsername(username));
        }

        [Test]
        public void FindByUsername_ThrExUsernameIsNotExist()
        {
            Assert.Throws<InvalidOperationException>(() => extendedDb.FindByUsername("Koko"));
        }

        [Test]
        public void FindByUsername_UsernameIsCorrect()
        {
            var person = new Person(1, "Toto");
            extendedDb.Add(person);
            var dbPerson = extendedDb.FindByUsername(person.UserName);

            Assert.That(person, Is.EqualTo(dbPerson));
        }

        [Test]
        public void FindById_ThrExInvalidId()
        {
            Assert.Throws<InvalidOperationException>((() => extendedDb.FindById(1)));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(- 21)]
        public void FindById_ThrExIdIsOutOfRange(int id)
        {
            Assert.Throws<ArgumentOutOfRangeException>((() => extendedDb.FindById(id)));
        }

        [Test]
        public void FindById_IdIsCorrect()
        {
            var person = new Person(1, "Toto");
            extendedDb.Add(person);
            var dbPerson = extendedDb.FindById(person.Id);

            Assert.That(person, Is.EqualTo(dbPerson));
        }

    }
}