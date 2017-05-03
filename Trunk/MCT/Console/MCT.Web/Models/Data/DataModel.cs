using System.Data;

namespace MCT.Web.Models.Data
{
    public class DataModel
    {
        public DataTable Species;
        public DataTable Plants;
        public DataTable Animals;


        public DataModel()
        {
            Species = new DataTable();
            Plants = new DataTable();
            Animals = new DataTable();
        }
    }
}