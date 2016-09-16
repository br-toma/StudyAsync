using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chap1_1
{
	public class TestCoroutine : MonoBehaviour
	{
		private InputField m_InputField {
			get {
				return GetComponent<InputField> ();
			}
		}


		private IEnumerator iterator;


		public void clearField ()
		{
			m_InputField.text = "";
		}

		private void Start ()
		{
			// IEnumerator型メソッドの処理開始
	//		StartCoroutine (waitInput ());
			iterator = waitInput ();
		}

		private void Update ()
		{
			iterator.MoveNext ();
			Debug.Log (iterator.Current);
		}

		// 入力キーを表示
		private IEnumerator waitInput ()
		{
			while (true) {
				while (!Input.anyKeyDown) {
					yield return "string";
				}
				m_InputField.text += Input.inputString;
				yield return 0;
			}
		}
	}
}
