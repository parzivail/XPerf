namespace XPerf.Api
{
    public interface IPollableDataSource
    {
        float Poll();

        string Format(float value);
    }
}
