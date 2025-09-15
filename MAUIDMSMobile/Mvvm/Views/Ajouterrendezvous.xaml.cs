using MAUIDMSMobile.Mvvm.ViewModels;
using Mopups.Interfaces;
using Mopups.Pages;
using Mopups.Services;
namespace MAUIDMSMobile.Mvvm.Views;


public partial class Ajouterrendezvous : PopupPage
{
    IPopupNavigation popupNavigation;
    RendezVousViewModel RVM;

    public Ajouterrendezvous(RendezVousViewModel VM)
    {
        RVM = VM;
        BindingContext = RVM;


        InitializeComponent();
    }
    //private async void OnVehiculeTapped(object sender, EventArgs e)
    //{
    //    await MopupService.Instance.PushAsync(new ChoixVehicule());
    //}
    ////private async void OnCentreServiceTapped(object sender, EventArgs e)
    ////{
    ////    await MopupService.Instance.PushAsync(new CentreService());
    ////}
    //private async void OnPackEntretienTapped(object sender, EventArgs e)
    //{
    //    await MopupService.Instance.PushAsync(new PackEntretien());
    //}
    //private async void OnVerifierCreneauxClicked(object sender, EventArgs e)
    //{
    //    await MopupService.Instance.PushAsync(new CreneauxDisponible());
    //}
    private async void ClosePopup_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }




}