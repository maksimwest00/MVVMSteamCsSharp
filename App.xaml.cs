using System.Windows;

namespace MVVM
{
    public partial class App : Application
    {
        // Надо вынести в отдельный класс
        public static MainViewModel MainViewModel { get; set; } = new MainViewModel();
    }
}
