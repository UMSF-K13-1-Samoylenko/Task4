// <copyright file="SourceNotFoundException.cs" company="My company">
//     Copyright (c) My company". All rights reserved.
// </copyright>

namespace Task4_Lib
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception which is created when source, which you ask was not found
    /// </summary>
    public class SourceNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceNotFoundException"/> class
        /// </summary>
        /// <param name="message">Message of exception</param>
        public SourceNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceNotFoundException"/> class
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="innerException">Inner exception for current exception</param>
        public SourceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceNotFoundException"/> class
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Streaming context</param>
        protected SourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
