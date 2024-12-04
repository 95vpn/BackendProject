using BackendProject.Dto;
using BackendProject.Models;
using BackendProject.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        
        private IValidator<BeerInserDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private IBeerServices _beerServices;



        public BeerController(StoreContext context,
            IValidator<BeerInserDto> beerInsertValidator,
            IValidator<BeerUpdateDto> beerUpdateValidator,
            IBeerServices beerServices)
        {
            
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerServices = beerServices;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() =>
            await _beerServices.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _beerServices.GetById(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInserDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            var beerDto = await _beerServices.Add(beerInsertDto);
            

            return CreatedAtAction(nameof(GetById), new { id = beerDto.Id }, beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            
            var beerDto = await _beerServices.Update(id, beerUpdateDto);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beerDto = await _beerServices.Delete(id);
            return beerDto == null ? NotFound() : Ok(beerDto);
        }
                
    }
}
