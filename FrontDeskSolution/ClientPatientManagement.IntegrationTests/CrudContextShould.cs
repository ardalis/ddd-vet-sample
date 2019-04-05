using System.Data.Entity;
using System.Linq;
using ClientPatientManagement.Data;
using NUnit.Framework;

namespace ClientPatientManagement.IntegrationTests.Data
{
  [TestFixture]
  public class CrudContextShould
  {
    public CrudContextShould()
    {
        Database.SetInitializer<CrudContext>(null);
    }
    [Test]
    public void GetClientList()
    {
      using (var db = new CrudContext())
      {
        Assert.IsNotEmpty(db.Clients.ToList());
      }
    }

    [Test]
    public void ReturnMaintenanceClientType()
    {
      using (var db = new CrudContext())
      {
        Assert.IsInstanceOf < ClientPatientManagement.Core.Model.Client > (db.Clients.FirstOrDefault());
      }
    }
  }
}