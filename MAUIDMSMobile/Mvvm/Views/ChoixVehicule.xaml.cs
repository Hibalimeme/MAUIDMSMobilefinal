using MAUIDMSMobile.Mvvm.ViewModels;
using Mopups.Pages;
using Mopups.Services;

namespace MAUIDMSMobile.Mvvm.Views;

public partial class ChoixVehicule : PopupPage
{
    RendezVousViewModel RVM;
    public ChoixVehicule(RendezVousViewModel VM)
    {
        RVM = VM;
        BindingContext = RVM;
        InitializeComponent();
    }
    private async void ClosePopup_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}