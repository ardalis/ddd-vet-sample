Feature: SearchForPatient
	Search for a patient (pet) by using
	patient first name and client last name
	Results presented in a list
	

@mytag2
Scenario: Matching Patients Found
	Given I have provided an existing patient first name into the form
	And I have provided the same patient's client last name into the form
	When I execute a patient search
	Then the result should be a list of one or more matching patients

	Scenario: No Matching Patients Found
	Given I have provided an existing patient first name into the form
	And I have provided a name that is different than that patient's client last name into the form
	When I execute a patient search
	Then the result should be an empty list 