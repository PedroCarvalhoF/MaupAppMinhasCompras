using MinhasCompras.Models;

namespace MinhasCompras.Views;

public partial class EditarProdutoPage : ContentPage
{
    public EditarProdutoPage()
    {
        InitializeComponent();
    }

    private async void mnSalvarProduto_Clicked(object sender, EventArgs e)
    {
        try
        {





            ProdutoDto produtoDto = new ProdutoDto()
            {
                Id = (BindingContext as ProdutoDto).Id,
                Descricao = txtDescricaoProduto.Text,
                Preco = Convert.ToDouble(txtPrecoUnitario.Text),
                Quantidade = Convert.ToDouble(txtQuantidadeProduto.Text)
            };



            bool confirma = await DisplayAlert("Editar", $"Deseja realizar altere��o do produto ?", "Sim", "N�o");

            if (!confirma)
                return;

            await App.Db.Update(produtoDto);
            await DisplayAlert("Sucesso", "Produto alterado com sucesso!", "Ok");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro.", ex.Message, "Ok");
        }
    }
}