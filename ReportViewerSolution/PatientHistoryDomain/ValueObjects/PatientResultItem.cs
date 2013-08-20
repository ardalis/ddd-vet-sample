namespace PatientHistory.Domain.ValueObjects
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
      PatientId = id;
    }

    public PatientResultItem()
    {
      
    }
    public string FirstName { get;  set; }
    public string LastName { get;  set; }
    public string Breed { get;  set; }
    public string Species { get;  set; }
    public decimal Age { get;  set; }
    public int PatientId { get; set; }
  }
}