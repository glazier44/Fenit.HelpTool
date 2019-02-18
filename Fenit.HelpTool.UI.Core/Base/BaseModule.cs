using System;
using Prism.Ioc;
using Prism.Modularity;
using Unity;
using Unity.Injection;

namespace Fenit.HelpTool.UI.Core.Base
{
    public abstract class BaseModule : IModule
    {
        private readonly IUnityContainer _container;
        private IContainerRegistry _containerRegistry;

        protected BaseModule(IUnityContainer container)
        {
            _container = container;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _containerRegistry = containerRegistry;
            RegisterModuleTypes(containerRegistry);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            OnInitializedModule(containerProvider);
        }

        public abstract void RegisterModuleTypes(IContainerRegistry containerRegistry);
        public abstract void OnInitializedModule(IContainerProvider containerProvider);

        public void RegisterDialog<TView, TViewModel>(string dialogName)
            where TView : IDialogView
            where TViewModel : IDialogViewModel
        {
            if (string.IsNullOrEmpty(dialogName)) throw new ArgumentNullException("dialogName");

            if (!_container.IsRegistered<TViewModel>())
                _container.RegisterType<TViewModel>();

            _container.RegisterType<IDialogView, TView>(dialogName, new InjectionProperty("DataContext", new ResolvedParameter<TViewModel>()));
        }
    }
}