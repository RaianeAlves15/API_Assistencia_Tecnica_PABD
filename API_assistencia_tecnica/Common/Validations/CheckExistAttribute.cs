using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace API_assistencia_tecnica.Validations
{
    public class CheckEntityExistAttribute : ValidationAttribute
    {
        private readonly Type _serviceType;

        public CheckEntityExistAttribute(Type serviceType)
        {
            _serviceType = serviceType;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("O valor n�o pode ser nulo.");

            var service = validationContext.GetService(_serviceType);
            if (service == null)
                return new ValidationResult($"Servi�o '{_serviceType.Name}' n�o encontrado.");

            int id;
            try
            {
                id = Convert.ToInt32(value);
            }
            catch
            {
                return new ValidationResult("ID inv�lido.");
            }

            var existsMethod = _serviceType.GetMethod("ExistsAsync");
            if (existsMethod == null)
                return new ValidationResult("M�todo 'ExistsAsync' n�o encontrado no servi�o.");

            var resultTask = (Task<bool>?)existsMethod.Invoke(service, new object[] { id });
            if (resultTask == null || !resultTask.Result)
                return new ValidationResult("Registro n�o encontrado.");

            return ValidationResult.Success;
        }
    }
}
