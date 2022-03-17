using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Hotel.Models;

namespace Hotel.DTOs;

public record StaffDTO
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("shift")]
    public string Shift { get; set; }

    [JsonPropertyName("rooms")]
    public List<RoomDTO> Room { get; set; }


}

public record StaffCreateDTO
{

    [JsonPropertyName("Name")]
    [Required]
    public string Name { get; set; }

    [JsonPropertyName("date_of_birth")]
    [Required]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("mobile")]
    [Required]

    public long Mobile { get; set; }

    [JsonPropertyName("shift")]
    [Required]

    public StaffShift Shift { get; set; }

}

public record StaffUpdateDTO
{

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("shift")]
    public StaffShift Shift { get; set; }

}