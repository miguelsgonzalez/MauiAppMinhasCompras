using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class RelatorioPorCategoria : ContentPage
    {
        public RelatorioPorCategoria()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            try
            {
                var listaCategoria = await App.Db.GetAll();
                var categoriaGastos = listaCategoria
                    .GroupBy(p => p.Categoria)
                    .Select(g => new
                    {
                        Categoria = g.Key,
                        TotalGasto = g.Sum(p => p.Preco * p.Quantidade) 
                    })
                    .ToList();

                lst_categoria.ItemsSource = categoriaGastos;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}
