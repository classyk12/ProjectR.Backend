namespace ProjectR.Backend.Application.Interfaces.Utility
{
    public interface ISlugService
    {
        string GenerateSlug(string input);
        Task<string> GenerateUniqueSlug(string input, Func<string, Task<bool>> slugExistsCheck);
    }
}