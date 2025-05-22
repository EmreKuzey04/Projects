using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest,TResponse>:IPipelineBehavior<TRequest, TResponse> where TRequest : class,IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);
             var validationResults = _validators.Select(x=> x.Validate(context));
            var errorList = validationResults
                .SelectMany(x=> x.Errors)
                .Where(x=> x != null)
                .ToList();

            if(errorList.Any())
                throw new ValidationException(errorList);

            return await next();
           
        }
    }
}
