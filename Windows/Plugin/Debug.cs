using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin {
    /// <summary>
    /// A delegate for the MessageListened Event of the Debug Class
    /// </summary>
    /// <param name="channel">The identifier of the sending debug channel</param>
    /// <param name="message">The logged message</param>
    public delegate void MessageListenedDelegate(int channel, string message);
    /// <summary>
    /// A delegate with no parameters
    /// </summary>
    public delegate void EventDelegate();

    /// <summary>
    /// Provides functionality for logging information
    /// </summary>
    public static class Debug {
        /// <summary>
        /// Gets called if a new message is beeing logged and the channel is enabled
        /// </summary>
        public static event MessageListenedDelegate MessageListened;
        /// <summary>
        /// Gets called if the list of registered channels has changed
        /// </summary>
        public static event EventDelegate ChannelsChanged;
        /// <summary>
        /// Gets called if the list of enabled channels has changed
        /// </summary>
        public static event EventDelegate EnabledChannelsChanged;
        /// <summary>
        /// Gets called if the list of disabled channels has changed
        /// </summary>
        public static event EventDelegate DisabledChannelsChanged;
                                                                                  
        private static Dictionary<int, string> _channelMap = new Dictionary<int, string>();      
        private static List<int> _enabledChannels = new List<int>();        
        private static List<int> _disabledChannels = new List<int>();
                                                                                   
        private static Semaphore _semaphore = new Semaphore();

        private static List<KeyValuePair<int, string>> _cache = new List<KeyValuePair<int, string>>();
        private static bool _cachingEnabled = false;

        /// <summary>
        /// Empty if caching is disabled
        /// Contains all logged messages since caching is enabled
        /// </summary>
        public static ReadOnlyCollection<KeyValuePair<int, string>> Cache {
            get { return _cache.AsReadOnly(); }
        }

        /// <summary>
        /// Enabled Caching. This will clear the cache. Cached messaged will be stored in <see cref="Cache"/>
        /// </summary>
        public static void EnableCaching() {
            _cache.Clear();
            _cachingEnabled = true;
        }
        /// <summary>
        /// Disables Caching. This will clear the cache. Cached messaged will be stored in <see cref="Cache"/>
        /// </summary>
        public static void DisableCaching() {
            _cache.Clear();
            _cachingEnabled = false;
        }

        /// <summary>
        /// A readonly collection of registered channels
        /// </summary>
        public static ReadOnlyCollection<int> Channels {
            get {                                        
                List<int> channels = new List<int>();

                foreach (int channel in _channelMap.Keys)
                    channels.Add(channel);

                return channels.AsReadOnly();
            }
        }
        /// <summary>
        /// A readonly collection of enabled channels
        /// </summary>
        public static ReadOnlyCollection<int> EnabledChannels {
            get {                                       
                return _enabledChannels.AsReadOnly();
            }
        }      
        /// <summary>
        /// A readonly collection of disabled channels
        /// </summary>
        public static ReadOnlyCollection<int> DisabledChannels {
            get {                                 
                return _disabledChannels.AsReadOnly();
            }
        }  

        /// <summary>
        /// Registers a new channel for use
        /// </summary>
        /// <param name="name">The name of the channel</param>
        /// <returns>The int identifier of the channel for function calls. -1 if failed</returns>
        public static int AddChannel(string name) { 
            _semaphore.Enqueue();

            if (_channelMap.ContainsValue(name)) {
                _semaphore.Dequeue();
                return _channelMap.FirstOrDefault(c => c.Value.Equals(name)).Key;
            }

            for (int i = 0; i < int.MaxValue; i++) {
                if (!_channelMap.ContainsKey(i)) {
                    _channelMap.Add(i, name);
                    ChannelsChanged?.Invoke();
                    EnableChannel(i);
                    _semaphore.Dequeue();
                    return i;
                }
            }

            _semaphore.Dequeue();
            return -1;
        }

        /// <summary>
        /// Deletes a channel
        /// </summary>
        /// <param name="channel">The int identifier of the channel</param>
        public static void DeleteChannel(int channel) {       
            _semaphore.Enqueue();   

            _channelMap.Remove(channel);
            ChannelsChanged?.Invoke();

            _semaphore.Dequeue();
        }

        /// <summary>
        /// Returns to corresponding channel name of a identifier
        /// </summary>
        /// <param name="channel">The channels identifier</param>
        /// <returns>The channels name. Null if failed</returns>
        public static string GetChannelName(int channel) {     
            if (!_channelMap.ContainsKey(channel))
                return null;

            return _channelMap[channel];
        }

        /// <summary>
        /// Returns the assigned channel id of a registered channel
        /// </summary>
        /// <param name="name">The channels name</param>
        /// <returns>The id of the channel. -1 if not registered</returns>
        public static int GetChannelId(string name) {
            if (!_channelMap.ContainsValue(name))
                return -1;

            return _channelMap.FirstOrDefault(c => c.Value.Equals(name)).Key;
        }

        /// <summary>
        /// Enabled a channel. Enabled channels messages will get logged
        /// </summary>
        /// <param name="channel">The channel identifier</param>
        public static void EnableChannel(int channel) {     
            _disabledChannels.Remove(channel);
            _enabledChannels.Add(channel);


            DisabledChannelsChanged?.Invoke();
            EnabledChannelsChanged?.Invoke();               
        }

        /// <summary>
        /// Disables a channel. Disabled channels messages will not get logged
        /// </summary>
        /// <param name="channel">The channel identifier</param>
        public static void DisableChannel(int channel) {     
            _enabledChannels.Remove(channel);
            _disabledChannels.Add(channel);


            DisabledChannelsChanged?.Invoke();
            EnabledChannelsChanged?.Invoke();  
        }

        /// <summary>
        /// Loggs a message. The message will only cause the MessageListened event to occur if the calling channel is enabled
        /// </summary>
        /// <param name="channel">The calling channel</param>
        /// <param name="message">The channels message</param>
        public static void Log(int channel, string message) {       
            if (!_channelMap.ContainsKey(channel))
                return;
            if (!_enabledChannels.Contains(channel))
                return;

            _semaphore.Enqueue();

            MessageListened?.Invoke(channel, message);

            if (_cachingEnabled)
                _cache.Add(new KeyValuePair<int, string>(channel, message));

            _semaphore.Dequeue();
        }
    }
}
