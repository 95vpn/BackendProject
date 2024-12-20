﻿using BackendProject.Dto;
using BackendProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.Services
{
    public class BeerService : IBeerServices
    {
        private StoreContext _context;

        public BeerService(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BeerDto>> Get() =>
            await _context.Beers.Select(b => new BeerDto
            {
                Id = b.BrandID,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandID = b.BrandID
            }).ToListAsync();


        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer != null)

            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };
                return beerDto;
            }
            return null;
        }   

        public async Task<BeerDto> Add(BeerInserDto beerInserDto)
        {
            var beer = new Beer()
            {
                Name = beerInserDto.Name,
                BrandID = beerInserDto.BrandID,
                Alcohol = beerInserDto.Alcohol
            };

            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };

            return beerDto;
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandID = beer.BrandID;
                await _context.SaveChangesAsync();

                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };
                return beerDto;
            }
            return null;
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };

                _context.Remove(beer);
                await _context.SaveChangesAsync();

                return beerDto;
            }
            return null;
        }

        

      
    }
}
