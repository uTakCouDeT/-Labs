using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using SQLite;

namespace App1.Models
{
    public class Good
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public float Cost { get; set; }
        [Indexed]
        public string NameOfRecipient { get; set; }
        [Indexed]
        public string NameOfCompany { get; set; }
    }
}
