﻿using BackendProject.Dto;
using FluentValidation;

namespace BackendProject.Validators
{
    public class BeerInsertValidation : AbstractValidator<BeerInserDto>
    {
        public BeerInsertValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre deber medir de 2 a 20 carateres");
            RuleFor(x => x.BrandID).NotNull().WithMessage(X => "La marca es obligatoria");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage(x => "Error con el valor enviado de marca");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage(x => "El {PropertyName} debe ser mayor a 0");
        }
    }
}