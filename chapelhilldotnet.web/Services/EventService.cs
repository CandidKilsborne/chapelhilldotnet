using chapelhilldotnet.web.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace chapelhilldotnet.web.Services;

public class EventService : IEventService
{
    private const string StorageKey = "events";
    private readonly IJSRuntime _jsRuntime;
    private List<Event> _events = new();
    private int _nextId = 1;

    public EventService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    private async Task LoadEventsAsync()
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", StorageKey);
            if (!string.IsNullOrEmpty(json))
            {
                _events = JsonSerializer.Deserialize<List<Event>>(json) ?? new List<Event>();
                _nextId = _events.Any() ? _events.Max(e => e.Id) + 1 : 1;
            }
            else
            {
                // Initialize with default events from EventsList
                _events = Data.EventsList.Events.Select((e, index) => new Event
                {
                    Id = index + 1,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    Location = e.Location,
                    Time = e.Time,
                    Attendees = e.Attendees
                }).ToList();
                _nextId = _events.Count + 1;
                await SaveEventsAsync();
            }
        }
        catch
        {
            // If error, use default events
            _events = Data.EventsList.Events.Select((e, index) => new Event
            {
                Id = index + 1,
                Title = e.Title,
                Description = e.Description,
                Date = e.Date,
                Location = e.Location,
                Time = e.Time,
                Attendees = e.Attendees
            }).ToList();
            _nextId = _events.Count + 1;
        }
    }

    private async Task SaveEventsAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(_events);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
        }
        catch
        {
            // Handle save errors silently
        }
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        if (!_events.Any())
        {
            await LoadEventsAsync();
        }
        return _events.OrderBy(e => e.Date).ToList();
    }

    public async Task<Event?> GetEventByIdAsync(int id)
    {
        if (!_events.Any())
        {
            await LoadEventsAsync();
        }
        return _events.FirstOrDefault(e => e.Id == id);
    }

    public async Task<Event> CreateEventAsync(Event newEvent)
    {
        if (!_events.Any())
        {
            await LoadEventsAsync();
        }
        
        newEvent.Id = _nextId++;
        _events.Add(newEvent);
        await SaveEventsAsync();
        return newEvent;
    }

    public async Task<Event> UpdateEventAsync(Event updatedEvent)
    {
        if (!_events.Any())
        {
            await LoadEventsAsync();
        }
        
        var index = _events.FindIndex(e => e.Id == updatedEvent.Id);
        if (index >= 0)
        {
            _events[index] = updatedEvent;
            await SaveEventsAsync();
        }
        return updatedEvent;
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        if (!_events.Any())
        {
            await LoadEventsAsync();
        }
        
        var eventToRemove = _events.FirstOrDefault(e => e.Id == id);
        if (eventToRemove != null)
        {
            _events.Remove(eventToRemove);
            await SaveEventsAsync();
            return true;
        }
        return false;
    }
}
