using AutoMapper;
using CarService.Authorization;
using CarService.Entities;
using CarService.Models;
using CarService.NotFoundException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarService.Services
{
    public class CarMarketService : Controller, ICarMarketService
    {
        private readonly IMapper _mapper;
        private readonly CarServiceDbContext _dbContext;
        private readonly ILogger<CarMarketService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public CarMarketService(IMapper mapper, CarServiceDbContext dbContext, ILogger<CarMarketService> logger,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        public CarMarketDto GetById(int id)
        {
            var carMarket = _dbContext
                .CarMarkets
                .Include(c => c.Address)
                .Include(c => c.Parts)
                .FirstOrDefault(c => c.Id == id);

            if (carMarket is null) throw new NotFoundException.NotFoundException("Car market not found");

            var result = _mapper.Map<CarMarketDto>(carMarket);
            return result;
        }
        public PageResult<CarMarketDto> GetAll(CarMarketQuery query)
        {
            var baseQuery = _dbContext
                .CarMarkets
                .Include(c => c.Address)
                .Include(c => c.Parts)
                .Where(r => query.SearchPhrase == null ||
                    (r.Name.ToLower().Contains(query.SearchPhrase) || r.Description.ToLower().Contains(query.SearchPhrase)));

            if(string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<CarMarket, Object>>>
                {
                    { nameof(CarMarket.Name), c => c.Name},
                    { nameof(CarMarket.Description), c => c.Description},
                    //{ nameof(CarMarket.Ca), c => c.Name},
                };

                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var carMarkets = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var carMarketsDtos = _mapper.Map<List<CarMarketDto>>(carMarkets);

            var countTotalItems = baseQuery.Count();

            var result = new PageResult<CarMarketDto>(carMarketsDtos, countTotalItems, query.PageSize, query.PageNumber);
            return result;
        }
        public IEnumerable<CarMarketDto> GetAllWithoutFiltering()
        {
            var carMarkets = _dbContext.CarMarkets.ToList();

            var carMarketsDtos = _mapper.Map<List<CarMarketDto>>(carMarkets);
            return carMarketsDtos;
        }
        public int Create(CreateCarMarketDto dto)
        {
            var carMarket = _mapper.Map<CarMarket>(dto);
            carMarket.OwnerId = _userContextService.GetUserId;
            _dbContext.CarMarkets.Add(carMarket);
            _dbContext.SaveChanges();
            return carMarket.Id;
        }
        public void Delete(int id)
        {
            _logger.LogError($"Car Market with id:{ id} DELETE action invoked");
            var carMarket = _dbContext
            .CarMarkets
            .FirstOrDefault(c => c.Id == id);

            var authorizationResult =
            _authorizationService.AuthorizeAsync(_userContextService.User, carMarket, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (carMarket is null) throw new NotFoundException.NotFoundException("Car market not found");
            _dbContext.CarMarkets.Remove(carMarket);
            _dbContext.SaveChanges();
        }
        public void Update(int id, UpdateCarMarketDto dto)
        {
            var carMarket = _dbContext
                .CarMarkets
                .FirstOrDefault(c => c.Id == id);

            if (carMarket is null) throw new NotFoundException.NotFoundException("Car market not found");
            var authorizationResult =
            _authorizationService.AuthorizeAsync(_userContextService.User, carMarket, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if(!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            carMarket.Name = dto.Name;
            carMarket.Description = dto.Description;
            carMarket.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();
        }
    }
}
