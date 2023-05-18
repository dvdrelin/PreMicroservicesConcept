using System.ComponentModel.DataAnnotations;

namespace Data.Model;

public class Provider
{
    [Key]
    public Guid ProviderId { get; set; }

    public string FirstName { get; set; }

    public string Lastname { get; set; }

    public List<Treatment> Treatments { get; set; }
}