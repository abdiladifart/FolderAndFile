using System;

namespace FileSystemApp
{
    // Derived class representing a file
    public class File : FileSystemElement
    {
        // Size of the file
        private readonly long _size;

        // Constructor
        public File(string name, string folderLocation, long size) : base(name, folderLocation)
        {
            _size = size;
        }

        // Property to determine purpose (file) and size
        public override FileSystemType Type => FileSystemType.File;
        public override long Size => _size;

        // Property indicating the location (calculated)
        public override string Location => FolderLocation == null ? "Root" : $"{FolderLocation}/{Name}";
    }
}