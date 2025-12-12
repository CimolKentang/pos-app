using inovasyposmobile.ViewModels.Masterdata.Produk;

namespace inovasyposmobile.Views.Pages.Masterdata.Produk.Components;

public partial class ProdukStokCreateView : ContentPage
{
	public ProdukStokCreateView(ProdukCreateViewModel produkCreateViewModel)
	{
		InitializeComponent();
		BindingContext = produkCreateViewModel;
	}
}