using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

namespace Chap1_3
{
	public class ChatCtrl : MonoBehaviour
	{
		private enum ChatState
		{
			Lobby,
			Room,
		}


		public ChatLoginCtrl _chatLogin;
		public ChatInputCtrl _chatInput;
		public ChatFieldCtrl _chatField;


		private ChatState m_ChatState;
		private string m_UserName;
		private DateTime m_Date;
		private int m_UniqueId;


		#region 変更・使用する場所
		/// <summary>
		/// メインループ
		/// </summary>
		private IEnumerator mainAsync ()
		{
			while (true) {
				yield return waitLoginAsync ();
				yield return chatAsync ();
			}
		}

		/// <summary>
		/// ログインされるまで待つ
		/// </summary>
		private IEnumerator waitLoginAsync ()
		{
			while (m_ChatState == ChatState.Lobby) {
				yield return 0;
			}
		}

		/// <summary>
		/// 自分で入力した文字列を表示
		/// サーバーに他のユーザーのデータがあれば取得し、表示 (updateChatField)
		/// </summary>
		private IEnumerator chatAsync ()
		{
			IEnumerator updateChatIterator = updateChatFieldAsync ();
			StartCoroutine (updateChatIterator);
			while (m_ChatState == ChatState.Room) {
				yield return 0;
			}
			StopCoroutine (updateChatIterator);
		}

		/// <summary>
		/// ログイン（チャットルームに入る）
		/// </summary>
		private void login (string iUserName)
		{
			m_UserName = iUserName;
			m_UniqueId = UnityEngine.Random.Range (int.MinValue + 1, int.MaxValue -1);

			_chatField.reset ();

			changeState (ChatState.Room);
		}

		/// <summary>
		/// ログアウト（ログイン画面にもどる）
		/// </summary>
		private void logout ()
		{
			changeState (ChatState.Lobby);
		}

		/// <summary>
		/// １秒ごとにサーバーをチェックし、データがあれば出力
		/// </summary>
		private IEnumerator updateChatFieldAsync ()
		{
			while (true) {
				NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("chat");
				query.OrderByDescending ("createDate");
				query.Limit = 20;

				query.FindAsync ((objects, error) => {
					if (error != null) {
						Debug.LogError (error.ToString ());
					} else {
						bool hasNew = false;
						foreach (var obj in objects) {
							if (m_Date < obj.UpdateDate && m_UniqueId != int.Parse (obj ["unique_id"].ToString ())) {
								_chatField.chatOutput (obj ["user_name"].ToString (), obj ["chat_text"].ToString (), "#000088");
								hasNew = true;
							}
						}
						if (hasNew) {
							m_Date = DateTime.UtcNow;
						}
					}
				});
				yield return new WaitForSeconds (1.0f);
			}
		}
		#endregion


		private void Start ()
		{
			initialize ();
			StartCoroutine (mainAsync ());
		}

		private void initialize ()
		{
			m_Date = DateTime.UtcNow;

			_chatLogin.setOnEnterEvent (login);
			_chatInput.setOnEnterEvent (chatOutput);
			_chatField.setOnEnterLogoutEvent (logout);

			changeState (ChatState.Lobby);
		}

		private void changeState (ChatState iNextTo)
		{
			_chatLogin.gameObject.SetActive (iNextTo == ChatState.Lobby);
			_chatInput.gameObject.SetActive (iNextTo == ChatState.Room);
			_chatField.gameObject.SetActive (iNextTo == ChatState.Room);

			m_ChatState = iNextTo;
		}

		private void chatOutput (string iChatText)
		{
			var chatData = new NCMBObject ("chat");
			_chatField.chatOutput (m_UserName, iChatText);
			chatData["unique_id"] = m_UniqueId;
			chatData["user_name"] = m_UserName;
			chatData["chat_text"] = iChatText;
			chatData.SaveAsync ();
		}
	}
}
