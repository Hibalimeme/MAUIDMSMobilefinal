using Mopups.Pages;
using Mopups.Services;

namespace MAUIDMSMobile.Mvvm.Views;

public partial class LogoutConfirmationPopup :  PopupPage

{
    public LogoutConfirmationPopup()
    {
        try
        {

            InitializeComponent();
        }
        catch (Exception e)
        {

            throw;
        }
	}
    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
        }
        catch (Exception ex )
        {

            throw;
        }
    }

    private async void OnYesClicked(object sender, EventArgs e)
    {


        try
        {
            Preferences.Clear();
           

            // Close ALL popups BEFORE navigating
            await MopupService.Instance.PopAllAsync();

            // Wait to ensure popup is closed
            await Task.Delay(200);

            // Reset MainPage to Login
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
        catch (Exception ex )
        {

            throw;
        }
    }

     // Redirige vers une autre page
   
    private async void OnNoClicked(object sender, EventArgs e)
    {


        try
        {
            await MopupService.Instance.PopAsync();
        }
        catch (Exception ex )
        {

            throw;
        }
    }

}