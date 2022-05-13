using System;
using System.Linq;
using FightingArena;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;
        [SetUp]
        public void Setup()
        {
            arena = new Arena();
        }

        [Test]
        public void Ctor_InitializeWarrior()
        {
            Assert.That(arena.Warriors, Is.Not.Null);
        }

        [Test]
        public void Count_IsZeroByEmptyArena()
        {
            Assert.That(arena.Count, Is.EqualTo(0));
        }

        [Test]
        public void Enroll_ThrExWhenWorriorAllreadyExists()
        {
            string name = "The Rock";
            arena.Enroll(new Warrior(name, 50, 50));
            Assert.Throws<InvalidOperationException>(() => arena.Enroll(new Warrior(name, 60, 60)));
        }

        [Test]
        public void Enroll_IncreasesArenaCount()
        {
            arena.Enroll(new Warrior("The Rock", 50, 50));
            Assert.That(arena.Count, Is.EqualTo(1));
        }

        [Test]
        public void Enroll_AddWarriorToWarriors()
        {
            string name = "The Rock";
            arena.Enroll(new Warrior(name, 50, 50));
            Assert.That(arena.Warriors.Any(w => w.Name == name), Is.True);
        }

        [Test]
        public void Fight_ThrExWhenEnemyNotExists()
        {
            string attacker = "The Rock";
            arena.Enroll(new Warrior(attacker, 50, 50));
            Assert.Throws<InvalidOperationException>(() => arena.Fight(attacker, "Enemy"));
        }

        [Test]
        public void Fight_ThrExWhenAttackerNotExists()
        {
            string enemy = "Enemy";
            arena.Enroll(new Warrior(enemy, 50, 50));
            Assert.Throws<InvalidOperationException>(() => arena.Fight("The Rock", enemy));
        }

        [Test]
        public void Fight_ThrExWhenBothNotExists()
        {
            Assert.Throws<InvalidOperationException>(() => arena.Fight("The Rock", "Enemy"));
        }

        [Test]
        public void Fight_BothWarriorsLooseHpByFight()
        {
            var baseHp = 100;
            var attacker = new Warrior("The Rock", 50, baseHp);
            var enemy = new Warrior("Enemy", 50, baseHp);
            arena.Enroll(attacker);
            arena.Enroll(enemy);
            arena.Fight(attacker.Name, enemy.Name);

            Assert.That(attacker.HP, Is.EqualTo(baseHp - enemy.Damage));
            Assert.That(enemy.HP, Is.EqualTo(baseHp - attacker.Damage));
        }

    }
}
