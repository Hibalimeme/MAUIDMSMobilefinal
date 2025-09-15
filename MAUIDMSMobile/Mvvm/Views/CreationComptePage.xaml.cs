
using MAUIDMSMobile.Services;
using MAUIDMSMobile.Mvvm.Models;
using YourApp.Services;
using MAUIDMSMobile.Mvvm.ViewModels;
using MAUIDMSMobile.Static;
using MAUIDMSMobile.Mvvm.Views;



namespace MAUIDMSMobile.Mvvm.Views
{
    public partial class CreationComptePage : ContentPage
    {
        CompteViewModel VM;

        public CreationComptePage()
        {
            VM = new CompteViewModel(this.Navigation);

            BindingContext = VM;

            InitializeComponent();
        }


  
        //private async void Onlogin(object sender, TappedEventArgs e)
        //{
        //    App.Current.MainPage = new NavigationPage(new LoginPage());
        //}
        protected override bool OnBackButtonPressed()
        {
           
            App.Current.MainPage = new NavigationPage(new LoginPage());

            return true;
        }

        //private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        //{
        //    App.Current.MainPage = new NavigationPage(new LoginPage());
        //}
    }
}
