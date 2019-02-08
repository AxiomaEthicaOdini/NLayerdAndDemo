using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Tüm Alanları Doldurunuz.");
            RuleFor(p => p.CategoryId).NotEmpty().WithMessage("Tüm Alanları Doldurunuz.");
            RuleFor(p => p.UnitPrice).NotEmpty().WithMessage("Tüm Alanları Doldurunuz.");
            RuleFor(p => p.UnitsInStock).NotEmpty().WithMessage("Tüm Alanları Doldurunuz.");
            RuleFor(p => p.QuantityPerUnit).NotEmpty().WithMessage("Tüm Alanları Doldurunuz.");

            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitsInStock).GreaterThanOrEqualTo((short)0);
            RuleFor(p => p.UnitPrice).GreaterThan(10).When(p => p.CategoryId==2);

        }
    }
}
