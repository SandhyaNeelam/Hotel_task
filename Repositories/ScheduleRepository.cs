using Dapper;
using Hotel.Models;
using Hotel.Utilities;

namespace Hotel.Repositories;

public interface IScheduleRepository
{
    Task<Schedule> Create(Schedule Item);
    Task<bool> Update(Schedule Item);
    Task<bool> Delete(int Id);
    Task<List<Schedule>> GetList();
    Task<Schedule> GetById(int Id);
    Task<List<Schedule>> GetListByGuestId(int GuestId);
    Task<List<Schedule>> GetAllRooms(int Id);
}


public class ScheduleRepository : BaseRepository, IScheduleRepository
{
    public ScheduleRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Schedule> Create(Schedule Item)
    {
        var query = $@"INSERT INTO {TableNames.schedule}(check_in, check_out, guest_count, price,guest_id, room_id)VALUES(@CheckIn, @CheckOut, @GuestCount, @Price, @GuestId, RoomId) RETURNING *";
        using (var con = NewConnection)
            return await con.QuerySingleAsync<Schedule>(query, Item);
    }

    public async Task<bool> Delete(int Id)
    {
        var query = $@"DELETE FROM {TableNames.schedule} WHERE id = @Id";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, new { Id }) > 0;
    }

    public async Task<List<Schedule>> GetAllRooms(int Id)
    {
       var query = $@"SELECT * FROM {TableNames.schedule}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Schedule>(query)).AsList();
    }

    public async Task<Schedule> GetById(int Id)
    {
        var query = $@"SELECT * FROM schedule WHERE id = @Id";
        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<Schedule>(query, new { Id });
    }

    public async Task<List<Schedule>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.schedule}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Schedule>(query)).AsList();
    }

    public async Task<List<Schedule>> GetListByGuestId(int GuestId)
    {
        var query = $@"SELECT * FROM {TableNames.schedule} 
        WHERE guest_id = @GuestId";

        using (var con = NewConnection)
            return (await con.QueryAsync<Schedule>(query, new { GuestId })).AsList();
    }

    public async Task<bool> Update(Schedule Item)
    {
       var query = $@"UPDATE {TableNames.schedule} SET price=@Price WHERE id = @Id";
        using (var con = NewConnection)
            return await con.ExecuteAsync(query, Item) > 0;
    }
}