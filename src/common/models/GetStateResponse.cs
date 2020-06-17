namespace common.models
{
    public class GetStateResponse
    {
       
        public State StateResult { get; set; }
        public GetStateStatus StateStatus { get; set; }

        public override string ToString()
        {
            return $"{StateStatus.ToString()}, { StateResult.ToString()}";
        }
    }

    public enum GetStateStatus
    {
        Success = 0,
        CommandCouldCauseOffGrid = 1,
        InvalidInitialState = 2,
        UnknowError = 3
    }
}
