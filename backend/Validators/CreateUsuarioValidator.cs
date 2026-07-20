using FluentValidation;
using backend_petshop.DTOs;

namespace backend_petshop.Validators
{
    public class CreateUsuarioValidator : AbstractValidator<CreateUsuarioDto>
    {
        public CreateUsuarioValidator()
        {
            RuleFor(u => u.Login)
                .NotEmpty().WithMessage("Login é obrigatório.")
                .MaximumLength(35).WithMessage("Login deve ter no máximo 35 caracteres.");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .MinimumLength(5).WithMessage("Senha deve ter no mínimo 5 caracteres.");
        }
    }

    public class CreateAnimalValidator : AbstractValidator<CreateAnimalDto>
    {
        public CreateAnimalValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(100);

            RuleFor(a => a.Peso)
                .GreaterThan(0).WithMessage("Peso deve ser maior que zero.");

            RuleFor(a => a.DataNascimento)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Data de nascimento não pode ser futura.");

            RuleFor(a => a.Especie)
                .NotEmpty().WithMessage("Espécie é obrigatória.")
                .MaximumLength(100);

            RuleFor(a => a.TutorId)
                .GreaterThan(0).WithMessage("Tutor é obrigatório.");
        }
    }

    public class UpdateAnimalValidator : AbstractValidator<UpdateAnimalDto>
    {
        public UpdateAnimalValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(100);

            RuleFor(a => a.Peso)
                .GreaterThan(0).WithMessage("Peso deve ser maior que zero.");

            RuleFor(a => a.DataNascimento)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Data de nascimento não pode ser futura.");

            RuleFor(a => a.Especie)
                .NotEmpty().WithMessage("Espécie é obrigatória.")
                .MaximumLength(100);

            RuleFor(a => a.TutorId)
                .GreaterThan(0).WithMessage("Tutor é obrigatório.");
        }
    }

    public class CreateTutorValidator : AbstractValidator<CreateTutorDto>
    {
        public CreateTutorValidator()
        {
            RuleFor(t => t.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(100);

            RuleFor(t => t.DataNascimento)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória.");

            RuleFor(t => t.CEP)
                .NotEmpty().WithMessage("CEP é obrigatório.")
                .Length(8).WithMessage("CEP deve ter 8 caracteres.");
        }
    }

    public class UpdateTutorValidator : AbstractValidator<UpdateTutorDto>
    {
        public UpdateTutorValidator()
        {
            RuleFor(t => t.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(100);

            RuleFor(t => t.DataNascimento)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória.");

            RuleFor(t => t.CEP)
                .NotEmpty().WithMessage("CEP é obrigatório.")
                .Length(8).WithMessage("CEP deve ter 8 caracteres.");
        }
    }
}
