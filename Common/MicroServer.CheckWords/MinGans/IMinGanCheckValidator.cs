using System.ComponentModel.DataAnnotations;

namespace MicroServer.CheckWords.MinGans
{
    public interface IMinGanCheckValidator
    {
        ValidationResult IsValid(object value, ValidationContext validationContext);
    }
}