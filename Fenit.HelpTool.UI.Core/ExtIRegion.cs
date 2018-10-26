using System.Linq;
using Prism.Regions;

namespace Fenit.HelpTool.UI.Core
{
    public static class ExtIRegion
    {
        public static void ClearRegion(this IRegion region)
        {
            if (region != null)
            {
                var objArray = region.Views.ToArray();
                foreach (var el in objArray) region.Deactivate(el);
            }
        }

        /// <summary>
        /// 
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
    }
}