﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChatRoom.Hubs
{
    public class ChatHub: Hub
    {
        public static List<string> ConnIDList = new List<string>();

        public override async Task OnConnectedAsync()
        {

            if (ConnIDList.Where(p => p == Context.ConnectionId).FirstOrDefault() == null)
            {
                ConnIDList.Add(Context.ConnectionId);
            }
            // 更新連線 ID 列表
            string jsonString = JsonConvert.SerializeObject(ConnIDList);
            await Clients.All.SendAsync("UpdList", jsonString);

            // 更新個人 ID
            await Clients.Client(Context.ConnectionId).SendAsync("UpdSelfID", Context.ConnectionId);

            // 更新聊天內容
            await Clients.All.SendAsync("UpdContent", "新連線 ID: " + Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            string id = ConnIDList.Where(p => p == Context.ConnectionId).FirstOrDefault();
            if (id != null)
            {
                ConnIDList.Remove(id);
            }
            // 更新連線 ID 列表
            string jsonString = JsonConvert.SerializeObject(ConnIDList);
            await Clients.All.SendAsync("UpdList", jsonString);

            // 更新聊天內容
            await Clients.All.SendAsync("UpdContent", "已離線 ID: " + Context.ConnectionId);

            await base.OnDisconnectedAsync(ex);
        }

        public async Task SendMessage(string selfID, string message, string sendToID)
        {
            if (string.IsNullOrEmpty(sendToID))
            {
                await Clients.All.SendAsync("UpdContent", selfID + " 說: " + message);
            }
            else
            {
                // 接收人
                await Clients.Client(sendToID).SendAsync("UpdContent", selfID + " 私訊向你說: " + message);

                // 發送人
                await Clients.Client(Context.ConnectionId).SendAsync("UpdContent", "你向 " + sendToID + " 私訊說: " + message);
            }
        }
    }
}
