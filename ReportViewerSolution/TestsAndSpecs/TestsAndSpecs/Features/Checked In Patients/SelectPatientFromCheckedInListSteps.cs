using System;
using System.Collections.Generic;
using System.Data.Entity;
using NUnit.Framework;
using PatientHistory.Domain.ValueObjects;
using Repository;
using TechTalk.SpecFlow;
using TestsAndSpecs.Helpers;

namespace TestsAndSpecs
{
    [Binding]
    public class SelectPatientFromCheckedInListSteps
    {
      public SelectPatientFromCheckedInListSteps()
      {
        Database.SetInitializer(new PatientHistoryDBSeedingInitializer());
        HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
      }
      private IList<CheckedInPatientResultItem> _searchResults;

        [Given(@"There are patients who are currently checked in but not checked out")]
        public void GivenThereArePatientsWhoAreCurrentlyCheckedInButNotCheckedOut()
        {
          var context = new PatientHistoryDataContext();
          var patientRepo = new PatientRepository(context);
          Assert.AreNotEqual(0, patientRepo.CurrentCheckedInPatientCount());
   
        }
        
        [Then(@"I can view a list of these patients")]
        public void ThenICanViewAListOfThesePatients()
        {
          var context = new PatientHistoryDataContext();
          var patientRepo = new PatientRepository(context);
          _searchResults = patientRepo.GetCurrentCheckedInPatients();
          CollectionAssert.IsNotEmpty(_searchResults);
        }
        
        [Then(@"Select a patient to be retrieved")]
        public void ThenSelectAPatientToBeRetrieved()
        {
          Assert.Greater(_searchResults[0].PatientId, 0);
        }
    }
}
