using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ButtonController : MonoBehaviour {

	void Start(){

		ConnectionMenager.SetConnectionOptions(6007, 6006);
	}

	public void OnClick(int motionType)
	{
		MotionMessage message = new MotionMessage();
		message.motionType = (MotionType)motionType;

		var bytes = message.Serialize ();

		ConnectionMenager.Instanse.Send (bytes);
	}
}

