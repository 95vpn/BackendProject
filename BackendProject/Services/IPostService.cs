using BackendProject.Dto;

namespace BackendProject.Services
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDto>> Get();
    }
}
