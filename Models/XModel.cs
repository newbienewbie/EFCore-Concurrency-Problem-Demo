using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models{
    public class XHouseInventory
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public int Sku{get;set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Timestamp]
        public DateTime RowVersion { get; set; }
    }

    public class XOrder
    {
        public int Id {get;set;}
        public XHouseInventory XHouseInventory{get;set;}
        public int XHouseInventoryId{get;set;}
        public string Description{get;set;}
    }

}