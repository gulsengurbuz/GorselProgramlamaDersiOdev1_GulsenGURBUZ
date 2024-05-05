namespace deneme3;

public partial class KrediHesaplama : ContentPage
{
    public KrediHesaplama()
    {

        InitializeComponent();

        pickerKrediTuru.ItemsSource = new string[]
            {
                "Ihtiyaç Kredisi",
                "Konut Kredisi",
                "Taþýt Kredisi",
                "Ticari Kredi"
            };

    }


    private void OnHesaplaClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(entryKrediTutari.Text, out decimal krediTutari)
                && decimal.TryParse(entryFaizOrani.Text, out decimal faizOrani)
                && int.TryParse(entryVade.Text, out int vade))
            {
                string krediTuru = pickerKrediTuru.SelectedItem.ToString();

                KrediHesaplamaSonucu sonuc = HesaplaKredi( krediTuru, krediTutari, faizOrani, vade);

                labelAylikTaksit.Text = $"Aylýk Taksit: {sonuc.AylikTaksit:C2}";
                labelToplamOdeme.Text = $"Toplam Ödeme: {sonuc.ToplamOdeme:C2}";
                labelToplamFaiz.Text = $"Toplam Faiz: {sonuc.ToplamFaiz:C2}";

                stackSonuc.IsVisible = true;
            }
            else
            {
                DisplayAlert("Hata", "Lütfen geçerli sayýsal deðerler girin.", "Tamam");
            }
        }

        private KrediHesaplamaSonucu HesaplaKredi(string krediTuru, decimal krediTutari, decimal faizOrani, int vade)
        {
            decimal aylikFaizOrani = (faizOrani / 100m) / 12m;
            decimal aylikTaksit = 0;
            decimal toplamOdeme = 0;
            decimal toplamFaiz = 0;

            switch (krediTuru)
            {
                case "Ihtiyaç Kredisi":
                    aylikTaksit = (krediTutari * aylikFaizOrani * (decimal)Math.Pow(1 + (double)aylikFaizOrani, vade))
                                    / ((decimal)Math.Pow(1 + (double)aylikFaizOrani, vade) - 1);
                    toplamOdeme = aylikTaksit * vade;
                    toplamFaiz = toplamOdeme - krediTutari;
                    break;
                case "Konut Kredisi":
                    aylikTaksit = (krediTutari * 0.01m * (decimal)Math.Pow(1 + 0.01, 120)) / ((decimal)Math.Pow(1 + 0.01, 120) - 1);
                    toplamOdeme = aylikTaksit * 120;
                    toplamFaiz = toplamOdeme - krediTutari;
                    break;
                case "Taþýt Kredisi":
                    aylikTaksit = (krediTutari * 0.015m * (decimal)Math.Pow(1 + 0.015, 60)) / ((decimal)Math.Pow(1 + 0.015, 60) - 1);
                    toplamOdeme = aylikTaksit * 60;
                    toplamFaiz = toplamOdeme - krediTutari;
                    break;
                case "Ticari Kredi":
                    aylikTaksit = (krediTutari * 0.02m * (decimal)Math.Pow(1 + 0.02, 36)) / ((decimal)Math.Pow(1 + 0.02, 36) - 1);
                    toplamOdeme = aylikTaksit * 36;
                    toplamFaiz = toplamOdeme - krediTutari;
                    break;
                default:
                    break;
            }

            return new KrediHesaplamaSonucu
            {
                AylikTaksit = aylikTaksit,
                ToplamOdeme = toplamOdeme,
                ToplamFaiz = toplamFaiz
            };
        }

        private void OnKrediTuruSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedKrediTuru = pickerKrediTuru.SelectedItem.ToString();

            switch (selectedKrediTuru)
            {
                case "Ihtiyaç Kredisi":
                    entryFaizOrani.Text = "1.5"; 
                    entryVade.Text = "60"; 
                    break;
                case "Konut Kredisi":
                    entryFaizOrani.Text = "1"; 
                    entryVade.Text = "120";
                    break;
                case "Taþýt Kredisi":
                    entryFaizOrani.Text = "1.5"; 
                    entryVade.Text = "60";
                    break;
                case "Ticari Kredi":
                    entryFaizOrani.Text = "2"; 
                    entryVade.Text = "36"; 
                    break;
                default:
                    break;
            }
        }
    }

    public class KrediHesaplamaSonucu
    {
        public decimal AylikTaksit { get; set; }
        public decimal ToplamOdeme { get; set; }
        public decimal ToplamFaiz { get; set; }
    }

