using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hotel.DTOs;

public record ScheduleDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("check_in")]
    public DateTimeOffset CheckIn { get; set; }

    [JsonPropertyName("check_out")]
    public DateTimeOffset CheckOut { get; set; }

    [JsonPropertyName("guest_count")]
    public int GuestCount { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    [JsonPropertyName("room")]
    public List<RoomDTO> Room { get; set; }
}

public record ScheduleCreateDTO
{
    [JsonPropertyName("check_in")]
    [Required]
    public DateTimeOffset CheckIn { get; set; }

    [JsonPropertyName("check_out")]
    [Required]
    public DateTimeOffset CheckOut { get; set; }

    [JsonPropertyName("guest_count")]
    [Required]
    public int GuestCount { get; set; }

    [JsonPropertyName("price")]
    [Required]
    public double Price { get; set; }

}


public record ScheduleUpdateDTO
{

    [JsonPropertyName("price")]
    [Required]
    public double Price { get; set; }

    [JsonPropertyName("room")]
    [Required]
    public RoomDTO Room { get; set; }
}