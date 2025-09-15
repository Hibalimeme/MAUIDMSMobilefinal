namespace MAUIDMSMobile.Mvvm.Views;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Services;
using MAUIDMSMobile.Mvvm.ViewModels;
using YourApp.Services;
using MAUIDMSMobile.Static;
public partial class miseajourdecompte : ContentPage
{
    CompteViewModel VM;
  
    public miseajourdecompte()
	{
        VM = new CompteViewModel(this.Navigation);

        BindingContext = VM;

        InitializeComponent();
        

    }
    protected override bool OnBackButtonPressed()
    {

        try
        {
            App.Current.MainPage = new NavigationPage(new MonComptePage());

            return true;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}