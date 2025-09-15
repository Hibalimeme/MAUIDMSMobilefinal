using MAUIDMSMobile.Mvvm.ViewModels;
using Mopups.Pages;
using Mopups.Services;

namespace MAUIDMSMobile.Mvvm.Views;

public partial class devisPOPUP : PopupPage
{

    DevisViewModel DVM;
    public devisPOPUP(DevisViewModel VM)
	{
        DVM = VM;
        BindingContext = DVM;
        InitializeComponent();
        
	}
    private async void ClosePopup_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}