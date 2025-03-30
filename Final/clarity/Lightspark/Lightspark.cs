using Clarity.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace ClarityAPI.Lightspark;

public class Atom : Hub
{

    public async Task PatientAtom(Patient patient)
    {
        await Clients.All.SendAsync("PatientGateway", patient);
    }
    
}