
namespace Application.Validators;
using FluentValidation;
using Application.Activities.Commands;
using System;

public class ActivityGameValidator : AbstractValidator<CreateActivityCommand>
{
    public ActivityGameValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name не может быть пустым")
             .MaximumLength(200).WithMessage("Name не может быть длиннее 200 символов");
        RuleFor(x => x.GameId).NotEmpty().WithMessage("GameId не может быть пустым");
    }
}