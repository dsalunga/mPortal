using System;
namespace WCMS.WebSystem.Controls
{
    public interface ISaveInFolder
    {
        int FolderId { get; set; }
        int FolderTextBoxSize { get; set; }
        int GetFolder(int objectId, int recordId);
        int ObjectId { get; set; }
        string SelectedPath { get; set; }
        void Update(string name, int objectId, int recordId);
    }
}
