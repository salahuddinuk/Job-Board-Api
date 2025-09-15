namespace JobBoardAPI.Dtos
{
    public sealed record UserDto(int id, string Email, string Password, DateTime CreatedOn, bool IsActive);        
}
