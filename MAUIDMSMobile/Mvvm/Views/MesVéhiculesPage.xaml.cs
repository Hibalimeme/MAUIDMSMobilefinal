using MAUIDMSMobile.Mvvm.ViewModels;
using MAUIDMSMobile.Mvvm.Views;
using Mopups.Interfaces;
using Mopups.Services;

namespace MAUIDMSMobile.Mvvm.Views;

public partial class MesVéhiculesPage : ContentPage
{
    IPopupNavigation popupNavigation;
    VehiculeViewModel VM;
    public MesVéhiculesPage()
	{
        VM = new VehiculeViewModel(this.Navigation);
        BindingContext = VM;
        InitializeComponent();
	}
    private async void OnOpenPopupClicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PushAsync(new AjouterVehiculePopup());
    }
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await VM.LoadPendingValidationVehiclesAsync();
            await VM.LoadValidatedVehiclesAsync();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}