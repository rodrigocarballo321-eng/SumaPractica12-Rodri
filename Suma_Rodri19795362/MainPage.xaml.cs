using System.Globalization;

namespace Suma_Rodri19795362
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editResultadoId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;

            Task.Run(async () =>
            {
                var lista = await _dbService.GetResultado();
                MainThread.BeginInvokeOnMainThread(() => listview.ItemsSource = lista);
            });
        }

        private async void sumarBtn_Clicked(object sender, EventArgs e)
        {
            // Convierte los textos a número (acepta decimales)
            if (!double.TryParse(Entryprimernumero.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double n1) ||
                !double.TryParse(Entrysegundonumero.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double n2))
            {
                await DisplayAlert("Error", "Ingresa dos números válidos.", "OK");
                return;
            }

            // Calcula la suma y la muestra en pantalla
            string suma = (n1 + n2).ToString(CultureInfo.CurrentCulture);
            labelresultado.Text = suma;

            if (_editResultadoId == 0)
            {
                // Agrega registro
                await _dbService.Create(new Resultado
                {
                    Numero1 = Entryprimernumero.Text,
                    Numero2 = Entrysegundonumero.Text,
                    Suma = suma
                });
            }
            else
            {
                // Edita registro
                await _dbService.Update(new Resultado
                {
                    Id = _editResultadoId,
                    Numero1 = Entryprimernumero.Text,
                    Numero2 = Entrysegundonumero.Text,
                    Suma = suma
                });
                _editResultadoId = 0;
            }

            Entryprimernumero.Text = string.Empty;
            Entrysegundonumero.Text = string.Empty;
            labelresultado.Text = string.Empty;

            listview.ItemsSource = await _dbService.GetResultado();
        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var resultado = (Resultado)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editResultadoId = resultado.Id;
                    Entryprimernumero.Text = resultado.Numero1;
                    Entrysegundonumero.Text = resultado.Numero2;
                    labelresultado.Text = resultado.Suma;
                    break;

                case "Delete":
                    await _dbService.Delete(resultado);
                    listview.ItemsSource = await _dbService.GetResultado();
                    break;
            }
        }
    }
}