using MinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MinhasCompras.Views;

public partial class ListaProdutoPage : ContentPage
{
    ObservableCollection<ProdutoDto> lista_produtosObservable = new ObservableCollection<ProdutoDto>();
    public ListaProdutoPage()
    {
        InitializeComponent();

        ListViewProdutos.ItemsSource = lista_produtosObservable;
    }

    protected override async void OnAppearing()
    {
       //await App.Db.DeleteFrom();
        await ConsultarProdutos();
    }

    private async Task ConsultarProdutos()
    {
        try
        {
            var produtos = await App.Db.GetAll();

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (this.Handler == null)
                    return;

                lista_produtosObservable.Clear();
                foreach (var prod in produtos)
                    lista_produtosObservable.Add(prod);
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "Ok");
        }
        finally
        {
           
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new NovoProdutoPage());
        }
        catch (Exception ex)
        {

            DisplayAlert("Erro", ex.Message, "Ok");
        }
    }

    private async void txtConsultaProduto_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            var produtos = await App.Db.Search(q) ?? new List<ProdutoDto>();

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (this.Handler == null)
                    return;

                lista_produtosObservable.Clear();
                foreach (var prod in produtos)
                    lista_produtosObservable.Add(prod);
            });

        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "Ok");
        }
    }


    private void mnSomar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var soma = lista_produtosObservable.Sum(i => i.Total);

            string msg = $"A soma dos produtos é {soma:C}";

            DisplayAlert("Total", msg, "Ok");
        }
        catch (Exception ex)
        {

            DisplayAlert("Erro", ex.Message, "Ok");
        }
    }

    private async void mnRemoverItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = (MenuItem)sender;

            ProdutoDto p = selecionado.BindingContext as ProdutoDto ?? throw new ArgumentException("Nenhum menu selecionado");

            bool confirma = await DisplayAlert("Excluir", $"Tem certeza que deseja excluir o produto {p.Descricao.ToUpper()}", "Sim", "Não");

            if (confirma)
            {
                await App.Db.Delete(p);
                lista_produtosObservable.Remove(p);
            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Erro", ex.Message, "Ok");
        }
    }

    private async void ListViewProdutos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            ProdutoDto p = e.SelectedItem as ProdutoDto;

            await Navigation.PushAsync(new EditarProdutoPage
            {
                BindingContext = p
            });
        }
        catch (Exception ex)
        {

            await DisplayAlert("Erro", ex.Message, "Ok");
        }
    }

    private async void ListViewProdutos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            await ConsultarProdutos();
        }
        catch (Exception ex)
        {

            await DisplayAlert("Erro", ex.Message, "Ok");
        }
        finally
        {
            
        }
    }

    private async void ListViewProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var produtoSelecionado = e.CurrentSelection.FirstOrDefault() as ProdutoDto;
            if (produtoSelecionado == null)
                return;

            await Navigation.PushAsync(new EditarProdutoPage
            {
                BindingContext = produtoSelecionado
            });

            ((CollectionView)sender).SelectedItem = null;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "Ok");
        }
    }

}