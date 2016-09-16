using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chap1_3
{
	public class ChatLoginCtrl : MonoBehaviour
	{
		public delegate void OnEnter (string iUserName);


		public InputField _inputField;
		public Button _enterButton;


		public void setOnEnterEvent (OnEnter iOnEnter)
		{
			_enterButton.onClick.RemoveAllListeners ();
			_enterButton.onClick.AddListener (() => {
				iOnEnter.Invoke (_inputField.text);
			});
		}
	}
}
