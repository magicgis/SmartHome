from kivy.app import App

from ESApi.Application import Application
from ESApi.AppScreen import AppScreen
from ESApi.ServerProvider import ServerProvider, ConnectionIdentifier

from QuitSrc.MainScreen import MainScreen

import ESApi.System as System

class QuitApp(Application):
    __curScreen = None  # type: AppScreen
    __connection = None  # type: ConnectionIdentifier

    def on_system_boot(self):
        instance = self
        self.__curScreen = MainScreen()

    def get_current_screen(self) -> AppScreen:
        return self.__curScreen

    def on_set(self):
        System.instance.shutdown()
