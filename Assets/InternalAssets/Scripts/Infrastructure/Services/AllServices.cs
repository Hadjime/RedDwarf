namespace InternalAssets.Scripts.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ??= new AllServices();

        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : IService => 
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService //интересный хак связанный с дженериками, для каждого вызова будет создан свой статический экземпляр сервиса
        {
            public static TService ServiceInstance;
        }
    }
}