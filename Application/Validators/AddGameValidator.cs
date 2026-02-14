using Application.Games.Commands;
using FluentValidation;

namespace Application.Validators;

public class AddGameValidator : AbstractValidator<AddGameCommand>
{

// Конфигурация валидации для команды добавления игры
    public AddGameValidator()
    {
        RuleFor(x => x.Title)
        .NotEmpty().WithMessage("Title is required.")
        .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Genre)
        .NotEmpty().WithMessage("Genre is required.")
        .MaximumLength(100).WithMessage("Genre cannot exceed 100 characters.");

        RuleFor(x => x.UserId)
        .NotEmpty().WithMessage("UserId is required.");
    }
}