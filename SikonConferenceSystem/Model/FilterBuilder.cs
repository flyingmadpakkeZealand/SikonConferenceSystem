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
        private Func<Event, bool>[] _firstFuncs;

        private Func<string, string, bool>[] _stringFunctions;

        private Func<double, double, bool>[] _numberFunctions;

        public int SelectedFirst { get; set; }

        public int SelectedSecond { get; set; }

        public object Value { get; set; }

        public FilterBuilder()
        {
            _stringFunctions = new Func<string, string, bool>[]
            {
                (caller, param) => caller.Contains(param),
                (caller, param) => caller.StartsWith(param),
                (caller, param) => caller.EndsWith(param)
            };

            _numberFunctions = new Func<double, double, bool>[]
            {
                (left, right) => Math.Abs(left - right) < 0.01,
                (left, right) => left < right,
                (left, right) => left > right,
                (left, right) => left < right || Math.Abs(left - right) < 0.01,
                (left, right) => left > right || Math.Abs(left - right) < 0.01
            };

            _firstFuncs = new Func<Event, bool>[]
            {
                @event => _stringFunctions[SelectedSecond](GetHeader(@event.Abstract), (string)Value),
                @event => _numberFunctions[SelectedSecond](@event.Rating, (double)Value),
                @event => @event.Type == (Event.EventType)Value
            };
        }

        private string GetHeader(string eventAbstract)
        {
            string indexString = eventAbstract.Split(';', 2)[0];
            string pureAbstract = eventAbstract.Remove(0, indexString.Length + 1);
            int headerLength = Convert.ToInt32(indexString);

            return pureAbstract.Substring(0, headerLength);
        }

        public Func<Event, bool> ConstructFilterFunc()
        {
            return _firstFuncs[SelectedFirst];
        }
    }
}
