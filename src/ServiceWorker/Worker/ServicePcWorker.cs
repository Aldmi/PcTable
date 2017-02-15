using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using ServiceWorker.Host;
using WCFAvtodictor2PcTableContract.Contract;
using WCFAvtodictor2PcTableContract.DataContract;

namespace ServiceWorker.Worker
{
    public class ServicePcWorker : IDisposable
    {
        // Ссылка на экзкмпляр ServiceHost.
        ServiceHost _service = null;

        private IDisposable DisposableDataSended { get; set; }



        public void OpenEndPoint()
        {
            if (_service == null)
            {
                string baseAddress = "http://localhost:8000/Service";


                var pcService = new PcService();
                DisposableDataSended= pcService.StatChange.Subscribe(DataSended, OnError, OnCompleted);
                _service = new ServiceHost(pcService, new Uri(baseAddress));


                //MEX EndPoint
                ServiceMetadataBehavior smb = _service.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (smb == null)
                    smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                _service.Description.Behaviors.Add(smb);
                _service.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                //IContract EndPoint
                //Зашищенное соединение 
                WSHttpBinding binding = new WSHttpBinding(SecurityMode.None, true);
                binding.OpenTimeout = new TimeSpan(0, 0, 20);
                binding.CloseTimeout = new TimeSpan(0, 0, 20);
                binding.SendTimeout = new TimeSpan(0, 0, 5);       //таймаут на Запрос-ответ
                binding.ReceiveTimeout = new TimeSpan(0, 0, 25);   //время жизни сесии (при бездействии клиентов)
                ServiceEndpoint endpoint = _service.AddServiceEndpoint(typeof(IPcTableContract), binding, "");

                _service.Open();
            }
        }




        #region RxEventHandler

        //Обработчики события.
        public void DataSended(UniversalDisplayType status) //вызовется если издатель вызовет StatChange.OnNext();  
        {

        }

        public void OnError(Exception ex) //вызовется если издатель вызовет StatChange.OnError();   
        {

        }

        public void OnCompleted() //вызовется если издатель вызовет StatChange.OnCompleted();    
        {

        }

        #endregion




        #region Dispose

        public void Dispose()
        {
            DisposableDataSended.Dispose();
        }

        #endregion

    }
}