using MAUIDMSMobile.Mvvm.ViewModels;
using Mopups.Interfaces;
using Mopups.Services;
namespace MAUIDMSMobile.Mvvm.Views;
public partial class MesRendezVousPage : ContentPage
{
    IPopupNavigation popupNavigation;
    RendezVousViewModel RVM;
    public MesRendezVousPage()
	{
        RVM = new RendezVousViewModel(this.Navigation);
        BindingContext =  RVM;
        
      
        InitializeComponent();
	}
    
    //private async void OnOpenPopupClicked(object sender, EventArgs e)
    //{
    //    await MopupService.Instance.PushAsync(new Ajouterrendezvous());
    //}
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await RVM.ChargerRendezVousAsync();
        }
        catch (Exception e)
        {

            throw;
        }
      
    }
}