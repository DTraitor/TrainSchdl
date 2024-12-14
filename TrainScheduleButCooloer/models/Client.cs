namespace TrainScheduleButCooloer.models;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string BillingInformation { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
