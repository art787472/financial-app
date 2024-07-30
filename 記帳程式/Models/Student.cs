using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳程式.Models
{
    internal class Student:IDisposable
    {
        public String Name { get; set; }    
        public int Number { get; set; } 
        public Student(int number,string name) { 
        
            this.Number = number;
            this.Name = name;
        }    

        public void ShowInfo()
        {

        }

        public void Dispose()
        {
           // reader.close()
        }
    }
}
