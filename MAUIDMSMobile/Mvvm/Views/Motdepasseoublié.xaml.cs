
namespace MAUIDMSMobile.Mvvm.Views;
using MAUIDMSMobile.Mvvm.ViewModels;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Services;
using MAUIDMSMobile.Static;
using MAUIDMSMobile.Mvvm.ViewModels;
using YourApp.Services;

public partial class Motdepasseoublié : ContentPage
{
    CompteViewModel VM;
   
    public Motdepasseoublié()
	{
        VM = new CompteViewModel(this.Navigation);

        BindingContext = VM;

        InitializeComponent();
    }
    protected override bool OnBackButtonPressed()
    {

        App.Current.MainPage = new NavigationPage(new LoginPage());

        return true;
    }

}