namespace ReWork.Logic.Infustructure
{
    public class OperationDetails
    {
        public OperationDetails(bool succedeed, string propertyName, string message)
        {
            Succedeed = succedeed;
            PropertyName = propertyName;
            Message = message;
        }

        public bool Succedeed { get; set; }
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
