namespace Application.Abstractions.Responses
{
    public abstract record BaseResponse
    {
        public List<string> Messages { get; }

        public BaseResponse()
        {
            Messages = new List<string>();
        }

        public void AddMessage(string message) => Messages.Add(message);

        public void AddMessages(IEnumerable<string> messages) => Messages.AddRange(messages);
    }
}