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

        //마스터 서버 접속 성공시 함수 실행
        public override void OnConnectedToMaster()
        {
            connectionInfoText.text = "Online : Connected to Master Server";
        }

        //접속 실패시 사유가 들어오고 함수 실행
        public override void OnDisconnected(DisconnectCause cause)
        {
            //실패 사유 출력
            connectionInfoText.text = $"Offline : Connection Disabled {cause.ToString()}";

            //재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }

        public void Connect()
        {
            //여러번 접속 시도 방지
            joinButton.interactable = false;
            
            //안전장치
            if(PhotonNetwork.IsConnected)
            {
                connectionInfoText.text = "Connecting to Random Room...";
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                connectionInfoText.text = $"Offline : Connection Disabled - Try reconnecting...";

                //재접속 시도
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        //룸이 없을때 실행
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            connectionInfoText.text = "There is no empty room, Creating new Room.";

            //방 만들기
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 3 });
            //방이름 : null
        }

        public override void OnJoinedRoom()
        {
            connectionInfoText.text = "Connected with Room";

            //모든 사람이 같은 장소로 이동
            PhotonNetwork.LoadLevel("InGame");
        }
    }

}
