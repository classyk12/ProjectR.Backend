using Microsoft.EntityFrameworkCore;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Persistence.DatabaseContext;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Shared.Mappers;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectR.Backend.Shared;

namespace ProjectR.Backend.Persistence.Repository
{
    public class BusinessAvailabilityRepository : IBusinessAvailabilityRepository
    {
        private readonly AppDbContext _appDbContext;

        public BusinessAvailabilityRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<BusinessAvailabilityModel?> GetByIdAsync(Guid id)
        {
            BusinessAvailability? availability = await _appDbContext.BusinessAvailabilities
                .Include(x => x.Slots)!
                .ThenInclude(s => s.Breaks)
                .FirstOrDefaultAsync(x => x.Id == id && x.RecordStatus == RecordStatus.Active);

            if (availability == null)
            {
                return null;
            }

            BusinessAvailabilityModel result = Mapper.Map<BusinessAvailability, BusinessAvailabilityModel>(availability);
            return result;
        }

        public async Task<BusinessAvailabilityModel> AddAsync(BusinessAvailabilityModel model)
        {
            // Create new availability
            BusinessAvailability newAvailability = Mapper.Map<BusinessAvailabilityModel, BusinessAvailability>(model);
            EntityEntry<BusinessAvailability> result = await _appDbContext.BusinessAvailabilities.AddAsync(newAvailability);
            await _appDbContext.SaveChangesAsync();
            model.Id = result.Entity.Id;
            return model;
        }

        public async Task<BusinessAvailabilityModel> UpdateAsync(Guid id, UpdateBusinessAvailabilityModel model)
        {
            BusinessAvailability? availability = await _appDbContext.BusinessAvailabilities
               .Include(x => x.Slots)!
                   .ThenInclude(s => s.Breaks)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (availability == null)
            {
                return null!;
            }

            if (availability.Slots != null && availability.Slots.Any())
            {
                _appDbContext.BusinessAvailabilitySlots.RemoveRange(availability.Slots);
            }

            List<BusinessAvailabilitySlot> newSlots = model.Slots?.Select(s => Mapper.Map<AddBusinessAvailabilitySlotModel, BusinessAvailabilitySlot>(s)).ToList()
                           ?? new List<BusinessAvailabilitySlot>();

            foreach (BusinessAvailabilitySlot slot in newSlots)
            {
                slot.BusinessAvailabilityId = availability.Id;
            }

            if (newSlots.Any())
            {
                await _appDbContext.BusinessAvailabilitySlots.AddRangeAsync(newSlots);
            }

            await _appDbContext.SaveChangesAsync();

            BusinessAvailability updatedAvailability = await _appDbContext.BusinessAvailabilities
                .Include(x => x.Slots)!
                    .ThenInclude(s => s.Breaks)
                .FirstAsync(x => x.Id == id);

            return Mapper.Map<BusinessAvailability, BusinessAvailabilityModel>(updatedAvailability);
        }

        public async Task<BusinessAvailabilityModel[]> GetAllByBusinessIdAsync(Guid id, bool includeAll = false)
        {
            IQueryable<BusinessAvailability> query = _appDbContext.BusinessAvailabilities;

            if (includeAll)
            {
                query = query.Where(x => x.BusinessId == id);
            }
            else
            {
                query = query.Where(x => x.BusinessId == id && x.RecordStatus == RecordStatus.Active);
            }

            if (includeAll)
            {
                query = query.Include(x => x.Slots!)
                             .ThenInclude(s => s.Breaks);
            }

            List<BusinessAvailability> availabilities = await query.ToListAsync();
            return availabilities.Select(c => Mapper.Map<BusinessAvailability, BusinessAvailabilityModel>(c)).ToArray();
        }

        public async Task<bool> HasActiveAvailabilityAsync(Guid businessId, DateTime startDate, DateTime endDate)
        {
            return await _appDbContext.BusinessAvailabilities
                .AnyAsync(x => x.BusinessId == businessId
                    && x.RecordStatus == RecordStatus.Active
                    && x.StartDate!.Value.Date <= endDate
                    && x.EndDate!.Value.Date >= startDate);
        }
    }
}