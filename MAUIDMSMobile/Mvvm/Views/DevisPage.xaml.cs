using MAUIDMSMobile.Mvvm.ViewModels;
using Mopups.Interfaces;
namespace MAUIDMSMobile.Mvvm.Views;

public partial class DevisPage : ContentPage
{
    IPopupNavigation popupNavigation;

    DevisViewModel DVM;
    public DevisPage()
	{
        DVM = new DevisViewModel();
        BindingContext = DVM;
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await DVM.ChargerDevisAsync();
  
    }
    



}