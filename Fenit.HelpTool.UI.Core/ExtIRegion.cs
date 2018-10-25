using System.Linq;
using Prism.Regions;

namespace Fenit.HelpTool.UI.Core
{
    public static class ExtIRegion
    {
        public static void ClearRegion(this IRegion region)
        {
            var objArray = region.Views.ToArray();
            foreach (var el in objArray) region.Deactivate(el);
        }
    }
}