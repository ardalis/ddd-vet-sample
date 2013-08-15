Feature: SearchForPatient
	Search for a patient (pet) by using
	patient first name and client last name
	Results presented in a list
	

@mytag2
Scenario: Patients Found
	Given I have entered a patient first name into the form
	And I have entered a client last name into the form
	When I press search
	Then the result should be a list of one or more matching patients
