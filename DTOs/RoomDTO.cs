using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Hotel.Models;

namespace Hotel.DTOs;

public record RoomDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("staff_name")]
    public string StaffName { get; set; }

    [JsonPropertyName("schedules")]
    public List<ScheduleDTO> Schedule { get; set; }
}


public record RoomCreateDTO
{

    [JsonPropertyName("type")]
    [Required]
    public RoomType Type { get; set; }

    [JsonPropertyName("size")]
    [Required]
    public int Size { get; set; }

    [JsonPropertyName("staff_name")]
    [Required]
    public string StaffName { get; set; }

}

public record RoomUpdateDTO
{

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("staff_name")]
    [Required]
    public string StaffName { get; set; }

   
}
