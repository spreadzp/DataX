using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;

namespace Client
{
    [Export(typeof(ClientViewModel))]
    public class ClientViewModel : PropertyChangedBase
    {
        private readonly IWindowManager _windowManager;
        private Client.ServiceReference1.ServiceDictonaryClient proxy = null;
        private string inputText;
        private string information;
        private string selectedLanguage;
        //private string selectedWord;
        private bool canConnect = true;
        private int indexLanguage;
        private BindableCollection<string> translatedwords;
        private BindableCollection<string> language;

        [ImportingConstructor]
        public ClientViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Languages = new BindableCollection<string>(new string[] { "Русский - Английский", "English - Russian" });
            translatedwords = new BindableCollection<string>();
            proxy = new ServiceReference1.ServiceDictonaryClient();
        }

        public bool BeConnectServer
        {
            get
            {
                return canConnect;
            }

            private set
            {
                canConnect = value;
                NotifyOfPropertyChange(() => BeConnectServer);
                NotifyOfPropertyChange(() => IsDisconnectServer);
            }
        }

        public bool IsDisconnectServer
        {
            get
            {
                return !canConnect;
            }
        }

        public BindableCollection<string> Words
        {
            get
            {
               return translatedwords;
            }

            //set // почему public?
            //    // по ходу, сеттер здесь вообще не нужен, просто инициализируй поле translatedwords в конструкторе (или при объявлении поля) и убери сеттер
            //    // та и геттер здесь пользы никакой не даёт :)
            //{
            //    translatedwords = value;
            //    NotifyOfPropertyChange(() => Words);
            //}
        }

        //public string SelectedWord
        //{
        //    get
        //    {
        //        return selectedWord;
        //    }

        //    set
        //    {
        //        selectedWord = value;
        //        NotifyOfPropertyChange(() => Words);
        //    }
        //}

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

        public string Information
        {
            get
            {
                return information;
            }

            set
            {
                information = value;
                NotifyOfPropertyChange(() => Information);
            }
        }

        public BindableCollection<string> Languages
        {
            get
            {
                return language;
            }

            set // зачем нужен сеттер?
            {
                language = value;
                NotifyOfPropertyChange(() => Languages);
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

        // не нужен обработчик  -  комбобокс при выборе элемента приложение погибает ,
        //а мне нужно выцепить порядковое число в списке и передать егов запрос  как № словаря в БД
        public void SelectedItemChanged(int index)
        {
            SelectedLanguage = Languages.ElementAt(index);
            indexLanguage = index;
        }

        public void ConnectingServer()
        {
            Information = "Устанавливается связь с сервером !";

            try
            {
                BeConnectServer = !proxy.ConnectClient();
                Information = "Связь с сервером установлена !";
            }
            catch (CommunicationException ex)
            {
                Information = "Возникли проблемы при соединении с сервером, попробуйте еще раз соединиться !\n" + ex.Message;
            }
        }

        public void Findtranslate()
        {
              string notice = "Вы собираетесь найти перевод  в  БД   слова :\n " + InputText + "\n словарь : " + Languages[indexLanguage];
              MessageViewModel messageViewModel = new MessageViewModel(notice);
              _windowManager.ShowDialog(messageViewModel);

              if (true == messageViewModel.Result)
              {
              try
              {
                  Words.Clear();
                  Information = "На сервер отправлен запрос на перевода слова : " + InputText;
                  var wordsTranslate = proxy.FindTranslate(indexLanguage, InputText);
                 
                  if (wordsTranslate != null)
                  {
                      Information = "Из базы данных получен  перевода слова : " + InputText;
                      translatedwords.AddRange(wordsTranslate);
                  }
                  else
                  {
                      Information = "В базе данных нет перевода слова : " + InputText;
                  }
              }
              catch (CommunicationException ex) 
              {
                  Information = "Возникли проблемы с отправкой пакета, попробуйте соединиться с сервером и отправить новый запрос!\n" + ex.Message;
                  BeConnectServer = true;
              }
            }
       }

        public void Editword()
        {
            _windowManager.ShowWindow(new EditViewModel(proxy, indexLanguage, InputText, Words, _windowManager));
        }

        public void DisconnectServer()
        {
            proxy.DisconnectClient();
            Information = "Связь с сервером отсутствует!";
            BeConnectServer = true;  
        }
    }
}