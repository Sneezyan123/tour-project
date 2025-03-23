using Microsoft.AspNetCore.Mvc.ModelBinding;
using TourProject.Models;

namespace TourProject.Infrastructure.ModelBinders
{
    public class FromUserBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var user = bindingContext.HttpContext.Items["user"] as User;
            if(user is null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
            bindingContext.Result = ModelBindingResult.Success(user);
            return Task.CompletedTask;
        }
    }
}
