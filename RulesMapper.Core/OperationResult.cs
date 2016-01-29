namespace RulesMapper.Core
{
    public class OperationResult
    {
        public dynamic Data { get; private set; }
        public string Message { get; private set; }
        public OperationStatus Status { get; private set; }

        public OperationResult(OperationStatus status, string message, dynamic data)
        {
            this.Data = data;
            this.Status = status;
            this.Message = message;
        }

        public OperationResult(OperationStatus status, string message): this(status,message, default(object))
        {
           
        }
    }
}
