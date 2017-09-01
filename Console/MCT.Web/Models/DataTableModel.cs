using System;

namespace MCT.Web.Models
{
    public class DataTableRecieverModel
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string search { get; set; }
        public object columns { get; set; }

        //public int iDisplayStart { get; set; }
        //public int iDisplayLength { get; set; }
        //public int iColumns { get; set; }
        //public string sSearch { get; set; }
        //public bool iSortable { get; set; }
        //public int iSortingCols { get; set; }

        //public int iSortCol_0 { get; set; }
        //public int iSortCol_1 { get; set; }
        //public int iSortCol_2 { get; set; }

        //public int iSortDir_0 { get; set; }
        //public int iSortDir_1 { get; set; }
        //public int iSortDir_2 { get; set; }
    }

    public class DataTableSendModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public Object data { get; set; }
        public string error { get; set; }

        public DataTableSendModel()
        {
            draw = 0;
            recordsTotal = 0;
            recordsFiltered = 0;
            data = new Object();
            error = "";
        }
    }
}