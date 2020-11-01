using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlogDoFt.QueueComponent.Models
{
    public class ValidatableModel : IValidatableObject
    {
        public bool IsValid() => !Validate().Any();

        public IEnumerable<ValidationResult> Validate()
        {
            var context = new ValidationContext(this);
            foreach (var validation in Validate(context))
            {
                yield return validation;
            }
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
    }
}
