using MAUIDMSMobile.Mvvm.ViewModels;
using MAUIDMSMobile.Services.Interfaces;
using Mopups.Pages;
using Mopups.Services;


namespace MAUIDMSMobile.Mvvm.Views;
public partial class AjouterVehiculePopup : PopupPage
{
    VehiculeViewModel VM;
  
    public AjouterVehiculePopup()
    {
        VM = new VehiculeViewModel(this.Navigation);
        BindingContext = VM;
        InitializeComponent();
       
    }

  

    private async void ClosePopup_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}
