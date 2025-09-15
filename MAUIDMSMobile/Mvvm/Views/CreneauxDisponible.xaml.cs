using MAUIDMSMobile.Mvvm.ViewModels;
using Mopups.Pages;
using Mopups.Services;

namespace MAUIDMSMobile.Mvvm.Views;

public partial class CreneauxDisponible : PopupPage
{
    RendezVousViewModel RVM;
    public CreneauxDisponible(RendezVousViewModel VM)
    {
        RVM = VM;
        BindingContext = RVM;
        InitializeComponent();
    }

    private void Crenaux_Selected_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }
    private async void ClosePopup_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}