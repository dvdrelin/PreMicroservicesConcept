using Contracts.Data;
namespace Contracts.Queries;
public interface IProceduresQueryResponse
{
    Procedure[] Procedures { get; set; }
}