using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentLibrary.Models
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; internal set; }

        private string _photo;
        public string Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Group { get; set; }

        public string DateOfEntering { get; set; }

        public string Rating { get; set; }

        [JsonProperty("Series Title")]
        public string Series { get; set; }

        public string Genre { get; set; }

        public string Category { get; set; }

        public bool IsEStudent { get; set; }
    }
}
