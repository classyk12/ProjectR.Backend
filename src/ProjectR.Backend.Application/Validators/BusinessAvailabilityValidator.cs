using FluentValidation;
using Microsoft.Extensions.Options;
using ProjectR.Backend.Application.Models;
using ProjectR.Backend.Application.Settings;
using ProjectR.Backend.Domain.Entities;

namespace ProjectR.Backend.Application.Validators
{
    public class BusinessAvailabilityValidator : AbstractValidator<AddBusinessAvailabilityModel>
    {
        private readonly BusinessAvailabilitySettings _settings;

        public BusinessAvailabilityValidator(IOptions<BusinessAvailabilitySettings> settings)
        {
            #region Validation Checks
            //check that the end date is after the start date
            //check if the business exist
            //check if business availability already exists for the business

            //check that slots are valid i.e One slot to One day of the week
            //check that only one date is used in the slots
            //check that there are not dup

            //ensure that start time of the date is before the end time
            //ensure that the times in the breaks are within the time of the slot
            //should we let users have breaks occupying the whole slot time?
            #endregion

            _settings = settings.Value;

            // Rule: BusinessId required
            RuleFor(x => x.BusinessId)
                .NotEmpty().WithMessage("BusinessId is required.");

            // Rule: StartDate and EndDate required and valid
            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotNull().WithMessage("End date is required.");

            // Rule: EndDate must be after StartDate
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("End date must be after or equal to start date.");

            // Rule: Gap must not exceed settings.MaxAdvanceBookingInDays
            RuleFor(x => x)
                .Must(x =>
                {
                    if (x.StartDate.HasValue && x.EndDate.HasValue)
                    {
                        TimeSpan diff = x.EndDate.Value
                                 - x.StartDate.Value;
                        return diff.Days <= _settings.MaxAdvanceBookingInDays;
                    }

                    return true;
                })
                .WithMessage($"The gap between start and end date cannot exceed {_settings.MaxAdvanceBookingInDays} days.");

            // Rule: At least one slot required
            RuleFor(x => x.Slots)
                .NotNull().WithMessage("At least one slot is required.")
                .Must(slots => slots != null && slots.Count > 0)
                .WithMessage("At least one slot is required.");

            // Rule: Each slot must have valid times and date
            RuleForEach(x => x.Slots).SetValidator(new AddBusinessAvailabilitySlotValidator());

            // Rule: No overlapping slots on the same date
            RuleFor(x => x.Slots)
                .Must(slots =>
                {
                    if (slots == null)
                    {
                        return true;
                    }

                    IEnumerable<IGrouping<DateTime, AddBusinessAvailabilitySlotModel>> slotGroups = slots.GroupBy(s => s.Date.Date);
                    foreach (IGrouping<DateTime, AddBusinessAvailabilitySlotModel> group in slotGroups)
                    {
                        List<AddBusinessAvailabilitySlotModel> slotList = group.ToList();
                        for (int i = 0; i < slotList.Count; i++)
                        {
                            for (int j = i + 1; j < slotList.Count; j++)
                            {
                                AddBusinessAvailabilitySlotModel a = slotList[i];
                                AddBusinessAvailabilitySlotModel b = slotList[j];
                                if (a.StartTime.HasValue && a.EndTime.HasValue && b.StartTime.HasValue && b.EndTime.HasValue)
                                {
                                    if (a.StartTime < b.EndTime && b.StartTime < a.EndTime)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    return true;
                })
                .WithMessage("Slots must not overlap on the same date.");

            // Rule: Breaks must not overlap within the same slot
            RuleFor(x => x.Slots)
                .Must(slots =>
                {
                    if (slots == null)
                    {
                        return true;
                    }

                    foreach (AddBusinessAvailabilitySlotModel? slot in slots)
                    {
                        if (slot.Breaks == null)
                        {
                            continue;
                        }

                        List<AddBreakModel> breaks = slot.Breaks;
                        for (int i = 0; i < breaks.Count; i++)
                        {
                            for (int j = i + 1; j < breaks.Count; j++)
                            {
                                AddBreakModel a = breaks[i];
                                AddBreakModel b = breaks[j];
                                if (a.StartTime.HasValue && a.EndTime.HasValue && b.StartTime.HasValue && b.EndTime.HasValue)
                                {
                                    if (a.StartTime < b.EndTime && b.StartTime < a.EndTime)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    return true;
                })
                .WithMessage("Breaks within a slot must not overlap.");

            // Rule: Each break must be within the slot's time range
            RuleFor(x => x.Slots)
                .Must(slots =>
                {
                    if (slots == null)
                    {
                        return true;
                    }

                    foreach (AddBusinessAvailabilitySlotModel? slot in slots)
                    {
                        if (slot.Breaks == null)
                        {
                            continue;
                        }

                        foreach (AddBreakModel brk in slot.Breaks)
                        {
                            if (slot.StartTime.HasValue && slot.EndTime.HasValue && brk.StartTime.HasValue && brk.EndTime.HasValue)
                            {
                                if (brk.StartTime < slot.StartTime || brk.EndTime > slot.EndTime)
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    return true;
                })
                .WithMessage("Breaks must be within the slot's start and end time.");
        }
    }

    public class AddBusinessAvailabilitySlotValidator : AbstractValidator<AddBusinessAvailabilitySlotModel>
    {
        public AddBusinessAvailabilitySlotValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Slot date is required.");

            RuleFor(x => x.StartTime)
                .NotNull().WithMessage("Slot start time is required.");

            RuleFor(x => x.EndTime)
                .NotNull().WithMessage("Slot end time is required.");

            RuleFor(x => x)
                .Must(x => x.StartTime.HasValue && x.EndTime.HasValue && x.EndTime > x.StartTime)
                .WithMessage("Slot end time must be after start time.");

            RuleForEach(x => x.Breaks).SetValidator(new AddBreakValidator());
        }
    }

    public class AddBreakValidator : AbstractValidator<AddBreakModel>
    {
        public AddBreakValidator()
        {
            RuleFor(x => x.StartTime)
                .NotNull().WithMessage("Break start time is required.");

            RuleFor(x => x.EndTime)
                .NotNull().WithMessage("Break end time is required.");

            RuleFor(x => x)
                .Must(x => x.StartTime.HasValue && x.EndTime.HasValue && x.EndTime > x.StartTime)
                .WithMessage("Break end time must be after start time.");
        }
    }
}