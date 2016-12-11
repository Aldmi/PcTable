using Caliburn.Micro;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ServiceWorker.Host;
using UI.ViewModels;
using WCFAvtodictor2PcTableContract.Contract;


namespace UI.Bootstrapper
{
    public class WindsorConfig : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
              .Register(Component.For<IWindsorContainer>().Instance(container).LifeStyle.Singleton)
              .Register(Component.For<AppViewModel>().LifeStyle.Singleton)
              .Register(Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifeStyle.Singleton)
              .Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifeStyle.Singleton);


            container.AddFacility<WcfFacility>()
                .Register(Component.For<IPcTableContract>().ImplementedBy<PcService>().Named("PcServiceResolver").LifestyleSingleton());
        }
    }
}