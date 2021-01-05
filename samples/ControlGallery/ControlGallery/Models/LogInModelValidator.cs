// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using FluentValidation;

namespace ControlGallery.Models
{
    public class LogInModelValidator : AbstractValidator<LogInModel>
    {
        public LogInModelValidator()
        {
            RuleFor(m => m.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(m => m.Password)
                .NotNull()
                .Length(8, 16);

            RuleFor(m => m.ConfirmPassword)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .Equal(m => m.Password)
                .WithMessage("'{PropertyName}' should be equal to '{ComparisonProperty}'");
        }
    }
}
