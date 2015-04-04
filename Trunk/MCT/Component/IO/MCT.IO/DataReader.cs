using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCT.IO
{
    public class DataReader
    {

        public TextSeperator Seperator { get; set; }
        public DecimalCharacter Decimal { get; set; }
        public Orientation Orientation { get; set; }


        /// <summary>
        /// If FileStream exist open a FileStream
        /// </summary>
        /// <remarks></remarks>
        /// <seealso cref="File"/>
        /// <param ="fileName">Full path of the FileStream</param>       
        public FileStream Open(string fileName)
        {
            if (File.Exists(fileName))
                return File.Open(fileName, FileMode.Open, FileAccess.Read);

            else
                return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <remarks></remarks>
        /// <seealso cref=""/>
        /// <param name="path"></param>
        public static bool FileExist(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;
        }
    }
}
