using DictonaryService.DbClasses;
using System.ServiceModel;

namespace DictonaryService 
{ 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ServiceForDictonary : IServiceDictonary 
    {
        private IDictonaryRepository repository;

        public ServiceForDictonary()
        {
            repository = new DictonaryRepository();
        }

        public ServiceForDictonary(IDictonaryRepository _repository)
        {
            repository = _repository;
        }

        public bool ConnectClient()
        {
           return true;
        }

        public string[] FindTranslate(int roomDictonary, string searchWord)
        {
            var translatedWords = repository.FindTranslateIntoDb(roomDictonary, searchWord);
            return translatedWords;
        }

        public bool UpdateWords(int roomDictonary, string updateSearchWord, string[] updateWordsTranslate)
        {
            var resultUpdated = repository.UpdateWordsToDb(roomDictonary, updateSearchWord, updateWordsTranslate);
           return resultUpdated;
        }

        public bool AddWord(int roomDictonary, string newWord)
        {
            var resultAddedWord = repository.AddWordToDb(roomDictonary, newWord);
            return resultAddedWord;
        }

        public bool DeleteWord(int roomDictonary, string deleteWord)
        {
            var resultDeletedWord = repository.DeleteWordFromDb(roomDictonary, deleteWord);
            return resultDeletedWord;
        }

        public void DisconnectClient()
        {
        }
    }
}
