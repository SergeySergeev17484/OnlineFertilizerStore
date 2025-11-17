namespace OnlineFertilizerStore.Settings
{
    public class OnlineFertilizerStoreSettingsReader
    {
        public static OnlineFertilizerStoreSettings Read(IConfiguration configuration)
        {
            return new OnlineFertilizerStoreSettings()
            {
                OnlineFertilizerStoreDbContext = configuration.GetConnectionString("OnlineFertilizerStoreDbContext")!
            };
        }
    }
}
