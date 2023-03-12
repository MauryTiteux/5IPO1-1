namespace FoodSearchTutorial.View;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext= viewModel;
	}

    protected override void OnAppearing()
    {
        var nutriScore = Settings.NutriScore;

        var radioButtons = nutriscoreStackLayout.Children.Where(c => c is RadioButton);

        foreach (RadioButton radioButton in radioButtons) 
        {
            if(radioButton.Value.ToString() == nutriScore) 
            {
                radioButton.IsChecked = true;
                break;
            }
        }

        base.OnAppearing();
    }

    private void OnCheckedChanged(object sender, CheckedChangedEventArgs e) 
    {
        var nutriScore = ((RadioButton)sender).Value.ToString();

        Settings.NutriScore = nutriScore;
    }
}