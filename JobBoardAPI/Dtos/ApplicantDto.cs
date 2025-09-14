namespace JobBoardAPI.Dtos
{
    public sealed record ApplicantDto
        (
            int ApplicantId,
            string Name, 
            string Email,
            bool IsActive
        );        
}
