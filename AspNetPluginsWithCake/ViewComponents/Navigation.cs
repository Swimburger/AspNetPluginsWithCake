using AspNetPluginsWithCake.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetPluginsWithCake.ViewComponents
{
    public class Navigation : ViewComponent
    {
        private readonly NavigationService navigationService;

        public Navigation(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public IViewComponentResult Invoke()
        {
            var model = navigationService.GetNavigationItems();
            return View(model);
        }
    }
}
