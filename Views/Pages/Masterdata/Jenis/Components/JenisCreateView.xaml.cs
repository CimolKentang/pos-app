using inovasyposmobile.ViewModels.Masterdata.Jenis;

namespace inovasyposmobile.Views.Pages.Masterdata.Jenis.Components;

public partial class JenisCreateView : ContentPage
{
	private readonly JenisAddViewModel _jenisAddViewModel;
	public JenisCreateView(JenisAddViewModel jenisAddViewModel)
	{
		InitializeComponent();
		BindingContext = _jenisAddViewModel = jenisAddViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_jenisAddViewModel.InitDataCommand.Execute(null);
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		_jenisAddViewModel.ClearDataCommand.Execute(null);
	}
}