namespace Manager.Network
{
    //System
    using System.Collections;
    using System.Collections.Generic;

    //Unity
    using UnityEngine;

    //Photon
    using Photon.Pun;
    using Photon.Realtime;

    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        private void Awake()
        {
            Screen.SetResolution(960, 540, false);
            PhotonNetwork.SendRate = 60;
            PhotonNetwork.SerializationRate = 30;
        }

        public void Contect() => PhotonNetwork.ConnectUsingSettings();

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 3 }, null);
        }

        public override void OnJoinedRoom()
        {
            
        }
    }
}
