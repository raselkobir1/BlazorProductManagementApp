using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace BlazorProducts.Server.Repository.RepositoryExtensions
{
    public static class RepositoryProductExtensions
    {
        public static IQueryable<Product> Search(this IQueryable<Product> products, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return products;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return products.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }
        public static IQueryable<T> Sort<T>(this IQueryable<T> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                // Default sorting → by the first property of T
                var defaultProperty = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();
                if (defaultProperty == null)
                    return source; // No properties to sort on
                return source.OrderBy(e => EF.Property<object>(e, defaultProperty.Name));
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                // Extract property name
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos
                    .FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                // Determine ASC or DESC
                var direction = param.EndsWith(" desc", StringComparison.InvariantCultureIgnoreCase)
                                ? "descending"
                                : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            // If no valid properties found, fallback to default sorting
            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                var defaultProperty = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();
                if (defaultProperty == null)
                    return source;
                return source.OrderBy(e => EF.Property<object>(e, defaultProperty.Name));
            }

            // Use System.Linq.Dynamic.Core for dynamic sorting
            return source.OrderBy(orderQuery);
        }
    }
}
