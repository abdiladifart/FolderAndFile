using System;

namespace FileSystemApp
{
    // Base class representing a file system element
    public abstract class FileSystemElement
    {
        // Properties
        public string Name { get; }
        public string FolderLocation { get; }
        public abstract string Location { get; }
        public abstract FileSystemType Type { get; }
        public abstract long Size { get; }

        // Constructor
        protected FileSystemElement(string name, string folderLocation)
        {
            Name = name;
            FolderLocation = folderLocation;
        }

        // Static method for copying an object to another folder
        public static void CopyTo(FileSystemElement element, string destinationFolder)
        {
            // Implementation for copying element
            Console.WriteLine($"Copying {element.Type.ToString().ToLower()} '{element.Name}' to '{destinationFolder}'...");
        }

        // Static method for moving an object to another folder
        public static void MoveTo(FileSystemElement element, string destinationFolder)
        {
            // Implementation for moving element
            Console.WriteLine($"Moving {element.Type.ToString().ToLower()} '{element.Name}' to '{destinationFolder}'...");
        }
    }

    // Enumeration for file system type
    public enum FileSystemType
    {
        Folder,
        File
    }
}