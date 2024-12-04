using BackendProject.Dto;

namespace BackendProject.Services
{
    public interface IBeerServices
    {
        Task<IEnumerable<BeerDto>> Get();

        Task<BeerDto> GetById(int id);

        Task<BeerDto> Add(BeerInserDto beerInserDto);

        Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto);

        Task<BeerDto> Delete(int id);
    }
}
