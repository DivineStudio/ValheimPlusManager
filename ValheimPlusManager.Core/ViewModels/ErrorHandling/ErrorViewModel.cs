using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ValheimPlusManager.Core.ViewModels.ErrorHandling
{
    public class ErrorViewModel
    {
        private List<Exception> _exceptions = new List<Exception>();

        public void Add(Exception exception)
        {
            _exceptions.Add(exception);
        }

        public void AddRange(IEnumerable<Exception> exceptions)
        {
            _exceptions.AddRange(exceptions);
        }

        public IReadOnlyCollection<Exception> Get => _exceptions.AsReadOnly();
        public override string ToString()
        {
            var exceptionsConcat = string.Empty;

            foreach (var exception in _exceptions)
            {
                exceptionsConcat += $"{exception.Message}{Environment.NewLine}";
            }

            return exceptionsConcat;
        }

        public int Count => _exceptions.Count;
    }
}
