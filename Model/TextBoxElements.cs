using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedElementByID
{
    public class TextBoxElements : NotifyPropertyClass
    {
        private string linkedElementID;

        public string LinkedElementID
        {
            get { return linkedElementID; }
            set
            {
                linkedElementID = value; 
                OnPropertyChanged();
            }
        }
        private string elementIDText;

        public string ElementIDText
        {
            get { return elementIDText; }
            set
            {
                elementIDText = value; 
                OnPropertyChanged();
            }
        }
    }
}
