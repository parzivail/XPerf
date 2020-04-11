namespace XPerf.Api
{
    public abstract class XPerfDataProvider
    {
        private float _value;

        public string UnitHeader { get; protected set; }
        public string GraphHeader { get; protected set; }
        public string GraphDetailHeader { get; protected set; }

        public abstract float CollectData();

        public virtual void Poll()
        {
            _value = CollectData();
        }

        public virtual float GetValue()
        {
            return _value;
        }

        public virtual string Format(float value)
        {
            return $"{value}";
        }

        public virtual float? GetMin() => null;

        public virtual float? GetMax() => null;
    }
}
