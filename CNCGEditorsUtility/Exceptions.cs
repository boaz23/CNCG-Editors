using System;
using System.Collections.Generic;
using System.Reflection;

namespace CNCGEditors {
    internal static class Exceptions {
        public const string NewLines = "\n\n";

        public static string ExceptionToStringFull(Exception ex) {
            string s;
            Exception exception;
            List<Exception> exceptions;

            if (ex == null) {
                return null;
            }
            exceptions = new List<Exception>();
            exception = ex;
            s = string.Empty;
            while (ex.InnerException != null) {
                Exception baseException;

                if (!exceptions.Contains(exception)) {
                    exceptions.Add(exception);
                    s += ExceptionToString(exception) + NewLines;
                }
                baseException = exception.GetBaseException();
                if (!exceptions.Contains(baseException)) {
                    exceptions.Add(baseException);
                    s += ExceptionToString(baseException) + NewLines;
                }
                exception = exception.InnerException;
            }
            if (!exceptions.Contains(exception)) {
                exceptions.Add(exception);
                s += ExceptionToString(exception);
            }
            return s;
        }
        private static string ExceptionToString(Exception ex) {
            string s;
            Type ex_type;

            ex_type = ex.GetType();
            s = "Exception:";
            foreach (PropertyInfo property in ex_type.GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
                try {
                    object propertyValue;
                    string propertyName;

                    propertyName = property.Name;
                    if (propertyName == "Data" || propertyName == "InnerException") {
                        continue;
                    }
                    propertyValue = property.GetValue(ex,
                                                      new object[] {
                                                      });
                    if (propertyValue != null) {
                        string propertyStringRepresntion;

                        propertyStringRepresntion = propertyValue.ToString();
                        if (!string.IsNullOrEmpty(propertyStringRepresntion)) {
                            s += string.Format("\n{0}: {1}", propertyName, propertyStringRepresntion);
                        }
                    }
                }
                catch {
                    // ignored
                }
            }
            s += string.Format("\nType: {0}", string.Format("[{0}]{1}", ex_type.Assembly.GetName().Name, ex_type.FullName));
            return s;
        }
    }
}
