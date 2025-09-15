using MAUIDMSMobile.Mvvm.ViewModels;
using Mopups.Pages;
using Mopups.Services;

namespace MAUIDMSMobile.Mvvm.Views;

public partial class PackEntretien : PopupPage
{
    RendezVousViewModel RVM;
    public PackEntretien(RendezVousViewModel VM)
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