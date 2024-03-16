using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using FileSystemApp;

namespace AvaloniaApplication4
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Create a list of folders
                List<Folder> folders = new List<Folder>();

                // Add folders to the list...
                
                // Create the MainWindow instance with the list of folders
                desktop.MainWindow = new MainWindow(folders);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}