using FluentValidation;

namespace Lambifast.Validations;

public static class ValidationExtensions
{
    public static void RegisterValidatorsFromAssemblyContaining<T>(this IServiceCollection services)
    {
        var types = from type in typeof(T).Assembly.GetTypes()
            where typeof(IValidator).IsAssignableFrom(type) &&
                  type.IsClass &&
                  !type.IsAbstract
            let baseType = type.BaseType
            where baseType != null &&
                  baseType.IsGenericType &&
                  baseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>)
            let validatingType = baseType.GetGenericArguments()[0]
            select new { ValidatorImplementation = type, ValidatingType = validatingType };
        foreach (var type in types)
        {
            var validatorInterface = typeof(IValidator<>).MakeGenericType(type.ValidatingType);
            services.AddSingleton(validatorInterface, type.ValidatorImplementation);
        }
    }
}