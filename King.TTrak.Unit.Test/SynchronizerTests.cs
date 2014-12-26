namespace King.TTrak
{
    using King.Azure.Data;
    using King.TTrak.Models;
    using Microsoft.WindowsAzure.Storage.Table;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    public class SynchronizerTests
    {
        [Test]
        public void Constructor()
        {
            var c = new ConfigValues
            {
                FromConnectionString = "UseDevelopmentStorage=true;",
                FromTable = "from",
                ToConnectionString = "UseDevelopmentStorage=true;",
                ToTable = "to",
            };
            new Synchronizer(c);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorConfigNull()
        {
            new Synchronizer(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorFromNull()
        {
            var to = Substitute.For<ITableStorage>();
            new Synchronizer(null, to);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorToNull()
        {
            var from = Substitute.For<ITableStorage>();
            new Synchronizer(from, null);
        }

        [Test]
        public async Task Run()
        {
            var random = new Random();
            var count = random.Next(1, 25);
            var entities = new List<IDictionary<string, object>>();
            for (var i = 0; i < count; i++)
            {
                entities.Add(new Dictionary<string, object>());
            }
            var from = Substitute.For<ITableStorage>();
            from.Query(Arg.Any<TableQuery>()).Returns(Task.FromResult((IEnumerable<IDictionary<string, object>>)entities));
            var to = Substitute.For<ITableStorage>();
            to.CreateIfNotExists();
            to.Insert(entities);

            var s = new Synchronizer(from, to);
            await s.Run();

            from.Received().Query(Arg.Any<TableQuery>());
            to.Received().CreateIfNotExists();
            to.Received().Insert(entities);
        }

        [Test]
        public async Task RunNoEntities()
        {
            var from = Substitute.For<ITableStorage>();
            from.Query(Arg.Any<TableQuery>()).Returns(Task.FromResult((IEnumerable<IDictionary<string, object>>)null));
            var to = Substitute.For<ITableStorage>();
            to.CreateIfNotExists();
            to.Insert(Arg.Any<IEnumerable<IDictionary<string, object>>>());

            var s = new Synchronizer(from, to);
            await s.Run();

            from.Received().Query(Arg.Any<TableQuery>());
            to.Received().CreateIfNotExists();
            to.Received(0).Insert(Arg.Any<IEnumerable<IDictionary<string, object>>>());
        }
    }
}