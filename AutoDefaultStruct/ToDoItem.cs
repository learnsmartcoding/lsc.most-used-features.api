using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSC.AutoDefaultStruct
{
    internal struct ToDoItem
    {
        public string Description { get; set; }
        public bool Done { get; set; }
        public DateTime CompletedDate { get; set; }

        public override string ToString()
        {
            return $"Todo item: {Description} - Done: {Done} - DateTime: {CompletedDate}";
        }
    }
}
