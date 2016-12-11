using System;
using System.ServiceModel;
using Caliburn.Micro;
using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using ServiceWorker.Worker;


namespace UI.ViewModels
{
    public class AppViewModel : Conductor<object>
    {
        #region field

        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly IWindsorContainer _windsorContainer;
        private readonly ServicePcWorker _servicePcWorker;

        #endregion





        #region ctor

        public AppViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, IWindsorContainer windsorContainer)  
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _windsorContainer = windsorContainer;

           _servicePcWorker = new ServicePcWorker();
           _servicePcWorker.OpenEndPoint();

        }

        #endregion

    }
}