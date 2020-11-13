using FluentValidation;

namespace Tecsys.Exercise.Application.Products.Queries
{
    public class GetProductWithTextSearchQueryValidator : AbstractValidator<GetProductWithTextSearchQuery>
    {
        public GetProductWithTextSearchQueryValidator()
        {
            RuleFor(x => x.SearchText)
                .MinimumLength(2)
                .WithMessage("Search text should has at least 2 characters");
        }
    }
}
