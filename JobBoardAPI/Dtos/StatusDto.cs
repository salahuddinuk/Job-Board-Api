namespace JobBoardAPI.Dtos
{
    public sealed record StatusDto
        (
        int Id,            
            int Status
        );
    public sealed record StatusBoolDto
      (
      int Id,
          bool Status
      );
}
