using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FlightControl.Models
{
    public class ControlSet
    {
        public string Name { get; set; }

        public ObservableCollection<ControlPage> ControlPages { get; set; }

        public ControlSet()
        {
            Name = "New Control Set";
            ControlPages = new ObservableCollection<ControlPage>();
        }

        public bool MovePage(Guid id, int newPosition)
        {
            var currentIndex = ControlPages.IndexOf(ControlPages.FirstOrDefault(x => x.Id == id));

            if (currentIndex != 0)
            {
                ControlPages.Move(currentIndex, newPosition);
            }

            return false;
        }
    }
}
