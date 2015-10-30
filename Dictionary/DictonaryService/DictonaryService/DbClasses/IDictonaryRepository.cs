using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictonaryService.DbClasses
{
    public interface IDictonaryRepository
    {
       bool AddWordToDb(int roomDictonary, string newWord);

       bool DeleteWordFromDb(int roomDictonary, string deleteWord);

       string[] FindTranslateIntoDb(int roomDictonary, string searchWord);

       bool UpdateWordsToDb(int roomDictonary, string updateSearchWord, string[] updateWordsTranslate);
    }
}