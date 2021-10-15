using Entidades.Entidades;
using FluentValidation;

namespace Aplicacion.Validaciones
{
    public class PropertyValidation : AbstractValidator<PropertyEntidad>
    {
        public PropertyValidation()
        {
            RuleFor(x => x.NameProperty).NotEmpty()
                .WithMessage("El nombre de la propiedad es requerido")
                .Length(10, 100)
                .WithMessage("El tamaño para el campo {PropertyName} no puede ser inferior a 10 caracteres o mayor a 100");
            RuleFor(x => x.Price).NotEmpty()
                .WithMessage("El valor para el campo {PropertyName} es requerido");
            RuleFor(x => x.YearProperty)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Addres).NotEmpty()
                .WithMessage("La dirección es requerida")
                .Length(2, 100)
                .WithMessage("El tamaño minimo para el campo {PropertyName} es de 2 letras o mayor a 100");

        }
    }
}
