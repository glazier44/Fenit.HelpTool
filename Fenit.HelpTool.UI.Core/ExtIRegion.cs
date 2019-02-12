using System;
using System.Linq;
using System.Windows;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

namespace Fenit.HelpTool.UI.Core
{
    public static class ExtIRegion
    {
        public static void Activate(this IRegionManager regionManager, string name, string region)
        {
            var mainRegion = regionManager.Regions[region];
            NavigationSwitch(mainRegion, name);
            var moduleAView = mainRegion.GetView(name);
            mainRegion.Activate(moduleAView);
        }

        private static void NavigationSwitch(IRegion region, string targetViewName)
        {
            foreach (var sourceViews in region.ActiveViews)
            {
                var sourceNavigation = ExtractNavigationAware(sourceViews);
                sourceNavigation?.OnNavigatedFrom(null);
            }

            var targetView = region.GetView(targetViewName);
            var targetNavigation = ExtractNavigationAware(targetView);
            targetNavigation?.OnNavigatedTo(null);
        }

        private static INavigationAware ExtractNavigationAware(object view)
        {
            if (view is FrameworkElement frameworkElement)
            {
                if (frameworkElement.DataContext is INavigationAware navigation)
                {
                    return navigation;
                }
            }
            return null;
        }

        public static void Activate(this IRegionManager regionManager, string name)
        {
            regionManager.Activate(name, "ContentRegion");
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


        public static bool IsActive(this IRegionManager regionManager, Type viewModelType)
        {
            var mainRegion = regionManager.Regions["ContentRegion"];
            return Find(mainRegion, viewModelType) != null;
        }

        public static void LoadModuleIfExist(this IModuleManager moduleManager, IModuleCatalog moduleCatalog,
            string name)
        {
            if (moduleCatalog.Modules.Any(w => w.ModuleName == name)) moduleManager.LoadModule(name);
        }


        public static void RequestNavigateContentRegion(this IRegionManager region, string view)
        {
            region.RequestNavigate("ContentRegion", view);
        }


        private static object Find(IRegion region, Type viewModelType)
        {
            foreach (var view in region.ActiveViews.OfType<FrameworkElement>())
                if (view.DataContext != null &&
                    viewModelType.IsInstanceOfType(view.DataContext))
                    return view.DataContext;
            return null;
        }
    }
}