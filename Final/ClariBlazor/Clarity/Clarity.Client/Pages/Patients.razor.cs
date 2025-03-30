using System.Net.Http.Json;
using Clarity.Client.Services;
using Clarity.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Clarity.Client.Pages;

public partial class Patients
{
    [Inject] private Lightspark Atom { get; set; }
    [Inject] private Surfer Surf { get; set; }
    [Inject] private HttpClient Http { get; set; }
    
    private bool _isLoading = true;
    private List<Patient>? _patients = [];
    
    protected override async Task OnInitializedAsync()
    {
        await LoadPatients();
        Atom.PatientUpdated += OnPatientUpdated;
    }

    private async Task LoadPatients()
    {
        try
        {
            var response = await Http.GetAsync($"https://localhost:8080/data/patients");
            _patients = await response.Content.ReadFromJsonAsync<List<Patient>>();
            _isLoading = false;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data: {ex.Message}");
            _isLoading = false;
        }
        
    }
    
    private IEnumerable<Patient> FilteredData => Surf.Search(_patients);
    private void OnPatientUpdated(Patient patientData)
    {
        var record = _patients.FirstOrDefault(s => s.Id == patientData.Id);
        if (record != null)
        {
            if (patientData != null)
                record.Name = patientData.Name;
            record.Gender = patientData.Gender;
            record.Measurements = patientData.Measurements;
            record.Age = patientData.Age;
            record.Room = patientData.Room;
            record.Wing = patientData.Wing;
            
            InvokeAsync(StateHasChanged);
        }
    }

    public async ValueTask DisposeAsync()
    {
        // Cleanup when the page is disposed
        Atom.PatientUpdated -= OnPatientUpdated;
        await Atom.DisconnectAsync();
    }
    
}