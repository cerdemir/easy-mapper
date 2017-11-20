using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMapper.test
{
    class Employee
    {
        [MappingOrder(0)]
        public int Id { get; set; }

        [MappingOrder(1)]
        public string Name { get; set; }

        [MappingOrder(2)]
        public string Surname { get; set; }

        [MappingOrder(5)]
        public decimal Salary { get; set; }

       
        [MappingOrder(4)]
        public bool Married { get; set; }      
      

        [ExcludeMapping]
        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }

    }
}
