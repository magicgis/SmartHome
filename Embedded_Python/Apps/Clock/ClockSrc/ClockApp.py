from ESApi.Application import Application
from ESApi.AppScreen import AppScreen

from ClockSrc.UI.MainScreen import MainScreen

import ClockSrc.TimeManager as TimeManager
import ClockSrc.ClockNetworking as ClockNetworking

class ClockApp(Application):
    __curScreen = None  # type: AppScreen

    def on_system_boot(self):
        instance = self
        ClockNetworking.instance.connect()
        TimeManager.instance.start()
        self.__curScreen = MainScreen()

    def get_current_screen(self) -> AppScreen:
        return self.__curScreen

    def on_set(self):
        pass

    def on_unset(self):
        pass

instance = None  # type: ClockApp
