using inovasyposmobile.ViewModels.Masterdata.Satuan;

namespace inovasyposmobile.Views.Pages.Masterdata.Satuan.Components;

public partial class SatuanCreateView : ContentPage
{
	private readonly SatuanCreateViewModel _satuanCreateViewModel;
	public SatuanCreateView(SatuanCreateViewModel satuanCreateViewModel)
	{
		InitializeComponent();
		BindingContext = _satuanCreateViewModel = satuanCreateViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_satuanCreateViewModel.InitDataCommand.Execute(null);
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		_satuanCreateViewModel.ClearDataCommand.Execute(null);
	}
}