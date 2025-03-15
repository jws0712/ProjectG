namespace Manager.Lobby
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //Unity
    using UnityEngine;
    using UnityEngine.UI;

    //Photon
    using Photon.Pun;
    using Photon.Realtime;

    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        public Text connectionInfoText;
        public Button joinButton;

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();

            connectionInfoText.text = "Connecting To Master Server...";
        }

        //������ ���� ���� ������ �Լ� ����
        public override void OnConnectedToMaster()
        {
            connectionInfoText.text = "Online : Connected to Master Server";
        }

        //���� ���н� ������ ������ �Լ� ����
        public override void OnDisconnected(DisconnectCause cause)
        {
            //���� ���� ���
            connectionInfoText.text = $"Offline : Connection Disabled {cause.ToString()}";

            //������ �õ�
            PhotonNetwork.ConnectUsingSettings();
        }

        public void Connect()
        {
            //������ ���� �õ� ����
            joinButton.interactable = false;
            
            //������ġ
            if(PhotonNetwork.IsConnected)
            {
                connectionInfoText.text = "Connecting to Random Room...";
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                connectionInfoText.text = $"Offline : Connection Disabled - Try reconnecting...";

                //������ �õ�
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        //���� ������ ����
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            connectionInfoText.text = "There is no empty room, Creating new Room.";

            //�� �����
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 3 });
            //���̸� : null
        }

        public override void OnJoinedRoom()
        {
            connectionInfoText.text = "Connected with Room";

            //��� ����� ���� ��ҷ� �̵�
            PhotonNetwork.LoadLevel("InGame");
        }
    }

}
