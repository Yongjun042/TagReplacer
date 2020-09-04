using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TagLib;
using TagReplacer.Tools;

namespace TagReplacer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class MusicInfo
        {
            public string Title { get; set; }
            public string FilePath { get; set; }
            public string[] AlbumArtist { get; set; }
            public string[] Artists { get; set; }
            public string[] Composers { get; set; }
        }
        private ObservableCollection<MusicInfo> musicList;


        public MainWindow()
        {
            InitializeComponent();
            musicList = new ObservableCollection<MusicInfo>();
            musicsListView.ItemsSource = musicList;

        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Audio (*.mp3,*.wav,*flac...)|*.mp3;*.wav;*.flac|All Files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                // Read the files
                foreach (String file in openFileDialog.FileNames)
                {
                    //
                    try
                    {
                        var tfile = TagLib.File.Create(file);
                        musicList.Add(item: new MusicInfo() { FilePath = file, AlbumArtist = tfile.Tag.AlbumArtists, Title = tfile.Tag.Title, Artists = tfile.Tag.Performers, Composers = tfile.Tag.Composers});

                    }
                    catch (SecurityException ex)
                    {
                        // The user lacks appropriate permissions to read files, discover paths, etc.
                        MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                            "Error message: " + ex.Message + "\n\n" +
                            "Details (send to Support):\n\n" + ex.StackTrace
                        );
                    }
                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        MessageBox.Show("Cannot load the music: " + file.Substring(file.LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);
                    }
                }
            }
        }

        private void BtnSplit_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (var item in musicsListView.SelectedItems)
            {

            }
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
