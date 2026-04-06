using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MauiAppTempoAgora.Services
{
    public class DataServices
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo t = null;

            string chave = "6135072afe7f6cec1537d5cb08a5a1a2";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={Uri.EscapeDataString(cidade)}&units=metric&appid={chave}&lang=pt_br";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage resp = await client.GetAsync(url);

                    // ❌ Cidade não encontrada
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Cidade não encontrada. Verifique o nome digitado.");
                    }

                    if (resp.IsSuccessStatusCode)
                    {
                        string json = await resp.Content.ReadAsStringAsync();
                        var rascunho = JObject.Parse(json);

                        DateTime sunrise = DateTimeOffset
                            .FromUnixTimeSeconds((long)rascunho["sys"]["sunrise"])
                            .ToLocalTime()
                            .DateTime;

                        DateTime sunset = DateTimeOffset
                            .FromUnixTimeSeconds((long)rascunho["sys"]["sunset"])
                            .ToLocalTime()
                            .DateTime;

                        t = new()
                        {
                            lat = (double)rascunho["coord"]["lat"],
                            lon = (double)rascunho["coord"]["lon"],
                            description = (string)rascunho["weather"][0]["description"],
                            main = (string)rascunho["weather"][0]["main"],
                            temp_min = (double)rascunho["main"]["temp_min"],
                            temp_max = (double)rascunho["main"]["temp_max"],
                            speed = (double)rascunho["wind"]["speed"],
                            visibility = (int?)(rascunho["visibility"]) ?? 0,

                            sunrise = sunrise.ToString("dd/MM/yyyy HH:mm:ss"),
                            sunset = sunset.ToString("dd/MM/yyyy HH:mm:ss")
                        };
                    }
                }
            }
            catch (HttpRequestException)
            {
                throw new Exception("Sem conexão com a internet.");
            }

            return t;
        }
    }
}