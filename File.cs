using System;

namespace FileSystemApp
{
    // Derived class representing a file
// Derived class representing a file
    public class File : FileSystemElement
    {
        // Size of the file
        public override long Size { get; }

        // Constructor
        public File(string name, string folderLocation, long size) : base(name, folderLocation)
        {
            Size = size;
        }

        // Property to determine purpose (file) and size
        public override FileSystemType Type => FileSystemType.File;

        // Property indicating the location (calculated)
        public override string Location => FolderLocation == null ? "Root" : $"{FolderLocation}/{Name}";
    }

}