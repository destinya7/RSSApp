namespace RSSAppApi.Services
{
    public interface IWorkerQueuePublisher
    {
        void PublishMessage(string message);
        void SetupConnection();
    }
}