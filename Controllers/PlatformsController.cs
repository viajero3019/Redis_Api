using Microsoft.AspNetCore.Mvc;
using REDIS_Api.Data;
using REDIS_Api.Models;

namespace REDIS_Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;

        public PlatformsController(IPlatformRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<Platform> GetPlatformById(string id)
        {
            var platform = _repository.GetPlatformById(id);

            if(platform != null) return Ok(platform);

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Platform> CreatePlatform(Platform platform)
        {
            _repository.CreatePlatform(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id}, platform);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
        {
            return Ok(_repository.GetAllPlatforms());
        }

    }
}