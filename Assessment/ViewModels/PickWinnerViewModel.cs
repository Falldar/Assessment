using Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.ViewModels
{
    public class PickWinnerViewModel
    {
        public List<Attendee> Winners { get; set; }
        public PickWinnerViewModel()
        {
            Winners = new List<Attendee>();
        }
    }
}
