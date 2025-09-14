namespace JobBoardAPI.Dtos
{
    public sealed record ApplicationDto
        (
            int ApplicationId,
            int ApplicantId, 
            int JobId,
            int Status
        );        
}
