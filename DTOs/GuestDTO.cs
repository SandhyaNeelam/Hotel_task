using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Hotel.DTOs;
using Hotel.Models;

namespace Hotelsql.DTOs;

public record GuestDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("schedules")]
    public List<ScheduleDTO> Schedule { get; set; }

    [JsonPropertyName("rooms")]
    public List<RoomDTO> Room { get; set; }
}

public record GuestCreateDTO
{
    [JsonPropertyName("name")]
    [Required]
    [MinLength(3)]
    [MaxLength(255)]
    public string Name { get; set; }

    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }

    [JsonPropertyName("email")]
    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    [JsonPropertyName("date_of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("address")]
    [MaxLength(255)]
    public string Address { get; set; }

    [JsonPropertyName("gender")]
    [Required]
    public Gender Gender { get; set; }
}