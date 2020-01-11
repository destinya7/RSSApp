namespace RSSAppApi.Config
{
    public interface IAppConfigManager
    {
        QueueVariable GetWorkerQueueEnvironmentVariable();

        QueueVariable GetMessageQueueEnvironmentVariable();
    }
}
