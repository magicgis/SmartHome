import ClockSrc.ClockApp as ClockApp
import ESApi.Networking as Networking

from ESApi.ServerProvider import ConnectionIdentifier, ServerProvider
from ESApi.IXPFile import IXPFile


class TimeManager:
    __hours = -1  # type: int
    __minutes = -1  # type: int
    __seconds = -1  # type: int

    __callbacks = []  # type: List[callable]

    def start(self):
        server = Networking.instance.get_server()
        con = ClockApp.instance.get_connection()
        server.register_message_listener(con, "clock_time_listener", self.time_listener)

        request = IXPFile()
        request.set_network_function("com.projectgame.clock.clock.registertotimeservice")
        server.no_response_request(con, request)

    def register_time_listener(self, callback):
        """
            Registers a listener that gets calles when the time changes
        :param callback: Callable with a the following optional parameters:
                         - hours: int = -1
                         - minutes: int = -1
                         - seconds: int = -1
        """

        self.__callbacks.append(callback)

    def unregister_time_listener(self, callback):
        """
            Unregisters a listener from the TimeManager
        :param callback: The callback to unregister
        """

        self.__callbacks.remove(callback)

    def time_listener(self, message: IXPFile = None):
        sH = message.get_info("hours")
        sM = message.get_info("minutes")
        sS = message.get_info("seconds")

        iH = int(sH)
        iM = int(sM)
        iS = int(sS)

        self.__set_time(hours=iH, minutes=iM, seconds=iS)

    def __set_time(self, hours: int, minutes: int, seconds: int):
        timeChanges = False

        if hours is not self.__hours:
            timeChanges = True

        if minutes is not self.__minutes:
            timeChanges = True

        if seconds is not self.__seconds:
            timeChanges = True

        if not timeChanges:
            return

        self.__hours = hours
        self.__minutes = minutes
        self.__seconds = seconds

        for listener in self.__callbacks:
            listener(hours=self.__hours, minutes=self.__minutes, seconds=self.__seconds)

instance = TimeManager()