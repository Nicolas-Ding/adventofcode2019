using System.Collections.Generic;

namespace IntCodeUtils
{
    public class DefaultToZeroDictionary : Dictionary<long, long>
    {
        public DefaultToZeroDictionary(IDictionary<long, long> dict) : base(dict)
        {

        }

        public new long this[long key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    base.Add(key, 0);
                }
                return base[key];
            }
            set => base[key] = value;
        }
    }
}
