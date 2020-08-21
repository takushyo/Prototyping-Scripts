using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TakuLib {

    class Util {
        /// <summary>
        /// Will give a 0-1 value from a range,
        /// 
        /// USAGE: 
        /// 
        /// float k = 5;
        /// NormalizeFloatRange(ref k, 0, 10);
        /// k will equal 0.5f
        /// 
        /// float k = 5;
        /// NormalizeFloatRange(ref k, 5, 10);
        /// k will equal 0.0f
        /// </summary>
        public static void NormalizeFloatRange(ref float Current, float min, float max) => Current = (Current - min) / (max - min);
    }

    public static class VectorExtensions {
        /// <summary>
        /// USAGE: transform.position = transform.position.With(x: 1);
        /// this will effectivly do the same as
        /// new Vector3(1, transform.position.y, transform.position.z);
        /// 
        /// additive bool will add it onto existing field
        /// transform.position = transform.position.With(y: 5, additive: true);
        /// will be the same as
        /// new Vector3(transform.position.x, transform.position.y + 5, tranform.position.z);
        /// </summary>
        
        public static Vector3 With(this Vector3 vec, float? x = null, float? y = null, float? z = null, bool additive = false) =>
        additive == false ? new Vector3(x ?? vec.x, y ?? vec.y, z ?? vec.z) :
                            new Vector3(x + vec.x ?? vec.x, y + vec.y ?? vec.y, z + vec.z ?? vec.z);
        public static Vector2 With(this Vector2 vec, float? x = null, float? y = null, bool additive = false) =>
            additive == false ? new Vector2(x ?? vec.x, y ?? vec.y) :
                                new Vector2(x + vec.x ?? vec.x, y + vec.y ?? vec.y);
    }

    public static class ComponantExtensions {
        /// <summary>
        /// USAGE: 
        /// RectTransform newRect = gameObject.GetRect();
        /// RectTransform newRect = gameObject.Transform.GetRect();
        /// </summary>
        public static RectTransform GetRect(this GameObject obj) => obj.GetComponent<RectTransform>();
        public static RectTransform GetRect(this Transform obj) => obj.GetComponent<RectTransform>();
    }

    public static class SubclassEnumerator {
        /// <summary>
        /// This will return an Ienumerable of your subtypes of a class that isnt abstract
        /// EG
        /// public absract Item
        /// public Eggs : Item
        /// public Tomatos : Item
        /// 
        /// In this instance, Eggs and Items will be returned in an Ienumerable<Item>
        /// 
        /// USAGE
        /// List<Item> ItemTypes = GetEnumerableOfSubTypes<Item>(null) as Item;
        /// </summary>

        
        public static IEnumerable<T> GetEnumerableOfSubTypes<T>(params object[] args) where T : class {
            List<T> objects = new List<T>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)))) {
                objects.Add((T)Activator.CreateInstance(type, args));
            }
            return objects;
        }

        /// <summary>
        /// This is pretty much the same as above but forced into a dictionary. you will have to cast the result from type in the dictionary
        /// as you cannot have a generic entry for a value.
        /// 
        /// This is REALLY helpful to build a database of items and have a string reference to them.
        /// using the string reference you can save via JSON just the type name and look it back up on load
        /// 
        /// USAGE:
        /// Dictionary<string, Type> ItemTypes = GetDictOfSubTypes<Item>(null);
        /// Item item = ItemTypes["Tomato"] as Item;
        /// </summary>

        public static Dictionary<string, Type> GetDictOfSubTypes<T>(params object[] args) where T : class {
            var dict = new Dictionary<string, Type>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)))) {
                var obj = (T)Activator.CreateInstance(type, args);
                dict.Add(obj.GetType().Name, obj.GetType());
            }
            return dict;
        }
    }
}
