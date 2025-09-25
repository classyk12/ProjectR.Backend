using Microsoft.EntityFrameworkCore;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Persistence.DatabaseContext;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Shared.Mappers;

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
                .FirstOrDefaultAsync(x => x.Id == id);

            if (availability == null)
            {
                return null;
            }

            BusinessAvailabilityModel result = Mapper.Map<BusinessAvailability, BusinessAvailabilityModel>(availability);
            return result;
            // return new BusinessAvailabilityModel
            // {
            //     Id = availability.Id,
            //     BusinessId = availability.BusinessId,
            //     Slots = availability.Slots?.Select(slot => new BusinessAvailabilitySlotModel
            //     {
            //         BusinessAvailabilityId = availability.Id,
            //         DayOfWeek = slot.DayOfWeek,
            //         StartTime = slot.StartTime,
            //         EndTime = slot.EndTime,
            //         Breaks = slot.Breaks?.Select(b => new BreakModel
            //         {
            //             Id = b.Id,
            //             StartTime = b.StartTime,
            //             EndTime = b.EndTime
            //         }).ToList()
            //     }).ToList()
            // };
        }

        public async Task AddAsync(BusinessAvailabilityModel model)
        {
            // Create new availability
            BusinessAvailability newAvailability = Mapper.Map<BusinessAvailabilityModel, BusinessAvailability>(model);
            await _appDbContext.BusinessAvailabilities.AddAsync(newAvailability);
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

            //remove all existing slots and breaks
            _appDbContext.BusinessAvailabilitySlots.RemoveRange(availability.Slots!);
            _appDbContext.AddRange(model.Slots!);
            await _appDbContext.SaveChangesAsync();
            return Mapper.Map<BusinessAvailability, BusinessAvailabilityModel>(availability);
        }
    }
}