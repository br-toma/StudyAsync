using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chap1_2
{
	public class JetCtrl : MonoBehaviour
	{
		public bool _isAsync;

		public GameObject _resBullet;

		#region Coroutine
		private void Start ()
		{
			StartCoroutine (waitInputAsync ());
		}

		private IEnumerator waitInputAsync ()
		{
			while (true) {
				if (!_isAsync) {
					yield return 0;
					continue;
				}

				yield return moveSide ();

				if (Input.GetMouseButtonDown (0)) {
					yield return StartCoroutine (trippleShotAsync ());
				}
				yield return 0;
			}
		}

		private IEnumerator trippleShotAsync ()
		{
			var shotCount = 0;
			while (shotCount < 3) {
				Instantiate<GameObject> (_resBullet, transform.position, Quaternion.identity);
				shotCount++;
				yield return new WaitForSeconds (0.1f);
			}
		}

		private IEnumerator moveSide ()
		{
			bool isLeft = Input.GetKeyDown (KeyCode.LeftArrow);
			bool isRight = Input.GetKeyDown (KeyCode.RightArrow);

			if (isLeft || isRight) {
				float movePow;
				if (isLeft) {
					movePow = -0.1f;
				} else {
					movePow = 0.1f;
				}

				while ((movePow > 0.01f || movePow < -0.01f)) {
					transform.position += Vector3.right * movePow;
					movePow *= 0.96f;
					yield return 0;
				}
			}
		}
		#endregion

		#region Update
		private bool m_IsShot = false;
		private int m_ShotCount = 0;
		private int m_ShotFrameCount = 0;
		private void Update ()
		{
			if (_isAsync) {
				return;
			}

			waitInput ();

			trippleShot ();
		}

		private void waitInput ()
		{
			if (!m_IsShot && Input.GetMouseButtonDown (0)) {
				m_ShotCount = 0;
				m_ShotFrameCount = 0;
				m_IsShot = true;
			}
		}

		private void trippleShot ()
		{
			if (m_IsShot) {
				if (m_ShotCount >= 3) {
					m_IsShot = false;
				} else {
					if (m_ShotFrameCount++ > 3) {
						Instantiate<GameObject> (_resBullet, transform.position, Quaternion.identity);
						_resBullet.transform.position = transform.position;
						m_ShotFrameCount = 0;
						m_ShotCount++;
					}
				}
			}
		}
		#endregion
	}
}
