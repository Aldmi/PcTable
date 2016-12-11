using System;
using System.Reactive.Subjects;
using System.ServiceModel;
using System.Threading.Tasks;
using WCFAvtodictor2PcTableContract.Contract;
using WCFAvtodictor2PcTableContract.DataContract;

namespace ServiceWorker.Host
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PcService : IPcTableContract
    {
        #region ctor

        public PcService()
        {
            StatChange = new Subject<UniversalDisplayType>();
        }

        #endregion



        #region Rx

        public ISubject<UniversalDisplayType> StatChange { get; set; }

        #endregion





        #region ImplementsIPcTableContract

        public async Task<bool> GetDisplayData(UniversalDisplayType displayData)
        {
            try
            {
                StatChange.OnNext(displayData);

                await Task.Delay(100);
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException<Exception>(ex, $"Запрос к PC табло вызвал исключение.");
            }

        }

        #endregion
    }
}
