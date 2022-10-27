using App.Business.Core.Models;
using FluentValidation;

namespace App.Business.Core.Services
{
    public abstract class BaseService
    {
        protected bool ExecutarValidacao<TValidation, TEntity>(TValidation validacao, TEntity entidade)
            where TValidation : AbstractValidator<TEntity>
            where TEntity : Entity
        {
            var validator = validacao.Validate(entidade);
            return validator.IsValid;
        }
    }
}