namespace JobBoardAPI.Dtos
{
    public sealed record JobDto
        (
        int JobId,
            int CompanyId, 
            string Title, 
            string Description,
            DateTime CreatedOn,
            DateTime? ActiveFrom, 
            DateTime? LastDate,
            int Status
        );        
}
