using FrontDesk.SharedKernel;
using System;
using System.Linq;

namespace AppointmentScheduling.Core.Model.ValueObjects
{
    public class AnimalType : ValueObject<AnimalType>
    {
        public string Species { get; private set; }
        public string Breed { get; private set; }

      public AnimalType()
      {
          
      }
        public AnimalType(string species, string breed)
        {
            this.Species = species;
            this.Breed = breed;
        }
    }
}
