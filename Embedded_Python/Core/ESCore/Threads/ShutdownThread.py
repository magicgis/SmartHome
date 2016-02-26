import time
import sys

import kivy.clock as clock

from kivy.app import App
from ESApi.Thread import Thread

from ESCore.ServerProvider import ServerProvider

import ESCore.Controller.AppLauncherController as AppLauncherController
import ESCore.ApplicationManager as ApplicationManager
import ESCore.CoreFileIO as CoreFileIO
import ESApi.Networking as Networking

class ShutdownThread(Thread):
    """
        The shutdown thread handles unloading all the apps and the core system
    """

    def _run(self):
        self.set_callback(self.__finished)
        for index in range(0, ApplicationManager.instance.application_count()):
            ApplicationManager.instance.application_at(index).app().on_system_shutdown()

        Networking.instance.get_server().free_all_connections()

    def __finished(self):
        App.get_running_app().stop()
        sys.exit()