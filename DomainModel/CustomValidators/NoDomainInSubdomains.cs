using Library.models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DomainModel.CustomValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NoDomainInSubdomains : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var bookDomain = (BookDomain)validationContext.ObjectInstance;

            if (bookDomain.BookSubdomains.Any(subdomain => subdomain.DomainName == bookDomain.DomainName))
            {
                return new ValidationResult("Domain name must be unique within subdomains!");
            }

            var currentDomain = bookDomain;
            while (currentDomain.ParentDomain != null)
            {
                currentDomain = currentDomain.ParentDomain;

                if (currentDomain.DomainName == bookDomain.DomainName)
                {
                    return new ValidationResult("Circular dependency detected! Domain name must be unique within parent domains!");
                }
            }

            return ValidationResult.Success;
        }
    }
}
