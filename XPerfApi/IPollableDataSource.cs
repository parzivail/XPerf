namespace XPerf.Api
{
    public interface IPollableDataSource
    {
        void Poll();

        float GetValue();

        string Format(float value);

        string GetUnitHeader();

        string GetGraphHeader();

        string GetGraphDetailHeader();
    }
}
