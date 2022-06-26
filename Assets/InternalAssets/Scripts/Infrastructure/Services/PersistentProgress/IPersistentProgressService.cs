using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services.StaticDI;


namespace InternalAssets.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}