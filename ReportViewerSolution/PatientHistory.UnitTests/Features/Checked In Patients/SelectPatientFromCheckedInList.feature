Feature: SelectPatientFromCheckedInList
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Select Patient from List of Checked In Patients
	Given There are patients who are currently checked in but not checked out
	Then I can view a list of these patients
	And Select a patient to be retrieved
