using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model;

public class Treatment
{
    [Key]
    public Guid TreatmentId { get; set; }
    
    public Patient Patient { get; set; }

    public Provider Provider { get; set; }

    public Procedure Procedure { get; set; }

    public decimal Fee { get; set; }
}