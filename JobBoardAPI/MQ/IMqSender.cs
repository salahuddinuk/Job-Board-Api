namespace JobBoardAPI.MQ
{
    public interface IMqSender
    {
        Task<bool> SendMessageAsync(string message);
    }
}
