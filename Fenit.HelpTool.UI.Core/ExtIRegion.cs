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

        public static void Initialize(this IRegionManager regionManager, object newView, string name, string region)
        {
            var mainRegion = regionManager.Regions[region];
            mainRegion.Add(newView, name);
        }

        public static void Initialize(this IRegionManager regionManager, object newView, string name)
        {
            regionManager.Initialize(newView, name, "ContentRegion");
        }

        public static void Activate(this IRegionManager regionManager, string name, string region)
        {
            var mainRegion = regionManager.Regions[region];
            var moduleAView = mainRegion.GetView(name);
            mainRegion.Activate(moduleAView);
        }

        public static void Activate(this IRegionManager regionManager, string name)
        {
            regionManager.Activate(name, "ContentRegion");
        }
    }
}