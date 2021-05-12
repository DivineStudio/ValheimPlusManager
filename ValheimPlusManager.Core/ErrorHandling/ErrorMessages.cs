using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValheimPlusManager.Core.ErrorHandling
{
    public class ErrorMessages
    {
        private List<string> _messages = new List<string>();

        private static string JoinedNames(string variableName, string[] variableParentNames)
        {
            var joinedParentNames = string.Join(".", variableParentNames);
            var joinedNames = !string.IsNullOrEmpty(joinedParentNames)
                ? string.Join(".", joinedParentNames, variableName)
                : variableName;
            return joinedNames;
        }

        /// <summary>
        /// Add an error message using the default message template where the variable value is hidden.
        /// </summary>
        /// <param name="variableName">The name of the error-invoking variable.</param>
        public void AddMessage(string variableName, params string[] variableParentNames)
        {
            AddMessage(ErrorMessagesConstants.HiddenArg, variableName, variableParentNames);
        }

        /// <summary>
        /// Add an error message using the default message template.
        /// </summary>
        /// <param name="variable">The value of the error-invoking variable. Pass null to hide value.</param>
        /// <param name="variableName">The name of the error-invoking variable.</param>
        /// <param name="variableParentNames">Zero or many names of the parent of object(s).</param>
        public void AddMessage(object variable, string variableName, params string[] variableParentNames)
        {
            AddMessage(null, variable, variableName, variableParentNames);
        }

        /// <summary>
        /// Add an error message using the default message template with exception.
        /// </summary>
        /// <param name="innerException">Inner Exception</param>
        /// <param name="variable">The value of the error-invoking variable. Pass null to hide value.</param>
        /// <param name="variableName">The name of the error-invoking variable.</param>
        /// <param name="variableParentNames">Zero or many names of the parent of object(s).</param>
        public void AddMessage(Exception innerException, object variable, string variableName, params string[] variableParentNames)
        {
            var joinedNames = JoinedNames(variableName, variableParentNames);
            var message = string.Empty;

            if (innerException != null)
            {
                if (string.IsNullOrEmpty(variable?.ToString()))
                {
                    message += string.Format(ErrorMessagesConstants.DefaultMessageWithException, joinedNames,
                        ErrorMessagesConstants.NullArg, innerException);
                }
                else
                {
                    message += string.Format(ErrorMessagesConstants.DefaultMessageWithException, joinedNames,
                        variable.ToString(), innerException);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(variable?.ToString()))
                {
                    message += string.Format(ErrorMessagesConstants.DefaultMessage, joinedNames, ErrorMessagesConstants.NullArg);
                }
                else
                {
                    message += string.Format(ErrorMessagesConstants.DefaultMessage, joinedNames, variable.ToString());
                }
            }
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
        public void Throw<TException>()
            where TException : Exception
        {
            Throw<TException>(null);
        }

        /// <summary>
        /// Throws an exception that will include all of the current error messages in the exception message.
        /// </summary>
        /// <typeparam name="TException">Must be of type <see cref="Exception"/></typeparam>
        /// <param name="innerException">Inner Exception of type <see cref="Exception"/></param>
        public void Throw<TException>(Exception innerException)
            where TException : Exception
        {
            throw (Exception)System.Activator.CreateInstance(
                typeof(TException), ToString(), innerException);
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
