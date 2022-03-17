using Hotel.DTOs;
using Hotel.Models;
using Hotel.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers;

[ApiController]
[Route("api/room")]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    private readonly IRoomRepository _room;
    private readonly IScheduleRepository _schedule;

    public RoomController(ILogger<RoomController> logger, IRoomRepository room, IScheduleRepository schedule)
    {
        _logger = logger;
        _room = room;
        _schedule = schedule;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomDTO>>> GetList()
    {
        var res = await _room.GetList();

        return Ok(res.Select(x => x.asDto));

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDTO>> GetById([FromRoute] int id)
    {
        var res = await _room.GetById(id);

        if (res is null)
            return NotFound();
        var dto = res.asDto;
        dto.Schedule = (await _schedule.GetAllRooms(id))
                        .Select(x => x.asDto).ToList();
        return Ok(dto);

    }

    // [HttpPost]
    // public async Task<ActionResult> Create([FromBody] RoomCreateDTO Data)
    // {
    //     var toCreateRoom = new Room
    //     {
    //         Type = Data.Type,
    //         Size = Data.Size,
    //         StaffName = Data.StaffName
    //     };
    //     var res = await _room.Create(toCreateRoom);

    //     return StatusCode(StatusCodes.Status201Created, res.asDto);

    // }


}
