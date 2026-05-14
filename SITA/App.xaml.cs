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
            window.Title = "SITA - Sistemas Integrados Tudo Azul";

#if WINDOWS
            // Define um tamanho grande para cobrir a maioria dos monitores
            window.Width = 1920;
            window.Height = 1080;

            // Centraliza
            window.X = 0;
            window.Y = 0;
#endif

            return window;
        }
    }
}