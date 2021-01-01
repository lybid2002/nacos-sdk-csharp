﻿namespace Nacos.Naming.Dtos
{
    using Nacos.Common;
    using System.Collections.Generic;
    using System.Linq;

    public class Instance
    {
        /// <summary>
        /// unique id of this instance.
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// instance ip.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// instance port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// instance weight.
        /// </summary>
        public double Weight { get; set; } = 1.0D;

        /// <summary>
        /// instance health status.
        /// </summary>
        public bool Healthy { get; set; } = true;

        /// <summary>
        /// If instance is enabled to accept request.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// If instance is ephemeral.
        /// </summary>
        public bool Ephemeral { get; set; } = true;

        /// <summary>
        /// cluster information of instance.
        /// </summary>
        public string ClusterName { get; set; }

        /// <summary>
        ///  Service information of instance.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// user extended attributes.
        /// </summary>
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();

        public void AddMetadata(string key, string value)
        {
            if (this.Metadata == null) this.Metadata = new Dictionary<string, string>();

            Metadata[key] = value;
        }

        public string ToInetAddr() => $"{this.Ip}:{this.Port}";

        public long GetInstanceHeartBeatInterval() => GetMetaDataByKeyWithDefault(PreservedMetadataKeys.HEART_BEAT_INTERVAL, Constants.DEFAULT_HEART_BEAT_INTERVAL);

        public long GetInstanceHeartBeatTimeOut() => GetMetaDataByKeyWithDefault(PreservedMetadataKeys.HEART_BEAT_TIMEOUT, Constants.DEFAULT_HEART_BEAT_TIMEOUT);

        public long GetIpDeleteTimeout() => GetMetaDataByKeyWithDefault(PreservedMetadataKeys.IP_DELETE_TIMEOUT, Constants.DEFAULT_IP_DELETE_TIMEOUT);

        public string GetInstanceIdGenerator() => GetMetaDataByKeyWithDefault(PreservedMetadataKeys.INSTANCE_ID_GENERATOR, Constants.DEFAULT_INSTANCE_ID_GENERATOR);

        private long GetMetaDataByKeyWithDefault(string key, long defaultValue)
        {
            if (Metadata == null || !Metadata.Any()) return defaultValue;

            if (Metadata.TryGetValue(key, out var value)
                && !string.IsNullOrWhiteSpace(value)
                && int.TryParse(value, out _))
            {
                return long.Parse(value);
            }

            return defaultValue;
        }

        private string GetMetaDataByKeyWithDefault(string key, string defaultValue)
        {
            if (Metadata == null || !Metadata.Any()) return defaultValue;

            return Metadata[key];
        }
    }
}