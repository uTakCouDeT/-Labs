using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using SQLite;

namespace App1.Models
{
    public class Company
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string WayOfDelevery { get; set; }
        public float CostOfDelevery { get; set; }


    }
}
