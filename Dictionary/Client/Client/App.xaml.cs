using System.Windows;

namespace Client
{
    public partial class App : Application
    {
        public App()
        {
            Bootstrapper = new AppBootstrapper();
        }

        private AppBootstrapper Bootstrapper
        { 
            get;
            set;
        }
    }
}
