using System;
using Xunit;
using VetOffice.SharedDatabase.DataModel;

namespace SharedDatabaseTests.Data
{
  public class SharedDatabaseContextShould
  {
    public SharedDatabaseContextShould()
    {
      Database.SetInitializer(new TestInitializerForVetContext());
 //     AppDomain.CurrentDomain.SetData("DataDirectory", "");
    }

        [Fact]
        //[Ignore]
        public void BuildModel()
    {
      var db = new VetOfficeContext();
       db.Database.Initialize(true);
     // var clients = db.Clients.ToList();
    }
  }
}