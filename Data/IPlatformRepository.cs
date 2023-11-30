using REDIS_Api.Models;

namespace REDIS_Api.Data
{
    public interface IPlatformRepository
    {
        void CreatePlatform(Platform platform);
        Platform? GetPlatformById(string id);
        IEnumerable<Platform?>? GetAllPlatforms();
    }
}