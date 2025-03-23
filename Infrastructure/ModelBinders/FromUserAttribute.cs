using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TourProject.Models;

namespace TourProject.Infrastructure.ModelBinders
{
    public class FromUserAttribute: ModelBinderAttribute
    {
        public FromUserAttribute() : base(typeof(FromUserBinder))
        {

        }
    }
}
