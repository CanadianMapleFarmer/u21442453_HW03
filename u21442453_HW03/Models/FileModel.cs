using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21442453_HW03.Models
{
    public class FileModel
    {
        private string fileName = "";
        private string fileType = "";
        private string filePath = "";

        public FileModel(string fileName, string filePath)
        {
            this.fileName = fileName;
            this.filePath = filePath;
            this.fileType = "." + fileName.Split('.').Last();
        }

        public string FileName { get => fileName; set => fileName = value; }
        public string FileType { get => fileType; }
        public string FilePath { get => filePath; set => filePath = value; }
    }
}