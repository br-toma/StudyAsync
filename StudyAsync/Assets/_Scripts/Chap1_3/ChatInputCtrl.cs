using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Chap1_3
{
	public class ChatInputCtrl : MonoBehaviour
	{
		public delegate void OnEnterChat (string iChatText);


		public InputField _inputField;
		public Button _enterButton;


		private OnEnterChat m_OnEnterChat;


		public void setOnEnterEvent (OnEnterChat iAction)
		{
			m_OnEnterChat = iAction;
		}

		public void onEnter ()
		{
			if (m_OnEnterChat != null) {
				m_OnEnterChat.Invoke (_inputField.text);
			}
			_inputField.text = "";
		}


		private void Start ()
		{
			_enterButton.onClick.AddListener (onEnter);
		}
	}
}
