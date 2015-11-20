using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;

namespace Plugin {
    /// <summary>
    /// A delegate for the <see cref="PluginManager.PluginUnload"/> event
    /// </summary>
    /// <param name="plugin">The plugin that will be unloaded</param>
    public delegate void PluginChangeDelegate(Plugin plugin);

    /// <summary>
    /// The pluginmanager manages the loading/unloading and the already loaded plugins for the application
    /// </summary>
    public static class PluginManager {                  
        /// <summary>
        /// Gets called if a plugin gets unloaded so everyone can react to it
        /// </summary>
        public static event PluginChangeDelegate PluginUnload;
        /// <summary>
        /// Gets called if a plugin gets loaded so everyone can react to it
        /// </summary>
        public static event PluginChangeDelegate PluginLoad;

        private static List<Plugin> _plugins = new List<Plugin>();
        private static Semaphore _semaphore = new Semaphore();

        private static int _defaultDebugChannel = -1;        

        /// <summary>
        /// A readonly collection of currenty loaded plugins
        /// </summary>
        public static ReadOnlyCollection<string> Plugins {
            get {
                List<string> plugins = new List<string>();

                foreach (Plugin plugin in _plugins)
                    plugins.Add(plugin.Name);

                return plugins.AsReadOnly();
            }
        }

        /// <summary>
        /// A readonly collection of currently loaded display plugins
        /// </summary>
        public static ReadOnlyCollection<string> DisplayPlugins {
            get {
                List<string> plugins = new List<string>();

                foreach(Plugin plugin in _plugins) {
                    if (plugin.GetType().IsSubclassOf(typeof(DisplayPlugin)))
                        plugins.Add(plugin.Name);
                }

                return plugins.AsReadOnly();
            }
        }

        /// <summary>
        /// Returns the plugin with a specific name
        /// </summary>
        /// <param name="plugin">The plugins name</param>
        /// <returns>The found plugin or null if not found</returns>
        public static Plugin GetPlugin(string plugin) {
            return _plugins.FirstOrDefault(p => p.Name.Equals(plugin));
        }

        /// <summary>
        /// Loads all plugins in the specified directory
        /// </summary>
        /// <param name="dir">The target directory</param>
        public static void LoadFromDir(DirectoryInfo dir) {
            Thread thread = new Thread(new ParameterizedThreadStart(LoadFromDirAsync));
            thread.Start(dir);
        }  

        private static void LoadFromDirAsync(object odir) {
            DirectoryInfo dir = (DirectoryInfo)odir;

            _semaphore.Enqueue();

            if (_defaultDebugChannel == -1)
                _defaultDebugChannel = Debug.AddChannel("com.projectgame.admintools.pluginmanager");

            Debug.Log(_defaultDebugChannel, "==== Loading Plugins ====");
            Debug.Log(_defaultDebugChannel, "Directory: " + dir.FullName);

            List<Type> ul = new List<Type>();
            LoadFromDir(dir, out ul);

            Debug.Log(_defaultDebugChannel, "==== Finished Loading Plugins ====");

            Debug.DisableCaching();
            _semaphore.Dequeue();
        }

        private static void Unload(List<string> plugins) {
            foreach (Plugin plugin in _plugins)
                plugin.OnPluginsUnload(plugins);

            foreach(string plugin in plugins) {
                Plugin pplug = GetPlugin(plugin);

                if (pplug == null)
                    continue;

                PluginUnload?.Invoke(pplug);
                pplug.OnPluginUnload();
                _plugins.Remove(GetPlugin(plugin));
            }
        }
        /// <summary>
        /// Unloads all plugins
        /// </summary>
        public static void UnloadAll() {
            Unload(Plugins.ToList());
        }

        /// <summary>
        /// Loads all plugins in a directory
        /// </summary>
        /// <param name="dir">The DirectoryInfo for the destination directory</param>
        /// <param name="unloadablePlugins">Contains a List of plugins that could not be loaded</param>
        /// <returns>A List of loaded plugins</returns>
        private static void LoadFromDir(DirectoryInfo dir, out List<Type> unloadablePlugins) {
            if (dir == null || !dir.Exists) {
                unloadablePlugins = new List<Type>();
                return;
            }

            Debug.Log(_defaultDebugChannel, "Loading types...");
            List<Type> types = LoadTypes(dir);
            Debug.Log(_defaultDebugChannel, "Types found: " + types.Count);      
            int loadedPlugins = -1;
                         
            while (loadedPlugins != 0) {
                loadedPlugins = 0;

                List<Type> tempLoadedPlugins = new List<Type>();
                foreach (Type type in types) {
                    if (!NeededPluginsLoaded(type, _plugins))
                        continue;

                    Plugin plugin = type.InvokeMember(null, BindingFlags.CreateInstance, null, null, null) as Plugin;

                    Debug.Log(_defaultDebugChannel, "Plugin found: " + plugin.Name);
                    
                    _plugins.Add(plugin);
                    plugin.OnPluginLoad();
                    loadedPlugins++;
                    tempLoadedPlugins.Add(type);
                    PluginLoad?.Invoke(plugin);


                    foreach(MethodInfo method in type.GetMethods()) {
                        foreach(Attribute attr in Attribute.GetCustomAttributes(method)) {
                            if(attr is NetworkFunction) {
                                NetworkFunction nfattr = (NetworkFunction)attr;                                   

                                FunctionInfo info = new FunctionInfo(nfattr.Name, plugin, method);
                                NetworkManager.RegisterNetworkFunction(info);
                            }
                        }
                    }
                }

                foreach (Type type in tempLoadedPlugins)
                    types.Remove(type);
            }

            foreach (Plugin plugin in _plugins)
                plugin.OnPluginsLoad();

            Debug.Log(_defaultDebugChannel, "Unloadable Plugins: " + types.Count);

            unloadablePlugins = types;  
        }
        /// <summary>
        /// Loads all types from all files in a dir
        /// </summary>
        /// <param name="dir">The DirectoryInfo for the destination directory</param>
        /// <returns>A List of types</returns>
        private static List<Type> LoadTypes(DirectoryInfo dir) {
            if (dir == null || !dir.Exists)
                return new List<Type>();

            List<FileInfo> files = dir.GetFiles("*.dll").ToList();
            List<Type> types = new List<Type>();

            foreach (FileInfo info in files) {
                Assembly assembly = Assembly.LoadFrom(info.FullName);

                foreach (Type type in assembly.GetTypes()) {
                    if (type.IsSubclassOf(typeof(Plugin)) && !type.IsAbstract) {
                        types.Add(type);
                    }
                }
            }

            return types;
        }
        /// <summary>
        /// Checks if all of a plugins needed plugins are already loaded.
        /// 
        /// Will return true if the type is no subclass of Plugin or does not have any NeedsPlugin attributes
        /// </summary>
        /// <param name="plugin">The plugin to check</param>
        /// <param name="plugins">A temporary list of already loaded plugins</param>
        /// <returns>True if all required plugins are already loaded. Otherwise false</returns>
        private static bool NeededPluginsLoaded(Type plugin, List<Plugin> plugins) {
            Attribute[] attributes = Attribute.GetCustomAttributes(plugin);

            foreach (Attribute attribute in attributes) {
                if (!(attribute is NeedsPlugin))
                    continue;

                NeedsPlugin np = (NeedsPlugin)attribute;

                if (!PluginLoaded(np.NeededPlugin, plugins))
                    return false;
            }

            return true;
        }
        /// <summary>
        /// Checks if a plugin is already loaded
        /// </summary>
        /// <param name="plugin">The name of the plugin to check</param>
        /// <param name="plugins">A temporary list of already loaded plugins</param>
        /// <returns>True if the plugins is already loaded. Otherwise false</returns>
        private static bool PluginLoaded(string plugin, List<Plugin> plugins) {
            foreach (Plugin cur in plugins) {
                if (cur.Name.Equals(plugin))
                    return true;
            }

            return false;
        }
    }
}
