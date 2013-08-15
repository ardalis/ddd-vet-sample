Feature: Retrieve Patient from User Selection
	User has selected a patient from a list
	Patient ID is used to retrieve patient and history from data store



@mytag
Scenario: Retrieve patient history
	Given I am viewing a list of patients
	When I select an item from the list
	Then the patient information for selected patient should be retrieved
	And patient info with history should be displayed
	And history should be grouped by descending date