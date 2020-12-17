using System;
using System.Collections.Generic;
using System.Text;

namespace testXamarin.ViewModels
{
    public class AddTodoViewModel
    {
        public int Id { get; set; }
        public string TodoDescription { get; set; }
        public string TodoTime { get; set; }
        public bool IsDone { get; set; }
    }
}
