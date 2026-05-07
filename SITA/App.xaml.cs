using SITA.src.Model;

namespace SITA
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Title = "SITA";

          
            window.Width = 1920;
            window.Height = 1080;

         

            return window;
        }
    }
}
