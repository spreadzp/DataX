using Caliburn.Micro;

namespace Client
{
    public class MessageViewModel : Screen
    {
        private string message;
        private bool result;

        public MessageViewModel(string message)
        {
            this.message = message;
        }

        public string Message   // зачем это свойство?  - наполняет TextBlock текстом, иначе окно будет чистым с кнопками только
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public bool Result
        {
            get
            {
                return result;
            }
        }

        public void Ok()
        {
            result = true;
            TryClose(true);
        }

        public void Cancel()
        {
            result = false;
            TryClose(false); 
        }
    }
}
