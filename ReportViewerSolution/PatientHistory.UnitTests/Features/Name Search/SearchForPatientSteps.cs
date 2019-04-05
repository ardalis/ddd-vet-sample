using System.Collections.Generic;
using System.Data.Entity;
using NUnit.Framework;
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
      private IEnumerable<PatientResultItem> _searchResults;


       [Given(@"I have provided an existing patient first name into the form")]
       public void GivenIHaveProvidedAnExistingPatientFirstNameIntoTheForm()
       {
         _patientFirstName = "Sam";
       }


        [Given(@"I have provided the same patient's client last name into the form")]
        public void GivenIHaveEnteredAClientLastNameIntoTheForm()
        {
          _clientLastName = "Flynn";
        }

        [Given(@"I have provided a name that is different than that patient's client last name into the form")]
        public void GivenIHaveProvidedANameThatIsDifferentThanThatPatientSClientLastNameIntoTheForm()
        {
          _clientLastName = "abcdefg";
        }


        [When(@"I execute a patient search")]
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

        [Then(@"the result should be an empty list")]
        public void ThenTheResultShouldBeAnEmptyList()
        {
          Assert.AreEqual(0,
                   _searchResults.Count(r => r.FirstName.Contains(_patientFirstName) && r.LastName.Contains(_clientLastName)));

        }

        
 
    }




  }

