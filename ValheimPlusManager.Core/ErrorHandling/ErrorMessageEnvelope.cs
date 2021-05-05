using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValheimPlusManager.Core.ErrorHandling
{
    public class ErrorMessageEnvelope
    {
        private List<string> _messages = new List<string>();

        /// <summary>
        /// Add an error message using the default message template where the variable value is hidden.
        /// </summary>
        /// <param name="variableName">The name of the error-invoking variable.</param>
        public void AddMessage(string variableName)
        {
            _messages.Add(string.Format(ErrorMessageConstants.DefaultMessage, variableName, ErrorMessageConstants.HiddenArg));
        }

        /// <summary>
        /// Add an error message using the default message template.
        /// </summary>
        /// <param name="variableName">The name of the error-invoking variable.</param>
        /// <param name="variable">The value of the error-invoking variable. </param>
        public void AddMessage(object variable, params string[] variableName)
        {
            var joinedVariableName = string.Join(".", variableName);

            if (string.IsNullOrEmpty(variable?.ToString()))
            {
                _messages.Add(string.Format(ErrorMessageConstants.DefaultMessage, joinedVariableName, ErrorMessageConstants.NullArg));
            }
            else
            {
                _messages.Add(string.Format(ErrorMessageConstants.DefaultMessage, joinedVariableName, variable.ToString()));
            }
        }

        /// <summary>
        /// Add an error message using a provided custom message.
        /// </summary>
        /// <param name="message">Custom error message.</param>
        public void AddCustomMessage(string message)
        {
            _messages.Add(message);
        }

        /// <summary>
        /// Returns a new-lined concatenation of all the added messages.
        /// </summary>
        public override string ToString()
        {
            var messageConcat = string.Empty;

            if (HasMessages)
            {
                foreach (var message in _messages)
                {
                    messageConcat += $"{message}{Environment.NewLine}";
                } 
            }

            return messageConcat;
        }

        /// <summary>
        /// Throws an exception that will include all of the current error messages in the exception message.
        /// </summary>
        /// <typeparam name="TException">Must be of type <see cref="Exception"/></typeparam>
        public TException Throw<TException>()
            where TException : Exception
        {
            throw (Exception)System.Activator.CreateInstance(
                typeof(TException), ToString());
        }

        /// <summary>
        /// Gets the current collection of error messages as a read-only collection.
        /// </summary>
        public IReadOnlyCollection<string> Get => _messages.AsReadOnly();
        /// <summary>
        /// Returns whether the current collection of error messages has any values or not.
        /// </summary>
        public bool HasMessages => _messages?.Any() ?? false;
        /// <summary>
        /// Returns the count of the current collection of error messages.
        /// </summary>
        public int Count => _messages?.Count ?? 0;
    }
}
