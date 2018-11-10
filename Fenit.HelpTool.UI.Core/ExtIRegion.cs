using System.Linq;
using Prism.Regions;

namespace Fenit.HelpTool.UI.Core
{
    public static class ExtIRegion
    {
        public static void RequestNavigateContentRegion(this IRegionManager region, string view)
        {
            region.RequestNavigate("ContentRegion", view);
        }

        public static void ClearRegion(this IRegion region)
        {
            if (region != null)
            {
                var objArray = region.Views.ToArray();
                foreach (var el in objArray) region.Deactivate(el);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="name">Name region</param>
        public static void ClearRegion(this IRegionManager regionManager, string name)
        {
            if (regionManager.Regions.Any())
            {
                var region = regionManager.Regions[name];
                ClearRegion(region);
            }
        }

        public static void Initialize(this IRegionManager regionManager, object newView, string name)
        {
            var mainRegion = regionManager.Regions["ContentRegion"];
            mainRegion.Add(newView, name);
        }

        public static void Activate(this IRegionManager regionManager, string name)
        {
            var mainRegion = regionManager.Regions["ContentRegion"];
            var moduleAView = mainRegion.GetView(name);
            mainRegion.Activate(moduleAView);
        }
    }
}