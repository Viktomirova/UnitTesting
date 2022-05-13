using System;
using FightingArena;
using NUnit.Framework;

namespace Tests
{
    public class WarriorTests
    {
        private Warrior warrior;
        [SetUp]
        public void Setup()
        {
            warrior = new Warrior("The Rock", 100, 100);
        }

        [Test]
        [TestCase("", 100, 100)]
        [TestCase(null, 100, 100)]
        [TestCase("The Rock", 0, 100)]
        [TestCase("The Rock", -10, 100)]
        [TestCase("The Rock", 100, -100)]

        public void Ctor_ThrExInvalidData(string name, int damage, int hp)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, damage, hp));
        }

        [Test]
        [TestCase(49)]
        [TestCase(25)]
        public void Attack_ThrExWhenHpIsToLow(int hp)
        {
            var attacker = new Warrior("The Rock", 10, hp);
            var enemy = new Warrior("Enemy", 50, 50);
            Assert.Throws<InvalidOperationException>(() => attacker.Attack(enemy));
        }

        [Test]
        [TestCase(30)]
        [TestCase(25)]
        public void Attack_ThrExWhenHpIsToLowEnemy(int hp)
        {
            var attacker = new Warrior("The Rock", 50, 50);
            var enemy = new Warrior("Enemy", 50, hp);
            Assert.Throws<InvalidOperationException>(() => attacker.Attack(enemy));
        }

        [Test]
        [TestCase(40, 100, 35, 100)]
        [TestCase(50, 100, 35, 50)]
        [TestCase(60, 100, 35, 50)]
        [TestCase(50, 100, 100, 50)]
        public void Attack_DecreasesWorriorsHp(int attackerDamage, int attackerHp, int enemyDamage, int enemyHp)
        {
            var attacker = new Warrior("The Rock", attackerDamage, attackerHp);
            var enemy = new Warrior("Enemy", enemyDamage, enemyHp);

            var attackerExpectedHp = attackerHp - enemyDamage;
            var enemyrExpectedHp = enemyHp - attackerDamage;

            if (enemyrExpectedHp < 0)
            {
                enemyrExpectedHp = 0;
            }

            attacker.Attack(enemy);

            Assert.That(attacker.HP, Is.EqualTo(attackerExpectedHp));
            Assert.That(enemy.HP, Is.EqualTo(enemyrExpectedHp));
        }


    }
}