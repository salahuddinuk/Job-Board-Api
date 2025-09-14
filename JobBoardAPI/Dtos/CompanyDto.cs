namespace JobBoardAPI.Dtos
{
    public sealed record CompanyDto
        (
            int CompanyId,
            string Name, 
            string Address,
            bool IsActive
        );        
}
