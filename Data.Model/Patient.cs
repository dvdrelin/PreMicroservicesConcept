using System.ComponentModel.DataAnnotations;

namespace Data.Model;

public class Patient
{
    [Key]
    public Guid PatientId { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public Provider? MainProvider { get; set; }
}