using Kolosok.Application.Contracts.BrigadeRescuer;
using Kolosok.Application.Contracts.Victim;

namespace Kolosok.Application.Contracts.Action;

public class ActionResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ActionTime { get; set; }
    public string ActionType { get; set; }
    public string ActionPlace { get; set; }
    
    public BrigadeRescuerResponse BrigadeRescuer { get; set; }
    
    public VictimResponse Victim { get; set; }
}

public class ActionLookupResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ActionTime { get; set; }
    public string ActionType { get; set; }
    public string ActionPlace { get; set; }
    public VictimLookupResponse Victim { get; set; }
}

public class VictimActionLookupResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ActionTime { get; set; }
    public string ActionType { get; set; }
    public string ActionPlace { get; set; }
}