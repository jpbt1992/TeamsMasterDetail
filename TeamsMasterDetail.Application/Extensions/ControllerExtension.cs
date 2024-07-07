using Microsoft.AspNetCore.Mvc;

namespace TeamsMasterDetail.Application.Extensions
{
    public static class ControllerExtension
    {
        public static string GetControllerName(this Type controllerType)
        {
            if (!controllerType.IsController())
            {
                throw new ArgumentException("Type must be a controller class.");
            }

            string name = controllerType.Name;
            return name.EndsWith("Controller") ? name[..^"Controller".Length] : name;
        }

        private static bool IsController(this Type type)
            => type.IsClass && typeof(ControllerBase).IsAssignableFrom(type);
    }
}
