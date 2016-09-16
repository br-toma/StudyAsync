using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chap1_2
{
	public class BulletCtrl : MonoBehaviour
	{
		private void Update ()
		{
			transform.position += Vector3.up * 0.3f;
		}

		private void OnBecameInvisible ()
		{
			Destroy (gameObject);
		}
	}
}
