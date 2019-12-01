using CastleDynamicProxy.Requests;
using FluentValidation;

namespace CastleDynamicProxy.Validators
{
    public class TestRequestValidator : AbstractValidator<TestRequest>
    {
        public TestRequestValidator()
        {
            RuleFor(_ => _.Name).NotEmpty();
            RuleFor(_ => _.Ids).Must(_ => _ != null && _.Count > 0)
                .WithMessage("Ids 不能为空");
        }
    }
}
