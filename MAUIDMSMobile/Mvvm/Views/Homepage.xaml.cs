using Mopups.Interfaces;
using Mopups.Services;

namespace MAUIDMSMobile.Mvvm.Views;

public partial class Homepage : ContentPage
{
    IPopupNavigation popupNavigation;
    public Homepage()
	{
		InitializeComponent();
	}
    
    private async void OnAjouterVehiculeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MesVéhiculesPage()); // Redirige vers une autre page
    }
    private async void OnGererRendezvousClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MesRendezVousPage()); // Redirige vers une autre page
    }
    private async void OnGererCompteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MonComptePage()); // Redirige vers une autre page
    }
    private async void OnConsulterHistoriqueClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HistoriquePage()); // Redirige vers une autre page
    }

    private async void OnAjouterDevisClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DevisPage()); // Redirige vers une autre page
    }

    
    //private async void OnBackButtonPressed(object sender, EventArgs e)
    //{

    //        await MopupService.Instance.PushAsync(new LogoutConfirmationPopup());



    //    //return base.OnBackButtonPressed();
    //}
    //protected override bool OnBackButtonPressed()
    //{

    //    MopupService.Instance.PushAsync(new LogoutConfirmationPopup());
    //    //return base.OnBackButtonPressed();
    //    return true;
    //}



}