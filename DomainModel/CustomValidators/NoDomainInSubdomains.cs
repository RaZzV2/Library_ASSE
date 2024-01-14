namespace DomainModel.CustomValidators
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Library.Models;

    /// <summary>
    /// The <see cref="NoDomainInSubdomains"/> attribute is a custom validation attribute for ensuring that a BookDomain's 
    /// domain name is unique within its subdomains and parent domains, preventing circular dependencies.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NoDomainInSubdomains : ValidationAttribute
    {
        /// <summary>
        /// Validates whether the specified BookDomain's domain name is unique within its subdomains and parent domains.
        /// </summary>
        /// <param name="value">The value of the property to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the validation was successful.</returns>
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
