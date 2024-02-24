namespace Sabanishi.ZundaManufacture.Common
{
    public abstract class InspectorDictionaryPair<TKey,TValue>
    {
        public TKey Key;
        public TValue Value;
        
        protected InspectorDictionaryPair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}