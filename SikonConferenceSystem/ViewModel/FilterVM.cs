using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Annotations;
using SikonConferenceSystem.Model;

namespace SikonConferenceSystem.ViewModel
{
    public class FilterVM : INotifyPropertyChanged
    {
        private FilterBuilder _filterBuilder;
        public FilterBuilder FilterBuilder
        {
            get { return _filterBuilder; }
        }

        private int _selectedFilter;
        public int SelectedFilter
        {
            get { return _selectedFilter; }
            set
            {
                _selectedFilter = value;
                if (value>=2)
                {
                    Conditions = _typeConditions;
                }
                else if (value>=1)
                {
                    Conditions = _numberConditions;
                }
                else
                {
                    Conditions = _stringConditions;
                }

                SelectedCondition = 0;
                _filterBuilder.SelectedFirst = value;
            }
        }

        private List<string> _conditions;
        public List<string> Conditions
        {
            get { return _conditions; }
            set
            {
                _conditions = value;
                OnPropertyChanged();
            }
        }

        private int _selectedCondition;
        public int SelectedCondition
        {
            get { return _selectedCondition; }
            set
            {
                _selectedCondition = value;
                OnPropertyChanged();
                _filterBuilder.SelectedSecond = value;
            }
        }

        private object _value;
        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (SelectedFilter>=2)
                {
                    _filterBuilder.Value = (Event.EventType) value;
                }
                else if(SelectedFilter>=1)
                {
                    _filterBuilder.Value = Convert.ToDouble(value);
                }
                else
                {
                    _filterBuilder.Value = value;
                }
            }
        }

        private List<string> _FilterOptions;
        public List<string> FilterOptions
        {
            get { return _FilterOptions; }
        }

        private List<string> _stringConditions;

        private List<string> _numberConditions;

        private List<string> _typeConditions;


        public FilterVM()
        {
            _filterBuilder = new FilterBuilder();

            _FilterOptions = new List<string>()
            {
                "Event name",
                "Rating",
                "Type"
            };

            _stringConditions = new List<string>()
            {
                "Contains",
                "Starts with",
                "Ends with",
                "Exact match"
            };

            _numberConditions = new List<string>()
            {
                "=",
                "<",
                ">",
                "≤",
                "≥"
            };

            _typeConditions = new List<string>()
            {
                "Is"
            };

            SelectedFilter = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
