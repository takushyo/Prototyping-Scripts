using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace TakuLib {

    class Util { }

	public static class VectorExtensions {
        public static Vector3 With(this Vector3 vec, float? x = null, float? y = null, float? z = null, bool additive = false) =>
        additive == false ? new Vector3(x ?? vec.x, y ?? vec.y, z ?? vec.z) :
                            new Vector3(x + vec.x ?? vec.x, y + vec.y ?? vec.y, z + vec.z ?? vec.z);
        public static Vector2 With(this Vector2 vec, float? x = null, float? y = null, bool additive = false) =>
            additive == false ? new Vector2(x ?? vec.x, y ?? vec.y) :
                                new Vector2(x + vec.x ?? vec.x, y + vec.y ?? vec.y);
    }

    public static class ComponantExtensions {
        public static RectTransform GetRect(this GameObject obj) => obj.GetComponent<RectTransform>();
        public static RectTransform GetRect(this Transform obj) => obj.GetComponent<RectTransform>();
    }
}
