﻿using FrontDesk.Core.Aggregates;
using FrontDesk.Core.ValueObjects;
using FrontDesk.SharedKernel.Enums;

namespace UnitTests.Builders
{
    public class PatientBuilder
    {
        private Patient _patient;

        public PatientBuilder()
        {
            WithDefaultValues();
        }

        public PatientBuilder Id(int id)
        {
            _patient.Id = id;
            return this;
        }

        public PatientBuilder SetPatient(Patient patient)
        {
            _patient = patient;
            return this;
        }

        public PatientBuilder WithDefaultValues()
        {
            _patient = new Patient(1, "Test Patient", Gender.Male, new AnimalType("Cat", "Mixed"));

            return this;
        }

        public Patient Build() => _patient;
    }
}