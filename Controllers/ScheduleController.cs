using Hotel.DTOs;
using Hotel.Models;
using Hotel.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController : ControllerBase
{
    private readonly ILogger<ScheduleController> _logger;
    private readonly IScheduleRepository _schedule;
    private readonly IRoomRepository _room;

    public ScheduleController(ILogger<ScheduleController> logger, IScheduleRepository schedule, IRoomRepository room)
    {
        _logger = logger;
        _schedule = schedule;
        _room = room;
    }

    [HttpGet]
    public async Task<ActionResult<List<ScheduleDTO>>> GetList()
    {
        var res = await _schedule.GetList();
        return Ok(res.Select(x => x.asDto));

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Schedule>> GetById([FromRoute] int id)
    {
        var schedule = await _schedule.GetById(id);
        if (schedule is null)
            return NotFound("No schedule is found with given id");
        var dto = schedule.asDto;


        dto.Room = (await _room.GetAllForSchedule(id)).Select(x => x.asDto).ToList();
        return Ok(dto);

    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ScheduleCreateDTO Data)
    {
        var toCreateSchedule = new Schedule
        {
            CheckIn = Data.CheckIn,
            CheckOut = Data.CheckOut,
            GuestCount = Data.GuestCount,
            Price = Data.Price

        };
        var res = await _schedule.Create(toCreateSchedule);

        return StatusCode(StatusCodes.Status201Created, res.asDto);

    }



    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ScheduleCreateDTO Data)
    {
        var existingSchedule = await _schedule.GetById(id);

        if (existingSchedule == null)
            return NotFound();

        var toUpdateSchedule = existingSchedule with
        {
            Price = Data.Price

        };

        var didUpdate = await _schedule.Update(toUpdateSchedule);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var existing = await _schedule.GetById(id);
        if (existing is null)
            return NotFound("No schedule found with given id");

        await _schedule.Delete(id);

        return NoContent();
    }
}
