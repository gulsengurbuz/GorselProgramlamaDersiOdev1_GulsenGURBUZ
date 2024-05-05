namespace deneme3;

public partial class YapılacaklarListesi : ContentPage
{ 
    int count = 0;
private List<StackLayout> todoItemStacks = new List<StackLayout>();

	public YapılacaklarListesi()
	{
		InitializeComponent();
	}


        private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            bool isChecked = e.Value;
            string status = isChecked ? "Tamamlandı" : "Tamamlanmadı";
            DisplayAlert("Durum", $"Bu madde {status}.", "Tamam");
        }

        private async void OnEditTapped(object sender, EventArgs e)
        {
            Image editImage = (Image)sender;
            StackLayout parentStack = (StackLayout)editImage.Parent;

            Label label = parentStack.Children.OfType<Label>().FirstOrDefault();
            if (label != null)
            {
                string editedText = await DisplayPromptAsync("Düzenle", "Yazıyı düzenleyin", "Tamam", "İptal", null, -1, Keyboard.Default, label.Text);
                if (!string.IsNullOrEmpty(editedText))
                {
                    label.Text = editedText;
                }
            }
        }

        private async void OnDeleteTapped(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Sil", "Bu maddeyi silmek istediğinizden emin misiniz?", "Evet", "Hayır");
            if (answer)
            {
                Image deleteImage = (Image)sender;
                StackLayout parentStack = (StackLayout)deleteImage.Parent;

                todoItemLayout.Children.Remove(parentStack);
                todoItemStacks.Remove(parentStack);
            }
        }

        private async void OnAddButtonTapped(object sender, EventArgs e)
        {
            string newLabelText = await DisplayPromptAsync("Yeni Madde Ekle", "Yapılacak maddeyi girin:", "Ekle", "İptal", null, -1, Keyboard.Default, "");

            if (!string.IsNullOrEmpty(newLabelText))
            {
                StackLayout newTodoStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Margin = new Thickness(0, 10)
                };

                CheckBox newCheckBox = new CheckBox
                {
                    VerticalOptions = LayoutOptions.Center
                };
                newCheckBox.CheckedChanged += OnCheckBoxCheckedChanged;
                newTodoStack.Children.Add(newCheckBox);

                Label newLabel = new Label
                {
                    Text = newLabelText,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                newTodoStack.Children.Add(newLabel);

                Image editImage = new Image
                {
                    Source = "ekle.png",
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(10, 0, 0, 0),
                    HeightRequest = 24,
                    WidthRequest = 24
                };
                editImage.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(async () =>
                    {
                        Image tappedImage = editImage;
                        StackLayout editStack = (StackLayout)tappedImage.Parent;

                        Label editLabel = editStack.Children.OfType<Label>().FirstOrDefault();
                        if (editLabel != null)
                        {
                            string editedText = await DisplayPromptAsync("Düzenle", "Yazıyı düzenleyin", "Tamam", "İptal", null, -1, Keyboard.Default, editLabel.Text);
                            if (!string.IsNullOrEmpty(editedText))
                            {
                                editLabel.Text = editedText;
                            }
                        }
                    })
                });
                newTodoStack.Children.Add(editImage);

                Image deleteImage = new Image
                {
                    Source = "del.png",
                    VerticalOptions = LayoutOptions.Center,
                    Margin = new Thickness(10, 0, 0, 0),
                    HeightRequest = 24,
                    WidthRequest = 24
                };
                deleteImage.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(async () =>
                    {
                        bool answer = await DisplayAlert("Sil", "Bu maddeyi silmek istediğinizden emin misiniz?", "Evet", "Hayır");
                        if (answer)
                        {
                            StackLayout deleteStack = (StackLayout)deleteImage.Parent;

                            todoItemLayout.Children.Remove(deleteStack);
                            todoItemStacks.Remove(deleteStack);
                        }
                    })
                });
                newTodoStack.Children.Add(deleteImage);

                todoItemLayout.Children.Add(newTodoStack);
                todoItemStacks.Add(newTodoStack);
            }
        }

        private async void OnSaveTapped(object sender, EventArgs e)
        {
            
            await DisplayAlert("Başarılı", "Değişiklikler kaydedildi.", "Tamam");
        }

    }
