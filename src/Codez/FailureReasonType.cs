namespace Codez
{
    public enum FailureReasonType
    {
        None,
        Uniqueness,
        Stopped,
        Transform,
        RequestInvalid // e.g. requested a non-repeating code longer than the supplied alphabet
    }
}