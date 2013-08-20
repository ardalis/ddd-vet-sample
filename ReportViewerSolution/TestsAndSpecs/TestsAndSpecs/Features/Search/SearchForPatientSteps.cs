using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientHistory.Domain.ValueObjects;
using Repository;
using TechTalk.SpecFlow;
using System.Linq;
using TestsAndSpecs.Helpers;

namespace TestsAndSpecs.Features.Search
{
    [Binding]
    public class SearchForPatientSteps
    {
      private string _patientFirstName;
      private string _clientLastName;
      private List<PatientResultItem> _searchResults;

        [Given(@"I have entered a patient first name into the form")]
        public void GivenIHaveEnteredAPatientFirstNameIntoTheForm()
        {
          _patientFirstName = "Sam";

        }
        
        [Given(@"I have entered a client last name into the form")]
        public void GivenIHaveEnteredAClientLastNameIntoTheForm()
        {
          _clientLastName = "Flynn";
        }
        
        [When(@"I press search")]
        public void WhenIPressSearch()
        {
          Database.SetInitializer(new PatientHistoryDBSeedingInitializer());
          var context = new PatientHistoryDataContext();
          var patientRepo=new PatientRepository(context);
          _searchResults = patientRepo.Find(_patientFirstName,_clientLastName);
        }

        [Then(@"the result should be a list of one or more matching patients")]
        public void ThenTheResultShouldBeAListOfOneOrMoreMatchingPatients()
        {
          Assert.AreNotEqual(0,
                             _searchResults.Count(r => r.FirstName.Contains(_patientFirstName) && r.LastName.Contains(_clientLastName)));
        }
        
 
    }




  }

