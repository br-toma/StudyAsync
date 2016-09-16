using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using Random = System.Random;

namespace Chap2_1
{
	public class ThreadRotate : MonoBehaviour
	{
		private Thread m_Thread;
		private Random m_Random;

		private IEnumerator Start ()
		{
			m_Random = new Random ();
			while (true) {
				m_Thread = new Thread (updateRotate);
				m_Thread.Start ();
//				while (m_Thread.IsAlive) {
//					yield return 0;
//				}
//				updateRotate ();
				yield return new WaitForSeconds (1.0f);
			}
		}

		private void updateRotate ()
		{
			int a = 0;
			for (int i = 0; i < 100000000; i++) {
				a++;
			}
			print (m_Random.Next ());
		}
	}
}
