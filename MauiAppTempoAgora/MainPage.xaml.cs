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
                if(!string.IsNullOrEmpty (txt_cidade.Text))

                {
                    Tempo? t = await DataServices.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        //dados_previsao = string dados_previsao = "";

                        dados_previsao = $"Latitude: {t?.lat} \n" +
                                         $"Longitude: {t?.lon} \n" +
                                         $"Clima: {t?.main} ({t?.description}) \n" +
                                         $"Temp Min: {t.temp_min:F1}°C \n" +
                                         $"Temp Máx: {t.temp_max:F1}°C \n"+
                                         //$"Temp Min: {t?.temp_min ?? F1}°C \n" +
                                         //$"Temp Máx: {t?.temp_max ?? F1}°C \n" +   
                                         $"Vento: {t?.speed} m/s \n" +
                                         $"Visibilidade: {t?.visibility} m \n" +
                                         $"Nascer do Sol: {t?.sunrise ?? "N/A"} \n" +
                                         $"Por do Sol: {t?.sunset ?? "N/A"}";






                        /*$"Latitude: {t?.lat} \n" +
                         $"Longitude: {t?.lon} \n" +
                         $"Nascer do Sol: {t?.sunrise ?? "N/A"} \n" +
                         $"Por do Sol: {t?.sunset ?? "N/A"} \n" +
                         $"Temp Máx: {t?.temp_max ?? 0} \n" +
                         $"Temp Min: {t?.temp_min ?? 0} \n";*/


                        lbl_res.Text = dados_previsao;
                    }

                    else
                    {
                        lbl_res.Text = "Sem dados de Previsão";
                    }
                }

                else
                {
                    lbl_res.Text = "Preencha a cidade";
                }
            }

            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }


        }
    }

}
