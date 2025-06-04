using System.Net.Http.Headers;
using CommunityToolkit.Maui;
using inovasyposmobile.Handlers;
using inovasyposmobile.Services.Implementations;
using inovasyposmobile.Services.Implementations.Masterdata;
using inovasyposmobile.Services.Implementations.Transaksi;
using inovasyposmobile.Services.Interfaces;
using inovasyposmobile.Services.Interfaces.Masterdata;
using inovasyposmobile.Services.Interfaces.Transaksi;
using inovasyposmobile.ViewModels;
using inovasyposmobile.ViewModels.Masterdata.Produk;
using inovasyposmobile.ViewModels.Transaksi;
using inovasyposmobile.ViewModels.Transaksi.Penjualan;
using inovasyposmobile.Views.Pages.Transaksi.Penjualan;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Toolkit.Hosting;

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
				fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
			})
			.ConfigureMauiHandlers(handlers =>
			{
#if ANDROID
				handlers.AddHandler<Entry, NoBorderBottomEntryhandler>();
#endif

#if IOS || MACCATALYST
				handlers.AddHandler<Entry, NoBorderBottomEntryhandler>();
#endif
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
		builder.Services.AddSingleton<IPenjualanService, PenjualanService>();
		builder.Services.AddSingleton<PenjualanViewModel>();		
		builder.Services.AddSingleton<PenjualanDetailViewModel>();

		builder.Services.AddSingleton<IProdukService, ProdukService>();
		builder.Services.AddSingleton<PenjualanCreateViewModel>();	
		builder.Services.AddSingleton<ProdukWithStokListViewModel>();
		builder.Services.AddSingleton<PenjualanKasirViewModel>();

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
