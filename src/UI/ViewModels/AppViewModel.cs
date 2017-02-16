using System;
using System.ServiceModel;
using Caliburn.Micro;
using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using ServiceWorker.Worker;
using WCFAvtodictor2PcTableContract.DataContract;


namespace UI.ViewModels
{
    public class AppViewModel : Conductor<object>, IHandle<UniversalDisplayType>
    {
        #region field

        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly IWindsorContainer _windsorContainer;
        private readonly ServicePcWorker _servicePcWorker;

        #endregion


        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }


        public BindableCollection<UniversalDisplayType> UniversalDisplayTypes { get; set; }= new BindableCollection<UniversalDisplayType>();


        #region ctor

        public AppViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, IWindsorContainer windsorContainer)  
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _windsorContainer = windsorContainer;

            _eventAggregator.Subscribe(this);

            _servicePcWorker = new ServicePcWorker(eventAggregator);
           _servicePcWorker.OpenEndPoint();

            Message = "sfsfgfds";

        }

        #endregion





        #region Events

        public void Handle(UniversalDisplayType message)
        {

            Message = message.Message;

            UniversalDisplayTypes.Add(message);
        }

        #endregion

    }
}