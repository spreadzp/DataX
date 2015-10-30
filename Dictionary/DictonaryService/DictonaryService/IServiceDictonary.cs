using System.ServiceModel;

namespace DictonaryService
{
    [ServiceContract(SessionMode = SessionMode.Required)]
   
    public interface IServiceDictonary
    {
        [OperationContract]
        string[] FindTranslate(int roomDictionary, string searchWord);

        [OperationContract(IsInitiating = true)]
        bool ConnectClient();

        [OperationContract]
        bool UpdateWords(int roomDictionary, string updateSearchWord, string[] updateWordsTranslate);

        [OperationContract]
        bool AddWord(int roomDictionary, string newWord);

        [OperationContract]
        bool DeleteWord(int roomDictionary, string deleteWord);

        [OperationContract(IsOneWay = true, IsTerminating = true)]
        void DisconnectClient();
    }
}
