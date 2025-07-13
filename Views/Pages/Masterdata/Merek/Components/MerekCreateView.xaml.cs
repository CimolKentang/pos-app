using inovasyposmobile.ViewModels.Masterdata.Merek;

namespace inovasyposmobile.Views.Pages.Masterdata.Merek.Components;

public partial class MerekCreateView : ContentPage
{
	private readonly MerekCreateViewModel _merekCreateViewModel;
	public MerekCreateView(MerekCreateViewModel merekCreateViewModel)
	{
		InitializeComponent();
		BindingContext = _merekCreateViewModel = merekCreateViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_merekCreateViewModel.InitDataCommand.Execute(null);
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		_merekCreateViewModel.ClearDataCommand.Execute(null);
	}
}