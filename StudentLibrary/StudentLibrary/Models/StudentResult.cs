using System.Collections.Generic;

namespace StudentLibrary.Models
{
    public class StudentResult
    {
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public VolumeInfo VolumeInfo { get; set; }

        public SaleInfo SaleInfo { get; set; }
    }
    
    public class VolumeInfo
    {
        public string Photo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Group { get; set; }

        public string DateOfEntering { get; set; }
    }

    public class SaleInfo
    {
        public bool IsEstudent { get; set; }
    }
}
