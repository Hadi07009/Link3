using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class OpenFileDialog
{
    public string Title { get; set; }

    public string InitialDirectory { get; set; }

    public string FileName { get; set; }

    public string Filter { get; set; }

    public int FilterIndex { get; set; }

    public bool RestoreDirectory { get; set; }
}
