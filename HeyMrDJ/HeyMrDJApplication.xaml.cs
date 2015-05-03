#region Hey Mr. DJ
// Hey Mr. DJ put a record on
// I wanna dance with my baby.
// https://github.com/picrap/HeyMrDJ
#endregion

namespace HeyMrDJ
{
    using System.Windows;
    using ArxOne.MrAdvice.Utility;
    using ViewModel;
    using ArxOne.MrAdvice.MVVM.Navigation;

    public partial class HeyMrDJApplication 
    {
        public HeyMrDJApplication()
        {
            Startup += OnStartup;
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var navigator = this.GetNavigator();
            navigator.Show<HomeViewModel>();
        }
    }
}
