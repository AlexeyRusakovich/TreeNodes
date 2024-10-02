using FluentValidation;
using TreeNodes.API.Models.Exceptions;

namespace TreeNodes.API.Extensions
{
    public static class ValidationExtensions
    {
        public async static Task<bool> ValidateAsync<T>(this T objectToValidate, IValidator<T> validator)
        {
            var validationResult = await validator.ValidateAsync(objectToValidate);
            if (!validationResult.IsValid)
            {
                throw new SecureException(string.Join(',', validationResult.Errors));
            }

            return true;
        }

        public static IValidator<T> GetValidator<T>(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IValidator<T>>();
        }
    }
}
