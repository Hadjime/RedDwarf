using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services.StaticDI;


namespace InternalAssets.Scripts.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}