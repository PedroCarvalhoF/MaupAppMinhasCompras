using MinhasCompras.Views;

namespace MinhasCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();            
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {                      
            return new Window(new NavigationPage(new ListaProdutoPage()));
        }
    }
}