using MinhasCompras.Models;

namespace MinhasCompras.Views;
public partial class NovoProdutoPage : ContentPage
{
    public NovoProdutoPage()
    {
        InitializeComponent();
    }

    private async void mnSalvarProduto_Clicked(object sender, EventArgs e)
    {
        try
        {

            ProdutoDto produtoDto = new ProdutoDto()
            {
                Descricao = txtDescricaoProduto.Text,
                Preco = Convert.ToDouble(txtPrecoUnitario.Text),
                Quantidade = Convert.ToDouble(txtQuantidadeProduto.Text)
            };

            await App.Db.Insert(produtoDto);
            await DisplayAlert("Sucesso", "Produto cadastrado com sucesso!", "Ok");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro.", ex.Message, "Ok");
        }
    }

    private async void txtPrecoUnitario_Completed(object sender, EventArgs e)
    {
        try
        {
            var preco = txtPrecoUnitario.Text;                     
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}