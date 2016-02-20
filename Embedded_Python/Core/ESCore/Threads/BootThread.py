import time

import kivy.clock as clock

from ESApi.Thread import Thread

from ESCore.ServerProvider import ServerProvider
from ESCore.Threads.ShutdownThread import ShutdownThread

import ESCore.Controller.AppLauncherController as AppLauncherController
import ESCore.ApplicationManager as ApplicationManager
import ESCore.CoreFileIO as CoreFileIO
import ESApi.Networking as Networking
import ESApi.System as System

class BootThread(Thread):
    """
        The boot thread handles loading all the apps and the core system
    """

    __counter = 0

    def _run(self):
        self.__counter = 0

        #init system
        System.instance.provide_shutdown_callback(self.__system_shutdown_callback)

        # init networking
        Networking.instance.provide_server(ServerProvider())

        # init app launcher on main thread
        clock.ClockBase.schedule_once(clock.Clock, self._init_app_launcher)

        # load all applications in apps directory
        ApplicationManager.instance.load_applications(CoreFileIO.apps_directory())

        for index in range(0, ApplicationManager.instance.application_count()):
            ApplicationManager.instance.application_at(index).app().on_system_boot(self.__app_finished)

        while self.__counter < ApplicationManager.instance.application_count():
            time.sleep(0.1)

    def __app_finished(self):
        self.__counter = self.__counter + 1

    def _init_app_launcher(self, dt: int = 0):
        AppLauncherController.instance.initialize()


    def __system_shutdown_callback(self):
        thread = ShutdownThread()
        thread.start()