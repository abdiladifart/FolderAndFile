using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystemApp
{
    // Derived class representing a folder
    public class Folder : FileSystemElement
    {
        // List of file system elements
        private readonly List<FileSystemElement> _elements = new List<FileSystemElement>();

        // Constructor
        public Folder(string name, string folderLocation) : base(name, folderLocation)
        {
        }

        // Property to determine purpose (folder) and size
        public override FileSystemType Type => FileSystemType.Folder;
        public override long Size => _elements.Sum(e => e.Size);

        // Method to add a file system element to the folder
        public void AddElement(FileSystemElement element)
        {
            _elements.Add(element);
        }

        // Method to remove a file system element from the folder
        public void RemoveElement(FileSystemElement element)
        {
            _elements.Remove(element);
        }

        // Property indicating the location (calculated)
        public override string Location => FolderLocation == null ? "Root" : $"{FolderLocation}/{Name}";
    }
}