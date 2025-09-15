using MAUIDMSMobile.Mvvm.ViewModels;

namespace MAUIDMSMobile.Mvvm.Views;

public partial class HistoriquePage : ContentPage
{
    RendezVousViewModel RVM;
    public HistoriquePage()
	{
        RVM = new RendezVousViewModel(this.Navigation);
        BindingContext = RVM;

        InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RVM.ChargerHistoriqeAsync();


    }
}