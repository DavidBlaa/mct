using System.Collections.Generic;

namespace MCT.Web.Models.Tree
{
    public class TreeElement
    {
        public long id { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public List<TreeElement> children { get; set; }

        public TreeElement()
        {
            id = 0;
            name = "";
            size = 100;
            children = new List<TreeElement>();
        }


    }
}