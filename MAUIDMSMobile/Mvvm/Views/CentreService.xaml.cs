using MAUIDMSMobile.Mvvm.ViewModels;
using Mopups.Pages;
using Mopups.Services;

namespace MAUIDMSMobile.Mvvm.Views;

public partial class CentreService : PopupPage
{
    RendezVousViewModel RVM;
   

    public CentreService(RendezVousViewModel VM)
	{
        RVM = VM;
        BindingContext = RVM;
        InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();

       /* await RVM.ChargerCentresServiceAsync();*/ // 👈 Ajoute cet appel ici
    }
    private async void ClosePopup_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}