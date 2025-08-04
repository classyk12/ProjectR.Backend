using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Domain.Entities;
using ProjectR.Backend.Persistence.DatabaseContext;
using ProjectR.Backend.Shared.Enums;
using ProjectR.Backend.Shared.Helpers;

namespace ProjectR.Backend.Persistence.Repository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly AppDbContext _context;

        public OtpRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OtpModel> AddAsync(OtpModel model)
        {
            Otp entity = new()
            {
                Code = model.Code?.Base64Encode(),
                RecordStatus = RecordStatus.Active,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CountryCode = model.CountryCode,
                ExpiryDate = model.ExpiryDate,
                OtpType = model.OtpType,
                DeliveryMode = model.DeliveryMode
            };

            EntityEntry<Otp> result = await _context.Otps.AddAsync(entity);
            await _context.SaveChangesAsync();

            model.Id = result.Entity.Id;
            return model;
        }

        public async Task<OtpModel?> GetAsync(OtpModel model)
        {
            Otp? result = await _context.Otps.FirstOrDefaultAsync(x => x.Code == model.Code && x.Id == model.Id &&
                 (x.PhoneNumber == model.PhoneNumber || x.Email == model.Email || x.CountryCode == model.CountryCode) && x.OtpType == model.OtpType);

            if (result == null)
            {
                return default;
            }

            model.ExpiryDate = result.ExpiryDate;
            model.CreatedAt = result.CreatedAt;
            model.RecordStatus = result.RecordStatus;
            return model;
        }

        public async Task<OtpModel?> UpdateAsync(OtpModel model)
        {
            Otp? otp = await _context.Otps.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (otp == null)
            {
                return default;
            }

            otp.RecordStatus = model.RecordStatus;
            otp.UpdatedAt = DateTimeOffset.Now;
            _context.Otps.Update(otp);

            await _context.SaveChangesAsync();

            return model;
        }
    }
}
