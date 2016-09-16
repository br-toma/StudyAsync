using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Chap1_3
{
	public class ChatFieldCtrl : MonoBehaviour
	{
		public Text _textField;
		public Button _logoutButton;


		public void setOnEnterLogoutEvent (UnityAction iAction)
		{
			_logoutButton.onClick.RemoveAllListeners ();
			_logoutButton.onClick.AddListener (iAction);
		}

		public void chatOutput (string iUserName, string iChatText)
		{
			chatOutput (iUserName, iChatText, "#000000");
		}

		public void chatOutput (string iUserName, string iChatText, string iColorCode)
		{
			_textField.text += string.Format ("\n<color=\"{0}\">{1}:{2}</color>", iColorCode, iUserName, iChatText);
		}

		public void reset ()
		{
			_textField.text = "";
		}
	}
}
