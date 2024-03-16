using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystemApp
{
    public partial class MainWindow : Window
    {
        private TextBlock _messageTextBlock;
        private TextBox _fileNameTextBox;
        private TextBox _folderNameTextBox;
        private List<Folder> _folders; // Collection of folders

        public MainWindow(List<Folder> folders) // Constructor with folders parameter
        {
            _folders = folders; // Set the _folders collection
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _messageTextBlock = this.Find<TextBlock>("MessageTextBlock");
            _fileNameTextBox = this.FindControl<TextBox>("FileNameTextBox");
            _folderNameTextBox = this.FindControl<TextBox>("FolderNameTextBox");
        }

        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            if (_fileNameTextBox != null && _folderNameTextBox != null)
            {
                string fileName = _fileNameTextBox.Text;
                string folderName = _folderNameTextBox.Text;

                if (!string.IsNullOrWhiteSpace(fileName) && !string.IsNullOrWhiteSpace(folderName))
                {
                    // Check if the folder exists
                    Folder folder = GetFolder(folderName);
                    if (folder != null)
                    {
                        // Create the file in the selected folder
                        File file = new File(fileName, folder.Location, 1024); // Assuming file size is not needed for creation
                        folder.AddElement(file);

                        _messageTextBlock.Text += $"File '{fileName}' created in folder '{folderName}'.{Environment.NewLine}";
                    }
                    else
                    {
                        _messageTextBlock.Text += $"Folder '{folderName}' not found.{Environment.NewLine}";
                    }
                }
                else
                {
                    _messageTextBlock.Text += "Please enter both file name and folder name to create a file.{Environment.NewLine}";
                }
            }
            else
            {
                _messageTextBlock.Text += "FileNameTextBox or FolderNameTextBox control is null.{Environment.NewLine}";
            }
        }


        private void CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            if (_folderNameTextBox != null && !string.IsNullOrEmpty(_folderNameTextBox.Text))
            {
                string folderName = _folderNameTextBox.Text;
        
                // Create the folder
                Folder newFolder = new Folder(folderName, null); // Assuming folder location is not needed for creation
        
                // Add the folder to the collection
                _folders.Add(newFolder);
        
                _messageTextBlock.Text += $"Folder '{folderName}' created.{Environment.NewLine}";
            }
            else
            {
                _messageTextBlock.Text += "Please enter a valid folder name.{Environment.NewLine}";
            }
        }


        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_fileNameTextBox.Text) && !string.IsNullOrEmpty(_folderNameTextBox.Text))
            {
                string fileName = _fileNameTextBox.Text;
                string folderName = _folderNameTextBox.Text;

                // Find the folder where the file exists
                Folder sourceFolder = GetFolderContainingFile(fileName);
                if (sourceFolder != null)
                {
                    // Find the destination folder
                    Folder destinationFolder = GetFolder(folderName);
                    if (destinationFolder != null)
                    {
                        // Find the file to copy
                        File fileToCopy = sourceFolder.ListFiles().FirstOrDefault(file => file.Name == fileName);
                        if (fileToCopy != null)
                        {
                            // Create a new File object for the copied file
                            File copiedFile = new File(fileName, destinationFolder.Location, fileToCopy.Size);

                            // Use the AddElement method to add the copied file to the destination folder
                            destinationFolder.AddElement(copiedFile);

                            _messageTextBlock.Text += $"Copying file '{fileName}' to folder '{folderName}'...{Environment.NewLine}";
                        }
                        else
                        {
                            _messageTextBlock.Text += $"File '{fileName}' not found in folder '{sourceFolder.Name}'.{Environment.NewLine}";
                        }
                    }
                    else
                    {
                        _messageTextBlock.Text += $"Destination folder '{folderName}' not found.{Environment.NewLine}";
                    }
                }
                else
                {
                    _messageTextBlock.Text += $"File '{fileName}' not found in any folder.{Environment.NewLine}";
                }
            }
            else
            {
                _messageTextBlock.Text += "Please enter a valid file name and folder name to copy.{Environment.NewLine}";
            }
        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_fileNameTextBox.Text) && !string.IsNullOrEmpty(_folderNameTextBox.Text))
            {
                string fileName = _fileNameTextBox.Text;
                string folderName = _folderNameTextBox.Text;

                // Find the folder where the file exists
                Folder sourceFolder = GetFolderContainingFile(fileName);
                if (sourceFolder != null)
                {
                    // Find the destination folder
                    Folder destinationFolder = GetFolder(folderName);
                    if (destinationFolder != null)
                    {
                        // Find the file to move
                        File fileToMove = sourceFolder.ListFiles().FirstOrDefault(file => file.Name == fileName);
                        if (fileToMove != null)
                        {
                            // Move the file to the destination folder
                            sourceFolder.MoveFileToFolder(fileToMove, destinationFolder);

                            _messageTextBlock.Text += $"Moving file '{fileName}' to folder '{folderName}'...{Environment.NewLine}";
                        }
                        else
                        {
                            _messageTextBlock.Text += $"File '{fileName}' not found in folder '{sourceFolder.Name}'.{Environment.NewLine}";
                        }
                    }
                    else
                    {
                        _messageTextBlock.Text += $"Destination folder '{folderName}' not found.{Environment.NewLine}";
                    }
                }
                else
                {
                    _messageTextBlock.Text += $"File '{fileName}' not found in any folder.{Environment.NewLine}";
                }
            }
            else
            {
                _messageTextBlock.Text +=
                    "Please enter a valid file name and folder name to move.{Environment.NewLine}";
            }
        }

        private Folder GetFolderContainingFile(string fileName)
        {
            foreach (var folder in _folders)
            {
                if (folder.ListFiles().Any(file => file.Name == fileName))
                {
                    return folder;
                }
            }
            return null;
        }

        private void ListFilesAndFolders_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_folderNameTextBox.Text))
            {
                string folderName = _folderNameTextBox.Text;
                Folder folder = GetFolder(folderName);
                if (folder != null)
                {
                    _messageTextBlock.Text += $"Files and folders in folder '{folderName}':{Environment.NewLine}";
                    foreach (var file in folder.ListFiles())
                    {
                        _messageTextBlock.Text += $"{file.Name} - File | Location: {file.Location}{Environment.NewLine}";
                    }
                    foreach (var subfolder in folder.ListSubFolders())
                    {
                        _messageTextBlock.Text += $"{subfolder.Name} - Folder | Location: {subfolder.Location}{Environment.NewLine}";
                    }
                    _messageTextBlock.Text += $"Total size: {folder.Size} bytes{Environment.NewLine}";
                }
                else
                {
                    _messageTextBlock.Text += $"Folder '{folderName}' not found.{Environment.NewLine}";
                }
            }
            else
            {
                _messageTextBlock.Text += "Please enter a folder name to list its files and folders.{Environment.NewLine}";
            }
        }

        private Folder GetFolder(string folderName)
        {
            foreach (var folder in _folders)
            {
                if (folder.Name == folderName)
                {
                    return folder;
                }
            }
            return null;
        }
    }
}
