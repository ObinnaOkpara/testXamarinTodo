using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace testXamarin.Models
{
    public class Token
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string UserToken { get; set; }
    }
}
