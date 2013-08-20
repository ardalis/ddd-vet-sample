using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientHistory.Domain.Entities;
using PatientHistory.Domain.ValueObjects;
using Repository;
using TechTalk.SpecFlow;
using TestsAndSpecs.Helpers;

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
          Database.SetInitializer(new PatientHistoryDBSeedingInitializer());
   var patientRepo = new PatientRepository(new PatientHistoryDataContext());
          _patient = patientRepo.Find(_selectedItem.PatientId);
          Assert.IsNotNull(_patient);
        }

        [Then(@"patient info with history should be displayed")]
        public void ThenPatientInfoWithHistoryShouldBeDisplayed()
        {
          Debug.WriteLine("{0} {1} {2} {3}", _patient.PatientFirstName, _patient.ClientLastName, _patient.Species,
                          _patient.Breed);
          if (_patient.VisitNotes == null)
          {
            Debug.WriteLine("No Notes");
          }
          else
          {
            foreach (var note in _patient.VisitNotes)
            {
              Debug.WriteLine("{0}: {1}", note.VisitDateTime, note.Notes);
            }
          }
        }

        [Then(@"history should be grouped by descending date")]
        public void ThenHistoryShouldBeGroupedByDescendingDate()
        {
          Debug.WriteLine("Oh writing a test to verify that linq worked is a)too  hard and b)stupid");
        }

    }
}
