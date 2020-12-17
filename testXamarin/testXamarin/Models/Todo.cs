using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace testXamarin.Models
{
    [Table("Todo")]
    public class Todo : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        private string todoTextValue = String.Empty;

        [MaxLength(500)]
        public string TodoText
        {
            get
            {
                return this.todoTextValue;
            }

            set
            {
                if (value != this.todoTextValue)
                {
                    this.todoTextValue = value;
                    NotifyPropertyChanged();
                }
            }
        }



        private string todoTimeValue = String.Empty;
        [MaxLength(10)]
        public string TodoTime
        {
            get
            {
                return this.todoTimeValue;
            }

            set
            {
                if (value != this.todoTimeValue)
                {
                    this.todoTimeValue = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private bool doneValue;
        public bool Done
        {
            get
            {
                return this.doneValue;
            }

            set
            {
                if (value != this.doneValue)
                {
                    this.doneValue = value;
                    NotifyPropertyChanged();
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
