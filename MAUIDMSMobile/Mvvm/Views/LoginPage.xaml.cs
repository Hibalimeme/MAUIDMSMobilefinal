namespace MAUIDMSMobile.Mvvm.Views;

using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Services;
using MAUIDMSMobile.Mvvm.ViewModels;
using YourApp.Services;
using MAUIDMSMobile.Static;
using Mopups.Interfaces;
using Mopups.Services;

public partial class LoginPage : ContentPage
{
    IPopupNavigation popupNavigation;
    CompteViewModel VM;

    public LoginPage()
	{
     
        VM = new CompteViewModel(this.Navigation);

        BindingContext = VM;
   
        InitializeComponent();
	}

    private async void OnCreateAccountTapped(object sender, EventArgs e)
    {
        if (!VM.IsBusy)
        {
            VM.IsBusy = true;
            try
            {
                await Navigation.PushAsync(new CreationComptePage());
            }
            finally
            {
                VM.IsBusy = false;
            }
        }
    }

    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        if (!VM.IsBusy)
        {
            VM.IsBusy = true;
            try
            {
                await Navigation.PushAsync(new Motdepasseoublié()); // Redirige vers une autre page
            }
            finally
            {
                VM.IsBusy = false;
            }
        }
       
    }
  
    protected override void OnAppearing()
    {
        base.OnAppearing();

        bool rememberMeState = VM.LoadRememberMeState(); // Appel de la méthode

        if (rememberMeState == true)
        {
            // Redirection vers la page de connexion
            MainThread.InvokeOnMainThreadAsync(async () =>
            {
                App.Current.MainPage = new NavigationPage(new Homepage());
            });
        }
    }
    //private async void HOME(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new Homepage()); // Redirige vers une autre page
    //}
    private async void OnlangueTapped(object sender, EventArgs e)
    {
        if (!VM.IsBusy)
        {
            VM.IsBusy = true;
            try
            {
                await MopupService.Instance.PushAsync(new Langue());
            }
            finally
            {
                VM.IsBusy = false;
            }
        }
      
    }
    


}
