using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    // Verifica se tem internet
                    var connectivity = Connectivity.Current.NetworkAccess;

                    if (connectivity != NetworkAccess.Internet)
                    {
                        await DisplayAlert("Sem conexão",
                            "Você está sem conexão com a internet. Verifique sua rede e tente novamente.",
                            "OK");
                        return;
                    }

                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = $"Latitude: {t.lat} \n" +
                                               $"Longitude: {t.lon} \n" +
                                               $"Nascer do Sol: {t.sunrise} \n" +
                                               $"Por do Sol: {t.sunset} \n" +
                                               $"Temp Máx: {t.temp_max} °C\n" +
                                               $"Temp Min: {t.temp_min} °C\n" +
                                               $"Descrição: {t.description} \n" +
                                               $"Velocidade do Vento: {t.speed} m/s\n" +
                                               $"Visibilidade: {t.visibility} metros";

                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de Previsão";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Sem conexão",
                    "Não foi possível conectar ao servidor. Verifique sua conexão com a internet.",
                    "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }

}