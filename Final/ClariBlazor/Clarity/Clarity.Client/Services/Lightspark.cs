using System.Dynamic;
using Microsoft.AspNetCore.SignalR.Client;
using Clarity.Shared.Models;

namespace Clarity.Client.Services;

    public class Lightspark
    {
        private HubConnection? _hubConnection;
        public async Task ConnectAsync()
        {
            if (_hubConnection is not null) return;

            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:8080/atom")
                .WithAutomaticReconnect()
                .Build();
            
            _hubConnection.On<Patient>("PatientGateway", (patient) =>
            {
                PatientUpdated?.Invoke(patient);
            });
            
            try
            {
                await _hubConnection.StartAsync();
                Console.WriteLine("✅ SignalR Connected!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ SignalR Connection Failed: {ex.Message}");
            }
        }

        public async Task DisconnectAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }
        
        public event Action<Patient> PatientUpdated;
    }

