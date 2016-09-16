using System;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chap2_2
{
	public class ClockCtrl : MonoBehaviour
	{
		private TextMesh _textMesh {
			get {
				return GetComponent<TextMesh> ();
			}
		}

		private IEnumerator Start ()
		{
			while (true) {
				var client = new WebClient ();
				var data = client.DownloadData ("https://ntp-a1.nict.go.jp/cgi-bin/json");
				var dataStr = Encoding.ASCII.GetString (data);
				var dataJson = (IDictionary)MiniJSON.Json.Deserialize (dataStr);
				double d = double.Parse (dataJson ["st"].ToString ());
				DateTime date = UnixTimeToDateTime (d);
				_textMesh.text = date.ToString ("hh:mm:ss");
				yield return new WaitForSeconds (1.0f);
			}
		}

		private DateTime UnixTimeToDateTime(double unixTime){
			DateTime dt = 
				new DateTime (1970, 1, 1, 9, 0, 0, System.DateTimeKind.Utc);
			dt = dt.AddSeconds (unixTime);
			return dt;
		}
	}
}
