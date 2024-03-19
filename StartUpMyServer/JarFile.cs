using System;
using System.Collections.Generic;

namespace StartUpMyServer
{
    [Serializable]
    public class JarFile
    {
        public System.Guid Id {  get; set; }
        public string Name { get; set; }
        public string JarFileName { get; set; }
        public string JarFilePath { get; set; }
        public string Folder { get; set; }

        public override string ToString()
        {
            return System.IO.Path.GetFileName(Name);
        }
    }
}
