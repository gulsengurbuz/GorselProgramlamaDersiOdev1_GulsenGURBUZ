namespace deneme3;

public partial class VucütKitleİndeksi : ContentPage
{
    public VucütKitleİndeksi()
    {
        InitializeComponent();

        
            entryKilo.TextChanged += (sender, e) =>
            {
                if (decimal.TryParse(entryKilo.Text, out decimal kilo))
                {
                    sliderKilo.Value = (double)kilo;
                }
            };
        
        }

    private void OnEntryKiloTextChanged(object sender, TextChangedEventArgs e)
    {
        if (decimal.TryParse(e.NewTextValue, out decimal kilo))
        {
            
            sliderKilo.Value = (double)kilo;
        }
    }
    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        
        entryKilo.Text = e.NewValue.ToString("F1"); 
    }

    private void OnEntryBoyTextChanged(object sender, TextChangedEventArgs e)
    {
        if (decimal.TryParse(e.NewTextValue, out decimal boy))
        {
            
            sliderBoy.Value = (double)boy;
        }
    }

    private void OnSliderBoyValueChanged(object sender, ValueChangedEventArgs e)
    {
        
        entryBoy.Text = e.NewValue.ToString("F2"); 
    }



    private void OnHesaplaClicked(object sender, EventArgs e)
    {
        if (double.TryParse(entryKilo.Text, out double kilo) && double.TryParse(entryBoy.Text, out double boy))
        {
           
            decimal bmi = (decimal)(kilo / (boy * boy));

            
            labelBMI.Text = $"Vücut Kitle İndeksi: {bmi:F2}";

            
            sliderBMI.Value = (double)bmi;

            
            SetSliderValue((double)bmi,sliderBMI);


            
            string durum = "";
            if (bmi <= 15)
                durum = "İleri Düzey Zayıf";
            else if (bmi <= 16.99m)
                durum = "Orta Düzey Zayıf";
            else if (bmi <= 18.49m)
                durum = "Hafif Düzey Zayıf";
            else if (bmi <= 24.9m)
                durum = "Normal Kilolu";
            else if (bmi <= 29.99m)
                durum = "Hafif Şişman/Fazla Kilolu";
            else if (bmi <= 34.99m)
                durum = "1. Derece Obez";
            else if (bmi <= 39.99m)
                durum = "2. Derece Obez";
            else
                durum = "3. Derece Obez/Morbid Obez";

            labelDurum.Text = $"Durum: {durum}";
        }
        else
        {
            DisplayAlert("Hata", "Lütfen geçerli sayısal değerler girin.", "Tamam");
        }
    }
    private void SetSliderValue(double value, Slider slider)
    {
        
        slider.Value = (double)value;
    }



}


