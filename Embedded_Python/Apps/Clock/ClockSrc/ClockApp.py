from ESApi.Application import Application
from ESApi.AppScreen import AppScreen


from ClockSrc.UI.MainScreen import MainScreen
from ClockSrc.UI.TimerScreen import TimerScreen

import ClockSrc.TimeManager as TimeManager
import ClockSrc.ClockNetworking as ClockNetworking



class ClockApp(Application):
    mainScreen = None  # type: MainScreen
    timerScreen = None  # type: TimerScreen

    __curScreen = None  # type: AppScreen

    def on_system_boot(self, finishedCallback: callable):
        set_instance(self)
        finishedCallback()
        TimeManager.instance.start()
        self.mainScreen = MainScreen()
        self.timerScreen = TimerScreen()
        self.__curScreen = self.mainScreen
        finishedCallback()

    def get_current_screen(self) -> AppScreen:
        return self.__curScreen

    def set_screen(self, screen: AppScreen):
        self.__curScreen = screen
        self.refresh_screen()

    def on_set(self):
        pass

    def on_unset(self):
        pass

global clockAppInstance  # type: ClockApp

def set_instance(i: ClockApp):
    clockAppInstance = i