using System.ComponentModel.DataAnnotations;

namespace MicroServer.CheckWords.MinGans
{
    public interface IMinGanReplaceValidator
    {
        void Replace(object value, ValidationContext validationContext);
    }
}