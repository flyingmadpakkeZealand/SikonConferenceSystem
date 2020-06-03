using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Model
{
    public class FilterBuilder
    {
        private Func<Event, bool>[] _filterFunctions;

        private Func<string, string, bool>[] _stringFunctions;

        private Func<double, double, bool>[] _numberFunctions;

        private Func<Event, Event, int>[] _orderFunctions;

        public int SelectedFirst { get; set; }

        public int SelectedSecond { get; set; }

        public object Value { get; set; }

        public int SelectedOrder { get; set; }

        public FilterBuilder() //Arg 1: retrieved from events in a list, Arg 2: provided value. 
        {
            _stringFunctions = new Func<string, string, bool>[]
            {
                (caller, param) => caller.Contains(param, StringComparison.CurrentCultureIgnoreCase),
                (caller, param) => caller.StartsWith(param, StringComparison.CurrentCultureIgnoreCase),
                (caller, param) => caller.EndsWith(param, StringComparison.CurrentCultureIgnoreCase),
                (left, right) => left == right
            };

            _numberFunctions = new Func<double, double, bool>[]
            {
                (left, right) => Math.Abs(left - right) < 0.01,
                (left, right) => left < right,
                (left, right) => left > right,
                (left, right) => left < right || Math.Abs(left - right) < 0.01,
                (left, right) => left > right || Math.Abs(left - right) < 0.01
            };

            _filterFunctions = new Func<Event, bool>[]
            {
                @event => _stringFunctions[SelectedSecond](GetHeader(@event.Abstract), (string)Value), //Using Event Name - string functions.
                @event => _numberFunctions[SelectedSecond](@event.Rating, (double)Value), //Using Event rating - number functions.
                @event => @event.Type == (Event.EventType)Value //Using Event Type - direct comparison to value.
            };

            _orderFunctions = new Func<Event, Event, int>[] //Arg 1 and 2 are retrieved from the events list.
            {
                null, //no change to order.
                (caller, other) => EvalGetHeader(caller.Abstract).CompareTo(EvalGetHeader(other.Abstract)), //Order by Event Name.
                CompareRating //Order by Event Rating.
            };
        }

        private int CompareRating(Event caller, Event other)
        {
            if (caller.Rating < other.Rating)
            {
                return 1; //May have swapped caller and other? This shows stuff in the right order.
            }
            if (caller.Rating > other.Rating)
            {
                return -1;
            }
            return 0;
        }

        private string _eventHeader;
        private string GetHeader(string eventAbstract)
        {
            string indexString = eventAbstract.Split(';', 2)[0];
            string pureAbstract = eventAbstract.Remove(0, indexString.Length + 1);
            int headerLength = Convert.ToInt32(indexString);

            string header = pureAbstract.Substring(0, headerLength);

            return header;
        }

        private string EvalGetHeader(string eventAbstract) //No difference between methods because experimental bit has been removed.
        {
            string indexString = eventAbstract.Split(';', 2)[0];
            string pureAbstract = eventAbstract.Remove(0, indexString.Length + 1);
            int headerLength = Convert.ToInt32(indexString);

            string header = pureAbstract.Substring(0, headerLength);

            return header;
        }

        public Func<Event, bool> ConstructFilterFunc()
        {
            _eventHeader = null; //Under testing.
            return _filterFunctions[SelectedFirst];
        }

        public Func<Event, Event, int> ConstructOrderFunc()
        {
            return _orderFunctions[SelectedOrder];
        }

        public Func<Event, Event, int> ConstructOrderFunc(int selectedOrder)
        {
            return _orderFunctions[selectedOrder];
        }
    }
}
