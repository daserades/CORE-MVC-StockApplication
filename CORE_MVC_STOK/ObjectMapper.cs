using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CORE_MVC_STOK
{
    public class ObjectMapper
    {
        /// <summary>
        /// Verilen kaynak nesneyi hedef nesnenin özelliklerine set eder.
        /// </summary>
        /// <param name="target">Hedef</param>
        /// <param name="source">Kaynak</param>
        public static void Map(object target, object source)
        {
            if (source != null && target != null)
            {
                foreach (var sourceProperty in source.GetType().GetProperties())
                {
                    MapProperty(sourceProperty, source, target);
                }
            }
        }

      

        /// <summary>
        /// Verilen kaynak nesneyi hedef nesne yaratarak nesnenin özelliklerine set eder.
        /// </summary> 
        /// <param name="source">Kaynak</param>
        public static T Map<T>(object source) where T : class
        {
            if (source == null)
                return null;

            object target = Activator.CreateInstance(typeof(T));
            foreach (var sourceProperty in source.GetType().GetProperties())
            {
                MapProperty(sourceProperty, source, target);
            }
            return (T)target;
        }

        /// <summary>
        /// Verilen kaynak nesneyi hedef nesnenin özelliklerine set eder. Exclude parametresi ile verilen özellikler atanmaz.
        /// </summary>
        /// <param name="target">Hedef</param>
        /// <param name="source">Kaynak</param>
        /// <param name="exclude">Atanmayacak Özellikler</param>
        public static void MapExclude(object target, object source, params string[] exclude)
        {
            if (source != null && target != null)
            {
                foreach (var sourceProperty in source.GetType().GetProperties())
                {
                    if (exclude.Contains(sourceProperty.Name))
                        continue;
                    MapProperty(sourceProperty, source, target);
                }
            }
        }

        /// <summary>
        /// Propertyinin değerini set eder.
        /// </summary>
        /// <param name="sourceProperty"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private static void MapProperty(PropertyInfo sourceProperty, object source, object target)
        {
            PropertyInfo targetProperty = target.GetType().GetProperties()
                .FirstOrDefault(x => x.Name == sourceProperty.Name);

            if (targetProperty != null)
            {
                if (targetProperty.PropertyType == sourceProperty.PropertyType && sourceProperty.GetValue(source) != null)
                    targetProperty.SetValue(target, sourceProperty.GetValue(source));
            }
        }
    }
}
