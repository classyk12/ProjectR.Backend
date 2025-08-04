using ProjectR.Backend.Application.Interfaces.Utility;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
namespace ProjectR.Backend.Infrastructure.Managers
{
    public class SlugService : ISlugService
    {
        public string GenerateSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            string slug = input.ToLowerInvariant();

            slug = RemoveDiacritics(slug);

            slug = Regex.Replace(slug, @"\s+", "-");
            slug = Regex.Replace(slug, @"-+", "-");

            slug = Regex.Replace(slug, @"[^a-z0-9\-]", "");

            slug = slug.Trim('-');

            if (slug.Length > 100)
            {
                slug = slug.Substring(0, 100).TrimEnd('-');
            }

            return slug;
        }

        public async Task<string> GenerateUniqueSlug(string input, Func<string, Task<bool>> slugExistsCheck)
        {
            string baseSlug = GenerateSlug(input);
            string uniqueSlug = baseSlug;
            int counter = 1;

            while (await slugExistsCheck(uniqueSlug))
            {
                uniqueSlug = $"{baseSlug}-{counter}";
                counter++;
            }

            return uniqueSlug;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

    }
}
