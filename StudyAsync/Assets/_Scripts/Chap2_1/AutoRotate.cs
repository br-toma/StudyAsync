using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chap2_1
{
	public class AutoRotate : MonoBehaviour
	{
		public Vector3 _axis;
		public float _angle;
		public Space _relativeTo;

		private void Update ()
		{
			transform.Rotate (_axis, _angle, _relativeTo);
		}
	}
}
