using System;
using System.Collections.Generic;
using System.Linq;

namespace FileSystemApp
{
    // Derived class representing a folder
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
    
    public override long Size => CalculateTotalSize();
    
    // Method to recursively calculate the total size of all elements within the folder
    private long CalculateTotalSize()
    {
        long totalSize = 0;

        foreach (var element in _elements)
        {
            totalSize += element.Size;

            // If the element is a folder, recursively calculate its size
            if (element.Type == FileSystemType.Folder)
            {
                totalSize += ((Folder)element).CalculateTotalSize();
            }
        }

        return totalSize;
    }

    // Method to add a file system element to the folder
    public void AddElement(FileSystemElement element)
    {
        _elements.Add(element);
        element.FolderLocation = this.Location; // Update the folder location of the added element
    }

    // Method to remove a file system element from the folder
    public void RemoveElement(FileSystemElement element)
    {
        _elements.Remove(element);
        element.FolderLocation = null; // Reset the folder location of the removed element
    }

    // Property indicating the location (calculated)
    public override string Location => FolderLocation == null ? "Root" : $"{FolderLocation}/{Name}";

    // Method to list all files in the folder
    public List<File> ListFiles()
    {
        return _elements.Where(e => e.Type == FileSystemType.File).Cast<File>().ToList();
    }

    // Method to move a file from this folder to another folder
    public void MoveFileToFolder(File file, Folder destinationFolder)
    {
        if (_elements.Contains(file))
        {
            destinationFolder.AddElement(file); // Add the file to the destination folder
            _elements.Remove(file); // Remove the file from this folder
            Console.WriteLine($"Moving file '{file.Name}' from folder '{Name}' to folder '{destinationFolder.Name}'...");
        }
        else
        {
            Console.WriteLine($"File '{file.Name}' does not exist in folder '{Name}'.");
        }
    }
    
    // Method to list all subfolders in the folder
    public List<Folder> ListSubFolders()
    {
        return _elements.Where(e => e.Type == FileSystemType.Folder).Cast<Folder>().ToList();
    }
}

}
