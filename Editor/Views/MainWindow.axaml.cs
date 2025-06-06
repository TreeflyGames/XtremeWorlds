using System;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Editor.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            int i = 0;
            while (true)
            {
                i++;
                if (CanLoadBitmap("avares://Editor/Content/Graphics/Tilesets/" + i + ".png"))
                {
                    tilesetComboBox.Items.Add(i);
                }
                else
                {
                    break;
                }
            }

            if (tilesetComboBox.Items.Count > 0)
            {
                tilesetComboBox.SelectedIndex = 0;
            }
        }

        public bool CanLoadBitmap(string uri)
        {
            try
            {
                using var stream = AssetLoader.Open(new Uri(uri));
                using var bitmap = new Bitmap(stream);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}