from ESApi.Application import Application
from ESApi.AppScreen import AppScreen
from ESApi.ServerProvider import ServerProvider, ConnectionIdentifier

import ESApi.Networking as Networking

from ClockSrc.UI.MainScreen import MainScreen

#import ClockSrc.TimeManager as TimeManager


class ClockApp(Application):
    __curScreen = None  # type: AppScreen
    __connection = None  # type: ConnectionIdentifier

    def on_system_boot(self):
        instance = self
        self.__curScreen = MainScreen()
  #      self.__connection = Networking.instance.get_server().request_connection()
 #       TimeManager.instance.start()

    def get_current_screen(self) -> AppScreen:
        return self.__curScreen

    def get_connection(self) -> ConnectionIdentifier:
        if not Networking.instance.get_server().connection_avaliable(self.__connection):
            Networking.instance.get_server().free_connection(self.__connection)
            self.__connection = None

        if self.__connection is None:
            self.__connection = Networking.instance.get_server().request_connection()

        return self.__connection

instance = None  # type: ClockApp
