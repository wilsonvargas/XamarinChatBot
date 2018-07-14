﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ChatBot.Clients.Models
{
    public partial class Activity
    {
        [JsonProperty("attachments")]
        public List<object> Attachments { get; set; }

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("conversation")]
        public ConversationId ConversationId { get; set; }

        [JsonProperty("entities")]
        public List<object> Entities { get; set; }

        [JsonProperty("from")]
        public User From { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("membersAdded")]
        public List<object> MembersAdded { get; set; }

        [JsonProperty("membersRemoved")]
        public List<object> MembersRemoved { get; set; }

        [JsonProperty("reactionsAdded")]
        public List<object> ReactionsAdded { get; set; }

        [JsonProperty("reactionsRemoved")]
        public List<object> ReactionsRemoved { get; set; }

        [JsonProperty("replyToId")]
        public string ReplyToId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("timestamp")]
        public System.DateTimeOffset Timestamp { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class BotMessage
    {
        [JsonProperty("activities")]
        public List<Activity> Activities { get; set; }

        [JsonProperty("watermark")]
        public string Watermark { get; set; }
    }

    public class Conversation
    {
        [JsonProperty(PropertyName = "conversationId")]
        public string ConversationId { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "streamUrl")]
        public string StreamUrl { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }

    public partial class ConversationId
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
