//using Android.Text.Format;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
//using ThreadNetwork;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
	int count = 0;
    private HttpContent requestContent;

    public MainPage()
	{
		InitializeComponent();
	}
	/*
	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

    private void OnCounterClicked1(object sender, EventArgs e)
    {

		var nowdate = DateTime.Now;

        string toString = nowdate.ToString();
        CounterBtn1.Text = toString;

        SemanticScreenReader.Announce(CounterBtn1.Text);
    }

	*/
    private async void OnCallApi(object sender, EventArgs e)
    {

        DisplayResuts.Text = "Fetching data .....";

        SemanticScreenReader.Announce(DisplayResuts.Text);

        using (var httpClient = new HttpClient())
        {
            //using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.openai.com/v1/completions"))
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://myopenaidev.openai.azure.com/openai/deployments/openaimobileapp/completions?api-version=2022-12-01"))
            {
                //request.Headers.TryAddWithoutValidation("Authorization", "Bearer xx-xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                request.Headers.TryAddWithoutValidation("api-key", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

                String mycontent1 = "{\"model\": \"text-davinci-003\",\"prompt\":";
                String mycontent2 = "\""+EnterQuestion.Text+"\",";
                String mycontent3 = "\"temperature\": 0.7,\"max_tokens\": 256, \"top_p\": 1, \"frequency_penalty\": 0, \"presence_penalty\": 0}";
                String mycontent = mycontent1+ mycontent2+ mycontent3;
                request.Content = new StringContent(mycontent);
                //DisplayInput.Text = mycontent;
                //request.Content = new StringContent("{\n  \"model\": \"text-davinci-003\",\n  \"prompt\":\"where is ice cream shop\",\n \"temperature\": 0.7,\n  \"max_tokens\": 256,\n  \"top_p\": 1,\n  \"frequency_penalty\": 0,\n  \"presence_penalty\": 0\n}");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var response =  await httpClient.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                // Parse the response with a JSON parser
                //dynamic jsonResponse = JObject.Parse(responseBody);

                // Access the "choices" property
                //string choicesText = jsonResponse.choices;
                //DisplayResuts.Text = choicesText.ToString();
                //DisplayResuts.Text = responseBody;
                // Deserialize the response string into an object
                dynamic responseObject = JsonConvert.DeserializeObject(responseBody);

                // Get the text attribute from the choices array
                string text = responseObject.choices[0].text;
                DisplayResuts.Text = text;
            }
        }

        EnterQuestion.Text = "";
        
    }



}

