using FrontDesk.SharedKernel;

namespace FrontDesk.Core.ValueObjects
{
    public class AnimalType : ValueObject
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
