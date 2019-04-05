using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientHistory.Domain.Entities;
using PatientHistory.Domain.ValueObjects;
using Repository.Fakes;

namespace WebAPI.Tests
{
  [TestClass]
  public class UnitTest1
  {
    //[TestMethod]
    //public void TestMethod1()
    //{
    //  var patientList=new PatientResultItem[]{
    //    new PatientResultItem("Sampson", "Flynn", "Newfoundland", "Dog", 5,1)
    //  };
    //  string first;
    //  string last;
    //  var repo = new StubIPatientRepository()
    //               {
    //                 FindInt32=(int) => {return new PatientInfo();}
    //               }
    //}
    [TestMethod]
    public void PatientSearchReturnList()
    {
      var controller = new Controllers.PatientNotesController();
      Assert.AreNotEqual(0, controller.GetPatientList("Sam", "Flynn").Count());
    }
  }
}
