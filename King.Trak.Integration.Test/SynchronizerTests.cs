namespace King.TTrak
{
    using King.Azure.Data;
    using King.TTrak.Models;
    using Microsoft.WindowsAzure.Storage.Table;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class SynchronizerTests
    {
        [Test]
        public async Task Synchronize()
        {
            var random = new Random();
            var count = random.Next(1, 25);
            var c = new ConfigValues
            {
                FromConnectionString = "UseDevelopmentStorage=true;",
                FromTable = "t" + random.Next(),
                ToConnectionString = "UseDevelopmentStorage=true;",
                ToTable = "t" + random.Next() + "f",
            };

            //generate
            var from = new TableStorage(c.FromTable, c.FromConnectionString);
            await from.CreateIfNotExists();
            for (var i = 0; i < count; i++)
            {
                var e = new TableEntity
                {
                    PartitionKey = Guid.NewGuid().ToString(),
                    RowKey = Guid.NewGuid().ToString(),
                };

                await from.InsertOrReplace(e);
            }

            //Synchronize
            var s = new Synchronizer(c);
            await s.Run();

            //validate
            var to = new TableStorage(c.ToTable, c.ToConnectionString);
            var entities = await to.Query<TableEntity>(new TableQuery<TableEntity>());
            Assert.IsNotNull(entities);
            Assert.AreEqual(count, entities.Count());
            foreach (var e in entities)
            {
                var t = await to.QueryByPartitionAndRow<TableEntity>(e.PartitionKey, e.RowKey);
                Assert.IsNotNull(t);
                Assert.AreEqual(e.PartitionKey, t.PartitionKey);
                Assert.AreEqual(e.RowKey, t.RowKey);
            }

            await from.Delete();
            await to.Delete();
        }
    }
}