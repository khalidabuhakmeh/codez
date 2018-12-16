namespace Codez.Alphabets
{
    public interface IAlphabet
    {
        char Get(int index);
        int Count { get; }
    }
}