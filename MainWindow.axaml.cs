using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace FileSystemApp
{
    public partial class MainWindow : Window
    {
        private string _selectedFile;
        private string _selectedFolder;
        private string _destinationFolder;

        private TextBox _messageTextBox;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            _messageTextBox = this.Find<TextBox>("MessageTextBox");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void CopyFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.AllowMultiple = false;
            _selectedFile = (await dialog.ShowAsync(this)).ToString();

            if (_selectedFile != null)
            {
                // Here you can implement logic to select a destination folder
                // For example, you can use OpenFolderDialog to select a destination folder
                OpenFolderDialog folderDialog = new OpenFolderDialog();
                _destinationFolder = await folderDialog.ShowAsync(this);

                if (_destinationFolder != null)
                {
                    File file = new File(System.IO.Path.GetFileName(_selectedFile), null, 1024); // Placeholder size
                    _messageTextBox.Text += $"Copying file '{System.IO.Path.GetFileName(_selectedFile)}' to '{_destinationFolder}'...{Environment.NewLine}";
                    FileSystemElement.CopyTo(file, _destinationFolder);
                }
            }
        }

        private async void MoveFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.AllowMultiple = false;
            _selectedFile = (await dialog.ShowAsync(this)).ToString();

            if (_selectedFile != null)
            {
                // Here you can implement logic to select a destination folder
                // For example, you can use OpenFolderDialog to select a destination folder
                OpenFolderDialog folderDialog = new OpenFolderDialog();
                _destinationFolder = await folderDialog.ShowAsync(this);

                if (_destinationFolder != null)
                {
                    File file = new File(System.IO.Path.GetFileName(_selectedFile), null, 1024); // Placeholder size
                    _messageTextBox.Text += $"Moving file '{System.IO.Path.GetFileName(_selectedFile)}' to '{_destinationFolder}'...{Environment.NewLine}";
                    FileSystemElement.MoveTo(file, _destinationFolder);
                }
            }
        }

        private async void CopyFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog folderDialog = new OpenFolderDialog();
            _selectedFolder = await folderDialog.ShowAsync(this);

            if (_selectedFolder != null)
            {
                // Here you can implement logic to select a destination folder
                // For example, you can use OpenFolderDialog to select a destination folder
                OpenFolderDialog destinationDialog = new OpenFolderDialog();
                _destinationFolder = await destinationDialog.ShowAsync(this);

                if (_destinationFolder != null)
                {
                    Folder folder = new Folder(System.IO.Path.GetFileName(_selectedFolder), null); // Placeholder for folder location
                    _messageTextBox.Text += $"Copying folder '{System.IO.Path.GetFileName(_selectedFolder)}' to '{_destinationFolder}'...{Environment.NewLine}";
                    FileSystemElement.CopyTo(folder, _destinationFolder);
                }
            }
        }

        private async void MoveFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog folderDialog = new OpenFolderDialog();
            _selectedFolder = await folderDialog.ShowAsync(this);

            if (_selectedFolder != null)
            {
                // Here you can implement logic to select a destination folder
                // For example, you can use OpenFolderDialog to select a destination folder
                OpenFolderDialog destinationDialog = new OpenFolderDialog();
                _destinationFolder = await destinationDialog.ShowAsync(this);

                if (_destinationFolder != null)
                {
                    Folder folder = new Folder(System.IO.Path.GetFileName(_selectedFolder), null); // Placeholder for folder location
                    _messageTextBox.Text += $"Moving folder '{System.IO.Path.GetFileName(_selectedFolder)}' to '{_destinationFolder}'...{Environment.NewLine}";
                    FileSystemElement.MoveTo(folder, _destinationFolder);
                }
            }
        }
    }
}
