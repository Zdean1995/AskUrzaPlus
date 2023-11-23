using AskUrzaPlus.ViewModels;

namespace AskUrzaPlus.Views;

public partial class AskUrzaPage : ContentPage
{
	public AskUrzaPage(AskUrzaViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}