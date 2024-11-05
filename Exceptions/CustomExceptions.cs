namespace MovieCharactersAPI.Exceptions
{
    /// <summary>
    /// Exception thrown when an entity is not found
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        public NotFoundException(string name, object key)
            : base($"Entity '{name}' ({key}) was not found.") { }
    }

    /// <summary>
    /// Exception thrown when there is a conflict with existing data
    /// </summary>
    public class ConflictException : Exception
    {
        public ConflictException() : base() { }

        public ConflictException(string message) : base(message) { }

        public ConflictException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when input validation fails
    /// </summary>
    public class CustomValidationException : Exception  // Renamed from ValidationException
    {
        public CustomValidationException() : base() { }

        public CustomValidationException(string message) : base(message) { }

        public CustomValidationException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    /// <summary>
    /// Exception thrown when an operation is not allowed
    /// </summary>
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base() { }

        public ForbiddenException(string message) : base(message) { }

        public ForbiddenException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}