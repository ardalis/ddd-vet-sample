namespace PatientHistory.Domain
{
  public class PatientResultItem
  {
    public PatientResultItem(string firstName, string lastName, string breed, string species, int age, int id)
    {
      FirstName = firstName;
      LastName = lastName;
      Breed = breed;
      Species = species;
      Age = age;
      Id = id;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Breed { get; private set; }
    public string Species { get; private set; }
    public int Age { get; private set; }
    public int Id { get; private set; }
  }
}