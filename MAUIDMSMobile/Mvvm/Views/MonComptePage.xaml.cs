using Mopups.Interfaces;
using Mopups.Services;

namespace MAUIDMSMobile.Mvvm.Views;

public partial class MonComptePage : ContentPage
{
    IPopupNavigation popupNavigation;
    public MonComptePage()
	{
		InitializeComponent();
	}
    private async void OndeconnectePressed(object sender, EventArgs e)
    {
        MopupService.Instance.PushAsync(new LogoutConfirmationPopup());

    }
    private async void OnUPDATETapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new miseajourdecompte()); // Redirige vers une autre page
    }
    protected override bool OnBackButtonPressed()
    {

        App.Current.MainPage = new NavigationPage(new Homepage());

        return true;
    }

}