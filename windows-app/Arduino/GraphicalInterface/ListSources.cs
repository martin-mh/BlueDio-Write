using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GraphicalInterface
{
    public class DigitalPinList : ObservableCollection<int>
    {
        public DigitalPinList()
        {
            for(int i = 0; i <= 100; ++i)
            {
                Add(i);
            }
        }
    }

    public class CardList : ObservableCollection<string>
    {
        public CardList()
        {
            Add("Master");
            Add("Slave");
        }
    }
}
