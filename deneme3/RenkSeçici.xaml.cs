namespace deneme3;

public partial class RenkSeçici : ContentPage
{
	public RenkSeçici()
	{
		InitializeComponent();
	}
	
   

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateColor();
        }

        private void UpdateColor()
        {
            int red = (int)redSlider.Value;
            int green = (int)greenSlider.Value;
            int blue = (int)blueSlider.Value;

            
            Color color = Color.FromRgb(red, green, blue);

            
            colorCodeLabel.Text = $"RGB: #{red:X2}{green:X2}{blue:X2}";

            
            this.BackgroundColor = color;
        }

        private void OnRandomColorClicked(object sender, EventArgs e)
        {
            Random random = new Random();

            
            redSlider.Value = random.Next(0, 256);
            greenSlider.Value = random.Next(0, 256);
            blueSlider.Value = random.Next(0, 256);

            
            UpdateColor();
    }


    private void OnColorCodeLabelTapped(object sender, EventArgs e)
    {
        
        string colorCode = colorCodeLabel.Text;
      //  Xamarin.Essentials.Clipboard.SetTextAsync(colorCode);

        
        DisplayAlert("Kopyalandı", "RGB", "OK");
    }



}


