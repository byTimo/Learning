namespace Garden
{
    public interface IClonable<out T>
    {
        T Clone();
    }
}