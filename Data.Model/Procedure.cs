using System.ComponentModel.DataAnnotations;

namespace Data.Model;

public class Procedure
{
    [Key]
    public Guid ProcedureId { get; set; }

    public string Item { get; set; }

    public List<Treatment> Treatments { get; set; }
}