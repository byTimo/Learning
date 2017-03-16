namespace Garden.Flowerbed
{
    public interface IGrowable : IClonable<IGrowable>
    {
        PlantSegment[] Segments { get; }
        IGrowable Grow();
    }
}