namespace BLL.Infrastructure
{
    public class OperationDetails
    {
        public bool Succeeded { get; private set; }
        public string Message { get; private set; }
        public string Property { get; set; }
        public OperationDetails(bool succeeded, string message, string property)
        {
            Succeeded = succeeded;
            Message = message;
            Property = property;
        }
    }
}
