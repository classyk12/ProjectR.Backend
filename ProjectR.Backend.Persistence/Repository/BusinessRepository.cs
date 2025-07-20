using Microsoft.EntityFrameworkCore;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Persistence.DatabaseContext;
using Serilog;
namespace ProjectR.Backend.Persistence.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly AppDbContext _context;
       private readonly ILogger _logger;

        public BusinessRepository(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            //_logger.Information("BusinessRepository initialized");
        }

        public async Task<BusinessModel[]> AddAsync(BusinessModel[] businessModels)
        {
            List<Business> entities = businessModels.Select(c => new Business {Name = c.Name }).ToList();
            
            await _context.Businesses.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return businessModels;
        }

        public async Task<BusinessModel> AddAsync(BusinessModel businessModel)
        {
            Business entity = new()
            {
                UserId = businessModel.UserId,
                Id = businessModel.Id,
                Name = businessModel.Name,
                Type = businessModel.Type,
                PhoneCode = businessModel.PhoneCode,
                PhoneNumber = businessModel.PhoneNumber,
                Industry = businessModel.Industry,
                About = businessModel.About,
                Location = businessModel.Location,
                Longitude = businessModel.Longitude,
                Latitude = businessModel.Latitude,
            };
            // _logger.Information("Adding business: {@Entity}", entity);
            await _context.Businesses.AddAsync(entity);
            await _context.SaveChangesAsync();
            return businessModel;
        }

        public async Task DeleteAsync(BusinessModel[] businessModels)
        {
            List<Business> entities = businessModels.Select(c => new Business { Id = c.Id}).ToList();
            _context.Businesses.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BusinessModel businessModel)
        {
            Business? entity = await _context.Businesses.SingleOrDefaultAsync(c => c.Id == businessModel.Id);
            _context.Businesses.Remove(entity!);
            await _context.SaveChangesAsync();
        }

        public async Task<BusinessModel[]> GetAllAsync()
        {
            List<Business> entities = await _context.Businesses.ToListAsync();
            return entities.Select(e => new BusinessModel
            {
                UserId = e.UserId,
                Id = e.Id,
                Name = e.Name,
                Type = e.Type,
                PhoneCode = e.PhoneCode,
                PhoneNumber = e.PhoneNumber,
                Industry = e.Industry,
                About = e.About,
                Location = e.Location,
                Longitude = e.Longitude,
                Latitude = e.Latitude,
            }).ToArray();
        }

        public async Task<BusinessModel> GetByIdAsync(Guid id)
        {
            Business? result = await _context.Businesses.SingleOrDefaultAsync(c => c.Id == id);
            return new BusinessModel
            {
                UserId = result.UserId,
                Id = id,
                Name = result?.Name,
                Type = result?.Type,
                 PhoneCode = result?.PhoneCode,
                PhoneNumber = result?.PhoneNumber,
                Industry = result?.Industry,
                About = result?.About,
                Location = result?.Location,
                Longitude = result?.Longitude,
                Latitude = result?.Latitude,
            };
        }

        public async Task<BusinessModel[]> UpdateAsync(BusinessModel[] businessModels)
        {
            List<Business> entities = businessModels.Select(c => new Business { Id = c.Id, Name = c.Name }).ToList();
            _context.Businesses.UpdateRange(entities);
            await _context.SaveChangesAsync();
            return businessModels;
        }

        public async Task<BusinessModel> UpdateAsync(BusinessModel businessModel)
        {
            Business? entity = await _context.Businesses.SingleOrDefaultAsync(c => c.Id ==  businessModel.Id);
            entity!.Name = businessModel.Name;
            entity!.Type = businessModel.Type;
            entity!.PhoneCode = businessModel.PhoneCode;
            entity!.PhoneNumber = businessModel.PhoneNumber;
            entity!.Industry = businessModel.Industry;
            entity!.About = businessModel.About;
            entity!.Location = businessModel.Location;
            entity!.Longitude = businessModel.Longitude;
            entity!.Latitude = businessModel.Latitude;
            _context.Businesses.Update(entity);
            await _context.SaveChangesAsync();
            return businessModel;
        }
    }
}
