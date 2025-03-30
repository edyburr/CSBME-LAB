using Microsoft.AspNetCore.Components;
namespace Clarity.Client.Services;

    public class Surfer
    {
        public string? SearchQuery { get; set; }

        public IEnumerable<T> Search<T>(IEnumerable<T> items)
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                return items;
            }

            var searchTerms = SearchQuery.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var filteredItems = items.Where(item => searchTerms.All(term => Match(item, term)));

            return filteredItems;
        }

        public void Thrillseeker(ChangeEventArgs e)
        {
            SearchQuery = e.Value?.ToString() ?? string.Empty;
        }

        private static bool Match<T>(T item, string searchTerm)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead)
                .Select(p =>
                {
                    var value = p.GetValue(item);
                    return value switch
                    {
                        string s => s,
                        Enum e => Enum.GetName(e.GetType(), e),
                        _ => value?.ToString()
                    };
                })
                .Where(value => value != null);

            return properties.Any(prop =>
                prop != null && prop.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
    }
