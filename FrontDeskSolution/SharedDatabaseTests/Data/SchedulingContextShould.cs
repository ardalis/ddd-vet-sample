using System;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using VetOffice.SharedDatabase.DataModel;

namespace SharedDatabaseTests.Data
{
  [TestFixture]
  public class SharedDatabaseContextShould
  {
    public SharedDatabaseContextShould()
    {
      Database.SetInitializer(new TestInitializerForVetContext());
 //     AppDomain.CurrentDomain.SetData("DataDirectory", "");
    }

    [Test]
      //[Ignore]
    public void BuildModel()
    {
      var db = new VetOfficeContext();
       db.Database.Initialize(true);
     // var clients = db.Clients.ToList();
    }
  }
}