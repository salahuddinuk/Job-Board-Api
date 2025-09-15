namespace JobBoardAPI.Dtos
{
    public sealed record UserPasswordDto(string id, string Email, string OldPassword, string NewPassword);        
}
