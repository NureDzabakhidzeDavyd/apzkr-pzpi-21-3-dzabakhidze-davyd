using Kolosok.Application.Contracts.BrigadeRescuer;

namespace Kolosok.Application.Contracts.Brigade;

public class BrigadeResponse : BaseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int BrigadeSize { get; set; }
        
    public ICollection<BrigadeRescuerResponse> BrigadeRescuers { get; set; }
}

public class BrigadeLookupResponse : BaseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int BrigadeSize { get; set; }
}