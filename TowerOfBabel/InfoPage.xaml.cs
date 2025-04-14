namespace TowerOfBabel;
public partial class InfoPage : ContentPage
{
    public static readonly Uri uri = new Uri("https://github.com/GiulianoSpaghetti/solitario.maui")

    ; public InfoPage()
	{
		InitializeComponent();
        Title=App.d["Informazioni"] as string;
    }
    private async void OnInformazioni_Click(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync(uri);
    }
}