using System;
using System.Collections.Generic;
using PatientHistory.Domain;
using Repository;
using TechTalk.SpecFlow;

namespace TestsAndSpecs.Features.Search
{
    [Binding]
    public class RetrievePatientFromUserSelectionSteps
    {
      private List<PatientResultItem> resultList;
      private PatientResultItem _selectedItem;
      private PatientInfo _patient;

      public RetrievePatientFromUserSelectionSteps()
      {
        resultList = new List<PatientResultItem>
         {new PatientResultItem("Sampson", "Lerman", "Newf", "Dog", 5, 1)};

      }

        [Given(@"I am viewing a list of patients")]
        public void GivenIAmViewingAListOfPatients()
        {
            
        }
        
        [When(@"I select an item from the list")]
        public void WhenISelectAnItemFromTheList()
        {
          _selectedItem = resultList[0];
        }

        [Then(@"the patient information for selected patient should be retrieved")]
        public void ThenThePatientInformationForSelectedPatientShouldBeRetrieved()
        {
          var context = new DataContext();
          context.PatientInfos = new List<PatientInfo> { PatientInfo.CreatePatientInfoWithHistory("Sampson", "Flynn", 1) };
          var patientRepo = new PatientRepository(context);
          _patient = patientRepo.Find(_selectedItem.Id);
          ScenarioContext.Current.Pending();
        }

        [Then(@"patient info with history should be displayed")]
        public void ThenPatientInfoWithHistoryShouldBeDisplayed()
        {
          ScenarioContext.Current.Pending();
        }

        [Then(@"history should be grouped by descending date")]
        public void ThenHistoryShouldBeGroupedByDescendingDate()
        {
          ScenarioContext.Current.Pending();
        }

    }
}
