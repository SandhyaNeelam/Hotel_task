using Hotel.Models;
using Hotel.Repositories;
using Hotelsql.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers;

[ApiController]
[Route("api/guest")]
public class GuestController : ControllerBase
{
    private readonly ILogger<GuestController> _logger;
    private readonly IGuestRepository _guest;
    private readonly IScheduleRepository _schedule;
    private readonly IRoomRepository _room;

    public GuestController(ILogger<GuestController> logger,
    IGuestRepository guest, IScheduleRepository schedule,
    IRoomRepository room)
    {
        _logger = logger;
        _guest = guest;
        _schedule = schedule;
        _room = room;
    }

    [HttpGet]
    public async Task<ActionResult<List<GuestDTO>>> GetList()
    {
        var res = await _guest.GetList();

        return Ok(res.Select(x => x.asDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var res = await _guest.GetById(id);

        if (res == null)
            return NotFound();

        var dto = res.asDto;
        dto.Schedule = (await _schedule.GetListByGuestId(id))
                        .Select(x => x.asDto).ToList();
        dto.Room = (await _room.GetListByGuestId(id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] GuestCreateDTO Data)
    {
        var toCreateGuest = new Guest
        {
            Address = Data.Address?.Trim(),
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Email = Data.Email.Trim(),
            Gender = Data.Gender,
            Mobile = Data.Mobile,
            Name = Data.Name.Trim(),
        };

        var res = await _guest.Create(toCreateGuest);

        return StatusCode(StatusCodes.Status201Created, res.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] GuestCreateDTO Data)
    {
        var existingGuest = await _guest.GetById(id);

        if (existingGuest == null)
            return NotFound();

        var toUpdateGuest = existingGuest with
        {
            Address = Data.Address?.Trim(),
            Email = Data.Email.Trim(),
            Mobile = Data.Mobile,

        };

        var didUpdate = await _guest.Update(toUpdateGuest);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }
}
