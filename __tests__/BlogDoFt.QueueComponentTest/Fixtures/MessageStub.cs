using BlogDoFt.QueueComponent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlogDoFt.QueueComponentTest.Fixtures
{
    public class MessageStub : MessageBase
    {
        public int NonRequiredField { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string RequiredField { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = base.Validate(validationContext).ToList();

            foreach (var prop in this.GetType().GetProperties())
            {
                validationContext.MemberName = prop.Name;
                Validator.TryValidateProperty(prop.GetValue(this), validationContext, result);
            }

            return result;
        }
    }
}
