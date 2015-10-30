using Caliburn.Micro;
using System;
using System.Linq;

namespace Client
{
    public class EditViewModel : PropertyChangedBase
    {
         private readonly IWindowManager _windowManager;
         private Client.ServiceReference1.ServiceDictonaryClient proxy; 
         private int indexLanguage;
         private string notice;         
         private string information;
         private string inputText;
         private string selectedLanguage;
         private string selectedWord;
         private BindableCollection<string> wordsTranslate;
         private BindableCollection<string> languages;

         public EditViewModel(Client.ServiceReference1.ServiceDictonaryClient proxi, int indexLanguage, string inputText, BindableCollection<string> words, IWindowManager windowManager)
        {
            this.proxy = proxi;
            this.InputText = inputText;
            this.indexLanguage = indexLanguage;
            Languages = new BindableCollection<string>(new string[] { "Русский - Английский", "English - Russian" });
            SelectedLanguage = Languages.ElementAt(indexLanguage);
            wordsTranslate = new BindableCollection<string>();
            Words = words;
            _windowManager = windowManager;
        }

        // см. комментарии в ClientViewModel
         public BindableCollection<string> Languages
         {
             get
             {
                 return languages;
             }

             set
             {
                 languages = value;
                 NotifyOfPropertyChange(() => Languages);
             }
         }

         public string InputText
         {
             get
             {
                 return inputText;
             }

             set
             {
                 inputText = value;
                 NotifyOfPropertyChange(() => InputText);
             }
         }

         public string AddEditWord
         {
             get
             {
                 return notice;
             }
             
            set
            {
             notice = value;
             NotifyOfPropertyChange(() => AddEditWord);
            }
         }

        public string SelectedLanguage
        {
            get
            {
                return selectedLanguage;
            }

            set 
            {
                selectedLanguage = value;
                NotifyOfPropertyChange(() => SelectedLanguage);
            }
        }

        public string Information
        {
            get
            {
                return information;
            }

            private set  
            {
                information = value;
                NotifyOfPropertyChange(() => Information);
            }
        }

        // см. комментарии в ClientViewModel
        public BindableCollection<string> Words
        {
            get
            {
                return wordsTranslate;
            }

            private set
            {
                wordsTranslate = value;
                NotifyOfPropertyChange(() => Words);
            }
        }

        public string SelectedWord
        {
            get
            {
                return selectedWord;
            }

            set 
            {
                selectedWord = value;
                NotifyOfPropertyChange(() => Words);
            }
        }

        // этот обработчик не нужен
        public void SelectedItemWord(string item)
        {
            notice = item;
        }

        // см. комментарии в ClientViewModel
        public void SelectedItemChanged(int index)
        {
            SelectedLanguage = Languages.ElementAt(index);
            indexLanguage = index;
        }

        public void AddWordToDb()
        {
            notice = "Вы  собираетесь добавить   в  БД   слово : \n" + InputText + " ?\n  словарь : " + Languages.ElementAt(indexLanguage);
            MessageViewModel messageViewModel = new MessageViewModel(notice);
            _windowManager.ShowDialog(messageViewModel); 
            if (messageViewModel.Result == true)
            {
                try
                {
                    if (proxy.AddWord(indexLanguage, InputText))
                    {
                        Information = "В результате добавления в БД будет добавлено слово : " + InputText
                        + ". Для проверки перейдите в форму с поиском и задайте слово: " + InputText;
                    }
                }
                catch (Exception ex)
                {
                    Information = ex.Message;
                }
            }
        }
       
        public void UpdateDb()
        {
            notice = "Вы собираетесь добавить или обновить в  БД   слова  : \n" + InputText + " - " + TranslatedWordsToOneString() + "?\n  словарь : " + Languages.ElementAt(indexLanguage);
            MessageViewModel messageViewModel = new MessageViewModel(notice);
            _windowManager.ShowDialog(messageViewModel);

            if (messageViewModel.Result == true)
            {
                try
                {
                    if (proxy.UpdateWords(indexLanguage, InputText, ConvertBindableToArray(Words)))
                    {
                        Information = "В результате обновления  БД вносятся изменения : " + InputText
                        + " - " + TranslatedWordsToOneString() + ". Для проверки перейдите в форму с поиском и задайте слово: " + InputText;
                    }
                }
                catch (Exception ex)
                {
                    Information = ex.Message;  
                }
            }
        }

        public void DeleteWordFromDb()
        {
            notice = "Вы собираетесь удалить из  БД  слово :\n " + InputText + " ?\n словарь :" + Languages.ElementAt(indexLanguage);
            MessageViewModel messageViewModel = new MessageViewModel(notice);
            _windowManager.ShowDialog(messageViewModel);

            if (messageViewModel.Result) 
            {
                try
                {
                    if (proxy.DeleteWord(indexLanguage, InputText))
                    {
                         Information = "В результате удаления из  БД будет удалено слово : " + InputText
                        + ". Для проверки перейдите в форму с поиском и задайте слово: " + InputText;
                    }
                }
                catch (Exception ex)
                {
                    Information = ex.Message;  
                }
            }
        }
      
        public void AddToList()
        {
            CheckingBeforeAddOrDelete(false);
            Information = "В результате добавления в список добавлено слово : " + AddEditWord;
        }

        public void DelFromList()
        {
            CheckingBeforeAddOrDelete(true);
            Information = "Из списка удалено слово : " + selectedWord;
        }

        private void CheckingBeforeAddOrDelete(bool isDelete)
        {
            if (isDelete == false && !Words.Contains(AddEditWord))
            {
                wordsTranslate.Add(AddEditWord);
                NotifyOfPropertyChange(() => Words);
            }

            if (isDelete) 
            {
                Information = "Из списка удалено слово : " + AddEditWord;
                Words.Remove(AddEditWord);
            }
        }
        
        private string TranslatedWordsToOneString()
        {
            return string.Join(" ", Words);
        }

        private string[] ConvertBindableToArray(BindableCollection<string> wordsEdit)
        {
            string[] messageForServer = null;

            if (wordsEdit.Count > 0)
            {
                messageForServer = new string[wordsEdit.Count];
                for (int i = 0; i < wordsEdit.Count; i++)
                {
                    if (!string.IsNullOrEmpty(wordsEdit[i]))
                    {
                        messageForServer[i] = wordsEdit[i];
                    }
                }
            }

            return messageForServer;
        }
    }
}
