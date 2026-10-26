using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAUIFoodie.Models
{
    public class Food
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
