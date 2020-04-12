using System.Windows.Forms;
using XPerf.Api;

namespace XPerf.DataSource.Random
{
    [XPerfPlugin("Random", "Provides random data for debugging")]
    public class RandomDataSourceProvider : XPerfSourceProvider
    {
        /// <inheritdoc />
        public override XPerfDataProvider CreateDataProvider()
        {
            var form = new FormCreateRandomSource();
            var res = form.ShowDialog();

            if (res != DialogResult.OK)
                return null;

            return new RandomDataSource(form.SourceName, form.SourceMin, form.SourceMax);
        }
    }
}