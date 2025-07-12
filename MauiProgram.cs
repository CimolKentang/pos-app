using System.Net.Http.Headers;
using CommunityToolkit.Maui;
using inovasyposmobile.Handlers;
using inovasyposmobile.Services.Implementations;
using inovasyposmobile.Services.Implementations.Auth;
using inovasyposmobile.Services.Implementations.Masterdata;
using inovasyposmobile.Services.Implementations.Transaksi;
using inovasyposmobile.Services.Interfaces;
using inovasyposmobile.Services.Interfaces.Auth;
using inovasyposmobile.Services.Interfaces.Masterdata;
using inovasyposmobile.Services.Interfaces.Transaksi;
using inovasyposmobile.ViewModels;
using inovasyposmobile.ViewModels.Auth;
using inovasyposmobile.ViewModels.Masterdata.Satuan;
using inovasyposmobile.ViewModels.Masterdata.Jenis;
using inovasyposmobile.ViewModels.Masterdata.Merek;
using inovasyposmobile.ViewModels.Masterdata.Produk;
using inovasyposmobile.ViewModels.Masterdata.Supplier;
using inovasyposmobile.ViewModels.Transaksi;
using inovasyposmobile.ViewModels.Transaksi.Penjualan;
using inovasyposmobile.Views.Pages.Transaksi.Penjualan;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Toolkit.Hosting;
using inovasyposmobile.ViewModels.Masterdata.Pelanggan;

namespace inovasyposmobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NNaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXtcdHRVR2JeU0Z+X0dWYUA=");

		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureEssentials()
			.ConfigureSyncfusionCore()
			.ConfigureSyncfusionToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				// fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("MaterialSymbolsOutlined.ttf", "MaterialSymbols");
			})
			.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler<Entry, CustomEntryHandler>();
				handlers.AddHandler<Editor, CustomEditorHandler>();
				handlers.AddHandler<Switch, CustomSwitchHandler>();
			});

		// http client with base address
		builder.Services.AddHttpClient("InovasyAPI", client =>
		{
			client.BaseAddress = new Uri("https://api-pos.inovasy.com/api/v1/");
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json")
			);
		});

		builder.Services.AddHttpClient("InovasyCustomerAPI", client =>
		{
			client.BaseAddress = new Uri("https://api-customer.inovasy.com/api/v1/");
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json")
			);
		});

		// register services
		builder.Services.AddSingleton<IAuthService, AuthService>();
		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<IPenjualanService, PenjualanService>();
		builder.Services.AddSingleton<PenjualanListViewModel>();		
		builder.Services.AddSingleton<PenjualanDetailViewModel>();
		builder.Services.AddSingleton<PenjualanCreateViewModel>();

		builder.Services.AddSingleton<IProdukService, ProdukService>();
		builder.Services.AddSingleton<ProdukListViewModel>();
		builder.Services.AddSingleton<ProdukDetailViewModel>();

		builder.Services.AddSingleton<IJenisService, JenisService>();
		builder.Services.AddSingleton<JenisListViewModel>();
		builder.Services.AddSingleton<JenisDetailViewModel>();
		builder.Services.AddSingleton<JenisAddViewModel>();

		builder.Services.AddSingleton<MerekService>();
		builder.Services.AddSingleton<MerekListViewModel>();

		builder.Services.AddSingleton<SatuanService>();
		builder.Services.AddSingleton<SatuanListViewModel>();

		builder.Services.AddSingleton<SupplierService>();
		builder.Services.AddSingleton<SupplierListViewModel>();

		builder.Services.AddSingleton<PelangganService>();
		builder.Services.AddSingleton<PelangganListViewModel>();

		builder.Services.AddSingleton<IValueDisplayService, ValueDisplayService>();
		builder.Services.AddSingleton<ValueDisplayViewModel>();

		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

		// builder.Services.Register

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
