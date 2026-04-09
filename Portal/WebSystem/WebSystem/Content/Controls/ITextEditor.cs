using System;
namespace WCMS.WebSystem.Controls
{
    public interface ITextEditor
    {
        string EditorToolbarSet { get; set; }
        string Height { get; set; }
        bool IsPlainTextDefault { get; set; }
        string Text { get; set; }
    }
}
