using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class Validated<T>
    {
        private T value;
        public bool IsValid { get; set; }

        public T Value
        {
            get
            {
                if (IsValid)
                    return value;

                throw new InvalidOperationException("This instance is not valid");
            }
            set { this.value = value; }
        }

        public IEnumerable<Exception> Exceptions { get; set; }

        public Validated(T value)
        {
            this.Value = value;
            IsValid = true;
        }

        public Validated(IEnumerable<Exception> exceptions)
        {
            Exceptions = exceptions;
            IsValid = false;
        }
    }
}